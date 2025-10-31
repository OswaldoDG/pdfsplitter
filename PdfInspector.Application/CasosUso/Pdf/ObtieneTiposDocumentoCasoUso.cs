using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class ObtieneTipoDocumentosPdfCasoUso
    {
        private readonly IPdfService _pdfService;

        public ObtieneTipoDocumentosPdfCasoUso(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        public async Task<List<DtoTipoDoc>> ExecuteAsync()
        {
            return await _pdfService.ObtieneTipoDocumentosAsync();
        }

    }
}
