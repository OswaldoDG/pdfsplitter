using PdfInspector.Domain.Models.Pdf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PdfInspector.Domain.Abstractions.Pdf
{
    public interface IPdfService
    {

        Task<bool> FinalizarPorIdAsync(int id, DtoFinalizar dtoFinalizar);

        Task<List<DtoTipoDoc>> ObtieneTipoDocumentosAsync();

        Task<DtoArchivo> SiguientePendiente();

        Task<List<DtoEstadisticasUsuario>> EstadisticasUsuarioAsync();
    }
}
