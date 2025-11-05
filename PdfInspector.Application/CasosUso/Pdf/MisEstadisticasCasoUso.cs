using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Pdf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class MisEstadisticasCasoUso
    {
        private readonly IPdfService _pdfService;

        public MisEstadisticasCasoUso(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<List<DtoEstadisticasUsuario>> ExecuteAsync() 
        {
            return await _pdfService.EstadisticasUsuarioAsync();
        }
    }
}
