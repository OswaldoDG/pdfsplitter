using PdfInspector.Application.CasosUso.Pdf;
using PdfInspector.Application.DTOs.PDF;
using PdfInspector.Controles;
using PdfInspector.Domain.Models.Pdf;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PdfInspector
{
    public partial class Form1 : Form
    {
        private readonly ObtieneTipoDocumentosPdfCasoUso _obtieneTipoDocumentosCasoUso;
        private readonly CompletarCasoUso _completarCasoUso;
        private readonly SiguientePendienteCasoUso _siguientePendienteCasoUso;
        private List<DtoTipoDoc> _listaGlobalDeArchivos;
        private List<DtoParteDocumental> _listaPartes;
        private DtoArchivo _archivoPdf;
        private DtoParteDocumental _tempParteTemporal;
        private TablaDocumento _tempDocumentoTabla;
        private int _paginaInicioTemporal = 0;
        private int _paginaActualMostrada = -1;
        private bool _isProcessing = false;

        private const string HardcodedEncryptionKey = "pruebademomensajeria12345";
        private const int IvSize = 16;
        public bool IsLoggingOut { get; private set; } = false;
        private static readonly HttpClient _httpClient = new HttpClient();

        public Form1(ObtieneTipoDocumentosPdfCasoUso obtieneTipoDocumentosCasoUso, CompletarCasoUso completarCasoUso, SiguientePendienteCasoUso siguientePendienteCasoUso)
        {
            InitializeComponent();
            KeyPreview = true;
            _obtieneTipoDocumentosCasoUso = obtieneTipoDocumentosCasoUso;
            _completarCasoUso = completarCasoUso;
            _siguientePendienteCasoUso = siguientePendienteCasoUso;
            btnSig.BotonPresionado += BotonDocumento_Click;
            btnFin.BotonPresionado += BotonDocumento_Click;
            btnCancel.BotonPresionado += BotonDocumento_Click;
            btnComplete.BotonPresionado += BotonDocumento_Click;
            _listaPartes = new List<DtoParteDocumental>();
            pdfVisor.MouseWheel += PdfVisor_MouseWheel;
            pdfVisor.Scroll += PdfVisor_Scroll;
            pdfVisor.MouseEnter += PdfVisor_MouseEnter;
            pdfVisor.Renderer.DisplayRectangleChanged += Renderer_DisplayRectangleChanged;
        }

        private void Renderer_DisplayRectangleChanged(object sender, EventArgs e)
        {
            pdfVisor.Focus();
            ActualizarPaginaActual();
        }

        private void PdfVisor_MouseEnter(object sender, EventArgs e)
        {
            pdfVisor.Focus();
        }

        private void PdfVisor_Scroll(object sender, ScrollEventArgs e)
        {
            pdfVisor.Focus();
            ActualizarPaginaActual();
        }

        private void PdfVisor_MouseWheel(object sender, MouseEventArgs e)
        {
            pdfVisor.Focus();
            ActualizarPaginaActual();
        }

        private void ActualizarPaginaActual()
        {
            if (pdfVisor.Document == null)
                return;

            int paginaNueva = pdfVisor.Renderer.Page + 1;

            if (paginaNueva != _paginaActualMostrada)
            {
                infoDocControl.PaginaActual= paginaNueva;
                _paginaActualMostrada = paginaNueva;
            }
        }

        private async void AppForm_Load(object sender, EventArgs e)
        {
            int anchoColumna = listViewPartes.ClientSize.Width / 4;
            foreach (ColumnHeader columna in listViewPartes.Columns)
            {
                columna.Width = anchoColumna;
            }
            await CargarDatosDeArchivos();
            listViewPartes.Items.Clear();
        }

        private async Task CargarDatosDeArchivos()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _listaGlobalDeArchivos = await _obtieneTipoDocumentosCasoUso.ExecuteAsync();
                CrearBotonesDesdeLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los documentos: " + ex.Message, "Error de Red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CrearBotonesDesdeLista()
        {
            flowLayoutPanelArchivos.Controls.Clear();
            if (_listaGlobalDeArchivos == null) return;

            Keys[] teclasDisponibles = new[] { Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y };
            int indice = 0;

            foreach (var archivo in _listaGlobalDeArchivos)
            {
                var nuevoBoton = new BotonDocumento
                {
                    Texto = archivo.Nombre,
                    Size = new Size(flowLayoutPanelArchivos.ClientSize.Width - 130, 50),
                    Tag = archivo,
                };

                if (indice < teclasDisponibles.Length)
                    nuevoBoton.TeclaAtajo = teclasDisponibles[indice];

                nuevoBoton.BotonPresionado += BotonArchivo_Click;
                flowLayoutPanelArchivos.Controls.Add(nuevoBoton);
                indice++;
            }
        }

        private async void BotonArchivo_Click(object sender, EventArgs e)
        {
            if (_isProcessing)
            {
                return;
            }

            if (_archivoPdf == null)
            {
                MessageBox.Show("Primero debe cargar un documento con 'Siguiente'.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _isProcessing = true;

                var botonPulsado = (BotonDocumento)sender;
                var archivoSeleccionado = (DtoTipoDoc)botonPulsado.Tag;

                if (_tempParteTemporal != null)
                {
                    if (_tempParteTemporal.TipoDocumentoId == archivoSeleccionado.Id)
                    {
                        return;
                    }

                    MessageBox.Show(
                        "Ya está capturando un documento. Debe 'Finalizar' o 'Cancelar' la captura actual antes de seleccionar otra.",
                        "Captura en Progreso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                Cursor = Cursors.WaitCursor;
                int paginaInicioActual = pdfVisor.Renderer.Page + 1;
                _tempParteTemporal = new DtoParteDocumental
                {
                    Id = _listaPartes.Any() ? _listaPartes.Max(x => x.Id) + 1 : 1,
                    ArchivoPdfId = _archivoPdf.Id,
                    PaginaInicio = paginaInicioActual,
                    PaginaFin = 0,
                    TipoDocumentoId = archivoSeleccionado.Id
                };

                _tempDocumentoTabla = new TablaDocumento()
                {
                    Id = _tempParteTemporal.Id,
                    TipoDocumento = archivoSeleccionado.Nombre,
                    PagInicio = paginaInicioActual,
                    PagFinal = 0
                };

                _paginaInicioTemporal = paginaInicioActual;

                infoDocControl.ActualizarInfo(
                    archivoSeleccionado.Nombre,
                    paginaInicioActual,
                    paginaInicioActual
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el documento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                _isProcessing = false;
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            BotonDocumento botonConAtajo = FindBotonDocumentoByShortcut(this, keyData);

            if (botonConAtajo != null)
            {
                botonConAtajo.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async void BotonDocumento_Click(object sender, EventArgs e)
        {
            if (_isProcessing)
            {
                return;
            }
            if (sender is BotonDocumento boton)
            {
                try
                {
                    _isProcessing = true;
                    flowLayoutPanelArchivos.Enabled = false;
                    btnSig.Enabled = false;
                    btnFin.Enabled = false;
                    btnCancel.Enabled = false;
                    btnComplete.Enabled = false;

                    switch (boton.Tag)
                    {
                        case "Finalizar":
                            await FinalizarAccion();
                            break;
                        case "Completar":
                            await CompletarAccion();
                            break;
                        case "Cancelar":
                            await CancelarAccion();
                            break;
                        case "Siguiente":
                            await SiguienteAccion();
                            break;
                    }
                }
                finally
                {
                    _isProcessing = false;
                    flowLayoutPanelArchivos.Enabled = true;
                    btnSig.Enabled = true;
                    btnFin.Enabled = true;
                    btnCancel.Enabled = true;
                    btnComplete.Enabled = true;
                }
            }
        }

        private async Task FinalizarAccion()
        {
            if (_tempParteTemporal == null)
            {
                MessageBox.Show("No ha iniciado la captura de ningún documento. " +
                                "Haga clic en un tipo de archivo primero.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int paginaFinalActual = pdfVisor.Renderer.Page + 1;
            int paginaInicial = _tempParteTemporal.PaginaInicio;

            if (paginaFinalActual < paginaInicial)
            {
                MessageBox.Show(
                    $"Error: La página final ({paginaFinalActual}) no puede ser menor que la página inicial ({paginaInicial}).\n\nLa captura será cancelada.",
                    "Error de Paginación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                _tempParteTemporal = null;
                _tempDocumentoTabla = null;

                infoDocControl.ActualizarInfo("Error Pág.", paginaInicial, paginaFinalActual);

                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                _tempParteTemporal.PaginaFin = paginaFinalActual;
                _tempDocumentoTabla.PagFinal = _tempParteTemporal.PaginaFin;

                var item = new ListViewItem(_tempDocumentoTabla.Id.ToString());
                item.SubItems.Add(_tempDocumentoTabla.TipoDocumento);
                item.SubItems.Add(_tempDocumentoTabla.PagInicio.ToString());
                item.SubItems.Add(_tempDocumentoTabla.PagFinal.ToString());
                listViewPartes.Items.Add(item);

                _listaPartes.Add(_tempParteTemporal);
                _tempParteTemporal = null;
                _tempDocumentoTabla = null;
                infoDocControl.ActualizarInfo("Captura Guardada", _paginaInicioTemporal, paginaFinalActual);

                MessageBox.Show("Captura realizada", "Captura", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task CompletarAccion()
        {
            if (_archivoPdf == null)
            {
                MessageBox.Show("No hay documento cargado para completar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_tempParteTemporal != null)
            {
                MessageBox.Show(
                    "Ha iniciado una captura que no ha sido finalizada. Por favor, 'Finalice' o 'Cancele' la captura actual antes de completar el documento.",
                    "Captura en Progreso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var dto = new DtoFinalizar
                {
                    TotalPaginas = pdfVisor.Document.PageCount,
                    Partes = _listaPartes
                };

                var completar = await Task.Run(() => _completarCasoUso.ExecuteAsync(_archivoPdf.Id, dto));

                _tempParteTemporal = null;
                _archivoPdf = null;
                _listaPartes = new List<DtoParteDocumental>();
                _paginaInicioTemporal = 0;
                listViewPartes.Items.Clear();
                infoDocControl.ActualizarInfo("Completado", 0, 0);
                this.Text = "Visor de Documentos";

                if (pdfVisor.Document != null)
                {
                    pdfVisor.Document.Dispose();
                    pdfVisor.Document = null;
                }


                if (!completar)
                    MessageBox.Show("Ocurrió un problema con el documento, intente más tarde.", "Documentación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Separación realizada correctamente.", "Documentación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar el documento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task CancelarAccion()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                _tempParteTemporal = null;
                _tempDocumentoTabla = null;
                infoDocControl.ActualizarInfo("Cancelado", _paginaInicioTemporal, pdfVisor.Renderer.Page + 1);
                MessageBox.Show("Captura cancelada", "Cancelación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el documento: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task SiguienteAccion()
        {
            if (_listaPartes.Any() || _tempParteTemporal != null)
            {
                var result = MessageBox.Show(
                    "Tiene cambios sin 'Completar'. ¿Desea descartarlos y cargar el siguiente documento?",
                    "Cambios sin guardar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;
                }
            }
            try
            {
                _archivoPdf = null;
                Cursor = Cursors.WaitCursor;
                _listaPartes.Clear();
                _tempDocumentoTabla = null;
                _tempParteTemporal = null;
                _paginaInicioTemporal = 0;
                infoDocControl.ActualizarInfo("Cargando...", 0, 0);
                var pdfPendiente = await _siguientePendienteCasoUso.SiguientePendiente();
                _archivoPdf = pdfPendiente;

                if (pdfPendiente == null)
                {
                    MessageBox.Show("No hay más documentos pendientes de revisión.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrEmpty(pdfPendiente.TokenSAS))
                {
                    MessageBox.Show("Se asignó un documento pero no se recibió una URL de acceso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var response = await _httpClient.GetAsync(pdfPendiente.TokenSAS);
                if (response.IsSuccessStatusCode)
                {
                    var encryptedStream = await response.Content.ReadAsStreamAsync();

                    if (encryptedStream == null || encryptedStream.Length == 0)
                    {
                        MessageBox.Show(
                            "La descarga fue exitosa, pero el archivo no tiene contenido.",
                            "Error de Contenido",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    var decryptedStream = DesencriptarStream(encryptedStream);

                    if (pdfVisor.Document != null)
                        pdfVisor.Document.Dispose();

                    pdfVisor.Document = PdfDocument.Load(decryptedStream);
                    _paginaActualMostrada = 1;
                    _paginaInicioTemporal = 1;
                    infoDocControl.ActualizarInfo("Sin Asignar", 1, 1);
                    Text = $"Visor de Documentos - {pdfPendiente.Nombre}";
                    pdfVisor.Focus();
                }
                else
                {
                    string codigoError = response.StatusCode.ToString();
                    string mensajeDetallado = $"Error al descargar el archivo. El servidor respondió con:\n\n{codigoError} ({(int)response.StatusCode})";
                    try
                    {
                        string errorBody = await response.Content.ReadAsStringAsync();
                        string detalle = errorBody.Substring(0, Math.Min(errorBody.Length, 300));
                        mensajeDetallado += $"\n\nDetalle del servidor:\n{detalle}";
                    }
                    catch
                    {
                    }

                    MessageBox.Show(
                        mensajeDetallado,
                        "Error de Descarga",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private BotonDocumento FindBotonDocumentoByShortcut(Control container, Keys keyData)
        {
            foreach (Control c in container.Controls)
            {
                if (c is BotonDocumento boton && boton.TeclaAtajo == keyData)
                {
                    return boton;
                }

                if (c.HasChildren)
                {
                    BotonDocumento found = FindBotonDocumentoByShortcut(c, keyData);
                    if (found != null)
                    {
                        return found;
                    }
                }
            }
            return null;
        }

        private void btnEliminarTabla_Click(object sender, EventArgs e)
        {
            if (listViewPartes.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona un elemento para eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ListViewItem seleccionado = listViewPartes.SelectedItems[0];

            var confirmacion = MessageBox.Show(
                $"¿Seguro que deseas eliminar \"{seleccionado.Text}\"?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmacion == DialogResult.Yes)
            {
                int idParte = int.Parse(seleccionado.SubItems[0].Text);
                _listaPartes.Remove(_listaPartes.FirstOrDefault(x => x.Id == idParte));
                listViewPartes.Items.Remove(seleccionado);
            }
        }

        private Stream DesencriptarStream(Stream encryptedStream)
        {
            var keyBytes = new byte[32];
            var configKeyBytes = System.Text.Encoding.UTF8.GetBytes(HardcodedEncryptionKey);
            Array.Copy(configKeyBytes, keyBytes, Math.Min(keyBytes.Length, configKeyBytes.Length));

            byte[] iv = new byte[IvSize];

            encryptedStream.Seek(0, SeekOrigin.Begin);

            encryptedStream.Read(iv, 0, IvSize);

            using (var aes = System.Security.Cryptography.Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                {
                    var decryptedStream = new MemoryStream();

                    using (var cryptoStream = new System.Security.Cryptography.CryptoStream(encryptedStream, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(decryptedStream);
                    }

                    decryptedStream.Seek(0, SeekOrigin.Begin);
                    return decryptedStream;
                }
            }
        }
    }
}