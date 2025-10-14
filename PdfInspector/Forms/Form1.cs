using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Domain.Models.Pdf;
using PdfInspector.Infraestructure.Config;
using PdfInspector.Infraestructure.Services.Pdf;
using PdfiumViewer;
using Spire.Pdf.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PdfInspector
{
    public partial class Form1 : Form
    {
        private readonly string _token;
        private IPdfServiceFactory _pdfServiceFactory { get; }
        private List<ParteDocumental> _secciones = new List<ParteDocumental>();
        private int? _paginaInicioTemporal = null;
        private int _paginaFinTemporal;
        private string _idDocumentoActual;
        private string _NombreDocumentoActual;
        public Form1(string token, IPdfServiceFactory pdfServiceFactory)
        {
            _token = token;
            _pdfServiceFactory = pdfServiceFactory;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string doc = @"C:\medica\CAJA 18_146.pdf";
            pdfViewer1.Document = OpenDocument(doc);
                //pdfDocumentViewer1.PageNumberChanged += PdfDocumentViewer1_PageNumberChanged;   
            //pdfDocumentViewer1.LoadFromFile();

        }

        private void PdfDocumentViewer1_PageNumberChanged(object sender, EventArgs args)
        {
            Console.WriteLine("Page number changed: " + pdfDocumentViewer1.CurrentPageNumber.ToString());
        }

        private PdfDocument OpenDocument(string fileName)
        {
            try
            {
                return PdfDocument.Load(this, fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pdfViewer1.Renderer.Page.ToString());
        }

        private async void btnDescargar_Click(object sender, EventArgs e)
        {
            try
            {
                pdfViewer1.Document = null;
                if(!int.TryParse(txtPdf.Text, out int id))
                {
                    MessageBox.Show("Ingrese un ID válido");
                    return;
                }

                var pdfService = _pdfServiceFactory.Create(_token);
                string ruta = await pdfService.DescargarPdfPorId(id);
                string nombreConId = Path.GetFileNameWithoutExtension(ruta);
                Match match = Regex.Match(nombreConId, @"^(.*)_(\d+)$");

                if (match.Success)
                {
                    _NombreDocumentoActual = match.Groups[1].Value;
                    _idDocumentoActual = match.Groups[2].Value;
                }

                pdfViewer1.Document = OpenDocument(ruta);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnInicioPag_Click(object sender, EventArgs e)
        {
            if (pdfViewer1.Document == null)
            {
                MessageBox.Show("No hay documento cargado.");
                return;
            }

            _paginaInicioTemporal = pdfViewer1.Renderer.Page + 1;
            MessageBox.Show($"Inicio marcado en página {_paginaInicioTemporal}");
        }

        private void btnFinPag_Click(object sender, EventArgs e)
        {
            if (_paginaInicioTemporal == null)
            {
                MessageBox.Show("Primero debes marcar el inicio.");
                return;
            }

            int paginaFin = pdfViewer1.Renderer.Page + 1;

            if (paginaFin < _paginaInicioTemporal)
            {
                MessageBox.Show("La página de fin no puede ser menor que la de inicio.");
                return;
            }

            string nombre = txtNombreParte.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Ingrese un nombre para la sección antes de marcar el fin.");
                return;
            }

            int nuevoId = _secciones.Any() ? _secciones.Max(s => s.Id) + 1 : 1;

            var nuevaSeccion = new ParteDocumental
            {
                Id = nuevoId,
                ArchivoPdfId = int.Parse(_idDocumentoActual),
                PaginaInicio = _paginaInicioTemporal.Value,
                PaginaFin = paginaFin,
                TipoDocumentoId = int.Parse(_idDocumentoActual)
            };

            _secciones.Add(nuevaSeccion);

            _paginaInicioTemporal = null;
            txtNombreParte.Text = "";

            MessageBox.Show($"Sección '{nombre}' registrada: Páginas {nuevaSeccion.PaginaInicio} - {nuevaSeccion.PaginaFin}");
        }

        private DtoFinalizar GenerarDtoSecciones()
        {
            var dto = new DtoFinalizar
            {
                Partes = _secciones.ToList(),
                TotalPaginas = _secciones.Sum(s => (s.PaginaFin - s.PaginaInicio + 1))
            };

            return dto;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            btnCrearPartes.Enabled = false;
            btnFinPag.Enabled = false;
            btnInicioPag.Enabled = false;
            progressBarPartes.Visible = true;
            Cursor = Cursors.WaitCursor;

            try
            {
                var pdfService = _pdfServiceFactory.Create(_token);
                bool repuesta= await pdfService.FinalizarPorId(int.Parse(_idDocumentoActual), GenerarDtoSecciones());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de autenticación: {ex.Message}");
            }
            finally
            {
                btnCrearPartes.Enabled = true;
                btnFinPag.Enabled = true;
                btnInicioPag.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async  void btnRevision_Click(object sender, EventArgs e)
        {
            btnCrearPartes.Enabled = false;
            btnFinPag.Enabled = false;
            btnInicioPag.Enabled = false;
            progressBarPartes.Visible = true;
            Cursor = Cursors.WaitCursor;

            try
            {
                var pdfService = _pdfServiceFactory.Create(_token);
                var repuesta = await pdfService.Siguiente();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de autenticación: {ex.Message}");
            }
            finally
            {
                btnCrearPartes.Enabled = true;
                btnFinPag.Enabled = true;
                btnInicioPag.Enabled = true;
                Cursor = Cursors.Default;
            }
        }
    }
}
