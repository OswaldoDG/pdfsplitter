using PdfInspector.Application.CasosUso.Auth;
using PdfInspector.Domain.Abstractions.Bitacora;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Domain.Models.Pdf;
using System;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class SiguientePendienteCasoUso
    {
        private readonly IBitacora _bitacora;
        private readonly IPdfService _pdfService;
        private readonly UsuarioSesion _usuarioSesion;
        private readonly RefrescarCasoUso _refrescarSesion;

        public SiguientePendienteCasoUso(IBitacora bitacora, IPdfService pdfService, UsuarioSesion usuarioSesion, RefrescarCasoUso refrescarCasoUso)
        {
            _bitacora = bitacora;
            _pdfService = pdfService;
            _usuarioSesion = usuarioSesion;
            _refrescarSesion = refrescarCasoUso;
        }

        public async Task<DtoArchivo> SiguientePendiente()
        {
            _bitacora.LogInfo("Inicio caso de uso SiguientePendiente");

            bool sesionValida = await _refrescarSesion.EjecutarSiNecesarioAsync();
            if (!sesionValida)
            {
                _bitacora.LogError("Sesión expirada", new UnauthorizedAccessException());
                throw new UnauthorizedAccessException("Sesión expirada. Por favor, inicie sesión de nuevo.");
            }

            var respuesta = await _pdfService.SiguientePendiente();

            if (!respuesta.Ok)
            {
                _bitacora.LogError(
                    $"Error en API PDF. HttpCode={respuesta.HttpCode}. Mensaje={respuesta.Error?.Mensaje}",
                    new Exception(respuesta.Error?.Mensaje)
                );
                throw new Exception("No fue posible obtener el PDF pendiente");
            }

            if (respuesta.Payload == null)
            {
                _bitacora.LogInfo("No hay PDFs pendientes");
                return null;
            }

            _bitacora.LogInfo($"PDF obtenido correctamente. Id={respuesta.Payload.Id}");

            return respuesta.Payload;
        }
    }
}
