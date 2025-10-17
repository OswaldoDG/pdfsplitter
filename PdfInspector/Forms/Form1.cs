using PdfInspector.Application.CasosUso.Pdf;
using PdfInspector.Domain.Models.Pdf;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfInspector
{
    public partial class Form1 : Form
    {
        private readonly ObtieneTipoDocumentosPdfCasoUso _obtieneTipoDocumentosCasoUso;
        private readonly DescargarPdfCasoUso _descargarPdfCasoUso;
        private List<DtoTipoDoc> _listaGlobalDeArchivos;
        public bool IsLoggingOut { get; private set; } = false;

        public Form1(ObtieneTipoDocumentosPdfCasoUso obtieneTipoDocumentosCasoUso, DescargarPdfCasoUso descargarPdfCasoUso)
        {
            InitializeComponent();
            _obtieneTipoDocumentosCasoUso = obtieneTipoDocumentosCasoUso;
            _descargarPdfCasoUso = descargarPdfCasoUso;
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            txtIdsDocumentos.Focus();
        }

        private async void btnCargarDocumentos_Click(object sender, EventArgs e)
        {
            var idsParaSolicitar = new List<int>();
            string textoIds = txtIdsDocumentos.Text;

            if (string.IsNullOrWhiteSpace(textoIds))
            {
                MessageBox.Show("Por favor, ingrese al menos un ID.", "Entrada Requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string[] idStrings = textoIds.Split(',');
            foreach (var idStr in idStrings)
            {
                if (int.TryParse(idStr.Trim(), out int id))
                {
                    idsParaSolicitar.Add(id);
                }
            }

            if (idsParaSolicitar.Count == 0)
            {
                MessageBox.Show("No se encontraron IDs numéricos válidos en la entrada.", "Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await CargarDatosDeArchivos(idsParaSolicitar);
        }

        private async Task CargarDatosDeArchivos(List<int> ids)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _listaGlobalDeArchivos = await _obtieneTipoDocumentosCasoUso.ExecuteAsync(ids);
                CrearBotonesDesdeLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar los documentos: " + ex.Message, "Error de Red", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void CrearBotonesDesdeLista()
        {
            flowLayoutPanelArchivos.Controls.Clear();
            if (_listaGlobalDeArchivos == null) return;

            foreach (var archivo in _listaGlobalDeArchivos)
            {
                var nuevoBoton = new Button
                {
                    Text = archivo.Nombre,
                    Size = new Size(flowLayoutPanelArchivos.ClientSize.Width - 10, 40),
                    Tag = archivo
                };
                nuevoBoton.Click += BotonArchivo_Click;
                flowLayoutPanelArchivos.Controls.Add(nuevoBoton);
            }
        }

        private async void BotonArchivo_Click(object sender, EventArgs e)
        {
            var botonPulsado = (Button)sender;
            var archivoSeleccionado = (DtoTipoDoc)botonPulsado.Tag;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                byte[] pdfBytes = await _descargarPdfCasoUso.ExecuteAsync(archivoSeleccionado.Id);

                if (pdfBytes != null && pdfBytes.Length > 0)
                {
                    if (pdfViewer.Document != null)
                    {
                        pdfViewer.Document.Dispose();
                    }
                    var stream = new MemoryStream(pdfBytes);
                    pdfViewer.Document = PdfDocument.Load(stream);
                }
                else
                {
                    MessageBox.Show("El archivo está vacío o no se pudo descargar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Tu sesión ha expirado. Por favor, vuelve a iniciar sesión.", "Sesión Expirada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                IsLoggingOut = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al descargar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
