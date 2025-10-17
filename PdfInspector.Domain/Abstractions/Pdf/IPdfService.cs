using PdfInspector.Domain.Models.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Domain.Abstractions.Pdf
{
    public interface IPdfService
    {
        Task<byte[]> DescargarPdfPorIdAsync(int id);
        Task<bool> SiguienteAsync();

        Task<bool> FinalizarPorIdAsync(int id, DtoFinalizar dtoFinalizar);

        Task<List<DtoTipoDoc>> ObtieneTipoDocumentosAsync(List<int> ids);
    }
}
