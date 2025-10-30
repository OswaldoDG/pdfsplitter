using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Pdf;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class SiguientePendienteCasoUso
    {
        private readonly IPdfService _pdfService;

        public SiguientePendienteCasoUso(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<DtoArchivo> SiguientePendiente()
        {
            return await _pdfService.SiguientePendiente();
        }
    }
}
