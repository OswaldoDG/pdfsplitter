using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class CompletarCasoUso
    {
        private readonly IPdfService _pdfService;

        public CompletarCasoUso(IPdfService pdfService) 
        {
            _pdfService = pdfService;
        }

        public async Task<bool> ExecuteAsync(int id, DtoFinalizar dtoFinalizar)
        {
            return await _pdfService.FinalizarPorIdAsync(id, dtoFinalizar);
        }
    }
}
