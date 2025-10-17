using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Pdf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class DescargarPdfCasoUso
    {
        private readonly IPdfService _pdfService;

        public DescargarPdfCasoUso(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<byte[]> ExecuteAsync(int id)
        {
            return await _pdfService.DescargarPdfPorIdAsync(id);
        }
    }
}
