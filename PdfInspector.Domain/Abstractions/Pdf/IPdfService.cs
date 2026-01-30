using PdfInspector.Domain.Comunes;
using PdfInspector.Domain.Models.Pdf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PdfInspector.Domain.Abstractions.Pdf
{
    public interface IPdfService
    {

        Task<RespuestaBoolean> FinalizarPorIdAsync(int id, DtoFinalizar dtoFinalizar);

        Task<RespuestaPayload<List<DtoTipoDoc>>> ObtieneTipoDocumentosAsync();

        Task<RespuestaPayload<DtoArchivo>> SiguientePendiente();

        Task<RespuestaPayload<List<DtoEstadisticasUsuario>>> EstadisticasUsuarioAsync();
    }
}
