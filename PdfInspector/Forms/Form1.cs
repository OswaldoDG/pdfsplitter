using GdPicture;
using PdfInspector.Application.CasosUso.Pdf;
using PdfInspector.Application.DTOs.PDF;
using PdfInspector.Controles;
using PdfInspector.Domain.Models.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PdfInspector
{
    public partial class Form1 : Form
    {
        private readonly ObtieneTipoDocumentosPdfCasoUso _obtieneTipoDocumentosCasoUso;
        private readonly CompletarCasoUso _completarCasoUso;
        private readonly SiguientePendienteCasoUso _siguientePendienteCasoUso;
        private readonly MisEstadisticasCasoUso _misEstadisticasCasoUso;
        private List<DtoTipoDoc> _listaGlobalDeArchivos;
        private List<DtoParteDocumental> _listaPartes;
        private DtoArchivo _archivoPdf;
        private DtoParteDocumental _tempParteTemporal;
        private TablaDocumento _tempDocumentoTabla;
        private int _paginaInicioTemporal = 0;
        private int _paginaActualMostrada = -1;
        private bool _isProcessing = false;
        private const string GDILIC = "411897669724499821116142492193218";
        private const string HardcodedEncryptionKey = "pruebademomensajeria12345";
        private const int IvSize = 16;
        private string _currentUserEmail = "";
        public bool IsLoggingOut { get; private set; } = false;
        private static readonly HttpClient _httpClient = new HttpClient();

        public Form1(ObtieneTipoDocumentosPdfCasoUso obtieneTipoDocumentosCasoUso, CompletarCasoUso completarCasoUso, SiguientePendienteCasoUso siguientePendienteCasoUso, MisEstadisticasCasoUso misEstadisticasCasoUso)
        {
            InitializeComponent();
            KeyPreview = true;
            _obtieneTipoDocumentosCasoUso = obtieneTipoDocumentosCasoUso;
            _completarCasoUso = completarCasoUso;
            _siguientePendienteCasoUso = siguientePendienteCasoUso;
            _misEstadisticasCasoUso = misEstadisticasCasoUso;
            btnSig.BotonPresionado += BotonDocumento_Click;
            btnFin.BotonPresionado += BotonDocumento_Click;
            btnCancel.BotonPresionado += BotonDocumento_Click;
            btnComplete.BotonPresionado += BotonDocumento_Click;
            _listaPartes = new List<DtoParteDocumental>();
            this.gdViewer1.SetLicenseNumber(GDILIC);
            this.gdViewer1.MouseEnter += GdViewer_MouseEnter;
            this.gdViewer1.PageChanged += GdViewer_PageChanged;

            this.timerNotificacion.Tick += new System.EventHandler(this.timerNotificacion_Tick);
            chart1.PostPaint += chart1_PostPaint;
        }

        private void GdViewer_PageChanged()
        {
            if (this.gdViewer1.PageCount == 0)
                return;

            int paginaNueva = this.gdViewer1.CurrentPage;

            if (paginaNueva != _paginaActualMostrada)
            {
                infoDocControl.PaginaActual = paginaNueva;
                _paginaActualMostrada = paginaNueva;
            }
        }

        private void GdViewer_MouseEnter(object sender, EventArgs e)
        {
            this.gdViewer1.Focus();
        }

        private async void AppForm_Load(object sender, EventArgs e)
        {
            this.gdViewer1.ZoomMode = GdPicture.ViewerZoomMode.ZoomModeFitToViewer;
            btnComplete.TeclaAtajo = Keys.Enter;
            int anchoColumna = listViewPartes.ClientSize.Width / 4;
            foreach (ColumnHeader columna in listViewPartes.Columns)
            {
                columna.Width = anchoColumna;
            }
            CargarEmailUsuario();
            this.Text = $"Visor de Documentos - {_currentUserEmail}";
            this.toolStripButton1.ToolTipText = "Actualizar mis estadísticas";
            lbTotalPag.ActualizarTotal(0);
            await CargarDatosDeArchivos();
            listViewPartes.Items.Clear();
            chart1.PostPaint += (s, ev) =>
            {
                var chart = (Chart)s;
                var area = chart.ChartAreas[0];

                foreach (var pt in chart.Series[0].Points)
                {
                    var posX = (float)area.AxisX.ValueToPixelPosition(pt.XValue);
                    var posY = (float)area.AxisY.ValueToPixelPosition(pt.YValues[0]);
                    var yZero = (float)area.AxisY.ValueToPixelPosition(0);
                    var altura = yZero - posY;
                    var rect = new RectangleF(posX - 10, posY + altura / 2 - 8, 20, 16);

                    ev.ChartGraphics.Graphics.DrawString(
                        pt.YValues[0].ToString("0"),
                        chart1.Font,
                        Brushes.White,
                        rect,
                        new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        });
                }
            };
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
                MostrarNotificacion("No se pudieron cargar los documentos: " + ex.Message, "Error");
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
                MostrarNotificacion("Primero debe cargar un documento con 'Siguiente'.", "Warning");
                return;
            }

            if (this.gdViewer1.PageCount == 0)
            {
                MostrarNotificacion("Documento no cargado.", "Warning");
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
                    MostrarNotificacion(
                        "Ya está capturando un documento. 'Finalice' o 'Cancele' la captura actual.",
                        "Warning");

                    return;
                }

                Cursor = Cursors.WaitCursor;
                int paginaInicioActual = this.gdViewer1.CurrentPage;
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
                MostrarNotificacion("Error al procesar el documento: " + ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
                _isProcessing = false;
            }
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
                MostrarNotificacion("No ha iniciado la captura. Haga clic en un tipo de archivo primero.", "Warning");
                return;
            }

            int paginaFinalActual = this.gdViewer1.CurrentPage;
            int paginaInicial = _tempParteTemporal.PaginaInicio;

            if (paginaFinalActual < paginaInicial)
            {
                MostrarNotificacion(
                    $"Error: La página final ({paginaFinalActual}) no puede ser menor que la inicial ({paginaInicial}). Captura cancelada.",
                    "Error");

                _tempParteTemporal = null;
                _tempDocumentoTabla = null;

                infoDocControl.ActualizarInfo("Error Pág.", paginaInicial, paginaFinalActual);

                return;
            }

            foreach (var parteExistente in _listaPartes)
            {
                bool seTraslapa = (paginaInicial <= parteExistente.PaginaFin) && (paginaFinalActual >= parteExistente.PaginaInicio);
                if (seTraslapa)
                {
                    var tipoDocExistente = _listaGlobalDeArchivos.FirstOrDefault(t => t.Id == parteExistente.TipoDocumentoId)?.Nombre ?? "ID " + parteExistente.TipoDocumentoId;
                    MostrarNotificacion($"El rango {paginaInicial}-{paginaFinalActual} se traslapa con '{tipoDocExistente}' (Págs: {parteExistente.PaginaInicio}-{parteExistente.PaginaFin}). Elimine la parte anterior para continuar o cancele la captura.", "Error");
                    return;
                }
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

                MostrarNotificacion("Captura realizada", "Success");
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
                MostrarNotificacion("No hay documento/partes de documento para completar.", "Warning");
                return;
            }

            if (_tempParteTemporal != null)
            {
                MostrarNotificacion(
                    "Tiene una captura sin finalizar. 'Finalice' o 'Cancele' la captura actual.",
                    "Warning");
                return;
            }

            if (_listaPartes.Count == 0)
            {
                MostrarNotificacion("Necesita registrar al menos una parte para poder completar.", "Warning");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                var dto = new DtoFinalizar
                {
                    TotalPaginas = this.gdViewer1.PageCount,
                    Partes = _listaPartes
                };

                var completar = await Task.Run(() => _completarCasoUso.ExecuteAsync(_archivoPdf.Id, dto));
                ResetDocumentState();
                _tempParteTemporal = null;
                _archivoPdf = null;
                _listaPartes = new List<DtoParteDocumental>();
                _paginaInicioTemporal = 0;
                listViewPartes.Items.Clear();
                infoDocControl.ActualizarInfo("Completado", 0, 0);
                this.gdViewer1.CloseDocument();
                this.gdViewer1.Visible = false;

                if (!completar)
                {
                    MostrarNotificacion("Ocurrió un problema con el documento, intente más tarde.", "Error");
                }
                else
                {
                    MostrarNotificacion("Separación realizada correctamente.", "Success");
                    if (checkAuto.Checked)
                    {
                        await Task.Delay(500);
                        await SiguienteAccion();
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarNotificacion($"Error al procesar el documento: {ex.Message}", "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async Task CancelarAccion()
        {
            if (_tempParteTemporal == null)
            {
                MostrarNotificacion("No puedes cancelar porque no tienes una captura iniciada.", "Warning");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                _tempParteTemporal = null;
                _tempDocumentoTabla = null;

                int paginaActual = _paginaInicioTemporal;
                if (this.gdViewer1.PageCount > 0)
                {
                    paginaActual = this.gdViewer1.CurrentPage;
                }

                infoDocControl.ActualizarInfo("Cancelado", _paginaInicioTemporal, paginaActual);

                MostrarNotificacion("Captura cancelada", "Info");
            }
            catch (Exception ex)
            {
                MostrarNotificacion("Error al procesar el documento: " + ex.Message, "Error");
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
                this.gdViewer1.CloseDocument();
                var pdfPendiente = await _siguientePendienteCasoUso.SiguientePendiente();
                _archivoPdf = pdfPendiente;

                if (pdfPendiente == null)
                {
                    MostrarNotificacion("No hay más documentos pendientes de revisión.", "Info");
                    infoDocControl.ActualizarInfo("", 0, 0);
                    return;
                }

                if (string.IsNullOrEmpty(pdfPendiente.TokenSAS))
                {
                    MostrarNotificacion("Se asignó un documento pero no se recibió una URL de acceso.", "Warning");
                    infoDocControl.ActualizarInfo("", 0, 0);
                    return;
                }

                var response = await _httpClient.GetAsync(pdfPendiente.TokenSAS);
                if (response.IsSuccessStatusCode)
                {
                    var encryptedStream = await response.Content.ReadAsStreamAsync();

                    if (encryptedStream == null || encryptedStream.Length == 0)
                    {
                        MostrarNotificacion("No hay más documentos por procesar", "Warning");
                        infoDocControl.ActualizarInfo("", 0, 0);
                        lbTotalPag.ActualizarTotal(this.gdViewer1.PageCount);
                        return;
                    }

                    var decryptedStream = DesencriptarStream(encryptedStream);
                    var status = this.gdViewer1.DisplayFromStream(decryptedStream);

                    if (status != GdPictureStatus.OK)
                    {
                        MostrarNotificacion($"Error al cargar PDF en visor: {status}", "Error");
                        return;
                    }

                    this.gdViewer1.Visible = true;
                    _paginaActualMostrada = 1;
                    _paginaInicioTemporal = 1;
                    infoDocControl.ActualizarInfo("Sin Asignar", 1, 1);
                    Text = $"Visor de Documentos - {pdfPendiente.Nombre}";
                    lbTotalPag.ActualizarTotal(this.gdViewer1.PageCount);
                    this.gdViewer1.Focus();
                    inactividadTimer.Start();
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
                MostrarNotificacion("Error al descargar el archivo: " + ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.gdViewer1.PageCount > 0)
            {
                if (keyData == Keys.PageDown)
                {
                    this.gdViewer1.DisplayPreviousPage();
                    return true;
                }
                if (keyData == Keys.PageUp)
                {
                    this.gdViewer1.DisplayNextPage();
                    return true;
                }
            }

            BotonDocumento botonConAtajo = FindBotonDocumentoByShortcut(this, keyData);
            if (botonConAtajo != null)
            {
                botonConAtajo.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
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
        private void EliminaElemento()
        {
            if (listViewPartes.SelectedItems.Count == 0)
            {
                MostrarNotificacion("Por favor, selecciona un elemento para eliminar.", "Info");
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
        private void timerNotificacion_Tick(object sender, EventArgs e)
        {
            timerNotificacion.Stop();
            statusLabel.Text = string.Empty;
        }

        private void MostrarNotificacion(string mensaje, string tipo = "Warning")
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => MostrarNotificacion(mensaje, tipo)));
                return;
            }

            switch (tipo.ToLower())
            {
                case "error":
                    statusLabel.BackColor = Color.Red;
                    break;

                case "success":
                    statusLabel.BackColor = Color.Green;

                    break;
                case "info":
                    statusLabel.BackColor =  Color.LightGray;

                    break;
                case "warning":
                default:
                    statusLabel.BackColor = Color.Yellow;
                    break;
            }

            statusLabel.Text = mensaje;
;
        }
   
        private void tsbDel1_Click(object sender, EventArgs e)
        {
            this.EliminaElemento();
        }

        private string GetConfigPath()
        {
            string tempDir = Path.GetTempPath();
            string appDir = "pdfsplitter";
            string configFile = "config.txt";
            return Path.Combine(tempDir, appDir, configFile);
        }

        private void CargarEmailUsuario()
        {
            try
            {
                string configPath = GetConfigPath();
                if (File.Exists(configPath))
                {
                    _currentUserEmail = File.ReadAllText(configPath);
                }
            }
            catch (Exception ex)
            {
                _currentUserEmail = "Error al cargar email";
                Console.WriteLine("Error al cargar email: " + ex.Message);
            }
        }

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            chart1.Legends[0].Enabled = false;

            var serie = new Series("Estadística")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = false,
                Color = Color.SteelBlue
            };

            chart1.Series.Add(serie);

            var estadisticasUsuario = await Task.Run(() => _misEstadisticasCasoUso.ExecuteAsync());

            foreach (var estadistica in estadisticasUsuario.OrderBy(x => x.Fecha))
            {
                string etiqueta = estadistica.Fecha.ToString("dd/MM");
                serie.Points.AddXY(etiqueta, estadistica.Conteo);
            }

            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
            chart1.ChartAreas[0].AxisX.Title = "Fecha";
            chart1.ChartAreas[0].AxisY.Title = "Conteo";

            chart1.Invalidate();
        }

        private void chart1_PostPaint(object sender, ChartPaintEventArgs e)
        {
            var chart = (Chart)sender;
            var area = chart.ChartAreas[0];

            if (chart.Series.Count == 0)
                return;

            var series = chart.Series[0];
            if (series.Points.Count == 0)
                return;

            int count = series.Points.Count;
            float xMin = (float)area.AxisX.ValueToPixelPosition(0.5);
            float xMax = (float)area.AxisX.ValueToPixelPosition(count + 0.5f);
            float totalWidth = Math.Abs(xMax - xMin);
            float columnWidth = totalWidth / count * 0.6f;

            for (int p = 0; p < series.Points.Count; p++)
            {
                float x = (float)area.AxisX.ValueToPixelPosition(p + 1);
                float y0 = (float)area.AxisY.ValueToPixelPosition(0);
                float y1 = (float)area.AxisY.ValueToPixelPosition(series.Points[p].YValues[0]);
                var rect = new RectangleF(
                    x - columnWidth / 2,
                    Math.Min(y0, y1),
                    columnWidth,
                    Math.Abs(y0 - y1));

                using (var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    e.ChartGraphics.Graphics.DrawString(
                        series.Points[p].YValues[0].ToString("0"),
                        chart.Font,
                        Brushes.White,
                        rect,
                        sf);
                }
            }
        }

        private void ResetDocumentState()
        {
            inactividadTimer.Stop();

            _archivoPdf = null;
            _listaPartes.Clear();
            _tempDocumentoTabla = null;
            _tempParteTemporal = null;
            _paginaInicioTemporal = 0;
            listViewPartes.Items.Clear();
            this.gdViewer1.CloseDocument();
            this.gdViewer1.Visible = false;
            lbTotalPag.ActualizarTotal(0);
            Text = $"Visor de Documentos - {_currentUserEmail}";
        }

        private void inactividadTimer_Tick(object sender, EventArgs e)
        {
            inactividadTimer.Stop();

            if (_archivoPdf != null)
            {
                MostrarNotificacion("Documento cerrado por inactividad (1 hora).", "Info");

                if (checkAuto.Checked)
                {
                    checkAuto.Checked = false;
                }

                ResetDocumentState();
                infoDocControl.ActualizarInfo("Cerrado por inactividad", 0, 0);
            }
        }
    }
}