using PdfInspector.Application.CasosUso.Auth;
using PdfInspector.Domain.Abstractions.Bitacora;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Domain.Models.Pdf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class MisEstadisticasCasoUso
    {
        private readonly IBitacora _bitacora;
        private readonly IPdfService _pdfService;
        private readonly UsuarioSesion _usuarioSesion;
        private readonly RefrescarCasoUso _refrescarSesion;

        public MisEstadisticasCasoUso(IBitacora bitacora, IPdfService pdfService, UsuarioSesion usuarioSesion, RefrescarCasoUso refrescarCasoUso)
        {
            _bitacora = bitacora;
            _pdfService = pdfService;
            _usuarioSesion = usuarioSesion;
            _refrescarSesion = refrescarCasoUso;
        }

        public async Task<List<DtoEstadisticasUsuario>> ExecuteAsync() 
        {
            _bitacora.LogInfo("Inicio caso de uso MisEstadisticasCasoUso");
            bool sesionValida = await _refrescarSesion.EjecutarSiNecesarioAsync();
            if (!sesionValida)
            {
                _bitacora.LogError("Sesión expirada", new UnauthorizedAccessException());
                throw new UnauthorizedAccessException("Sesión expirada. Por favor, inicie sesión de nuevo.");
            }

            var respuesta = await _pdfService.EstadisticasUsuarioAsync();

            if (!respuesta.Ok)
            {
                _bitacora.LogError($"Error en API PDF. HttpCode={respuesta.HttpCode}. Mensaje={respuesta.Error?.Mensaje}", new Exception(respuesta.Error?.Mensaje));
                throw new Exception("No fue posible obtener las estadisticas de los usuarios.");
            }

            if (respuesta.Payload == null)
            {
                _bitacora.LogInfo("No hay estadisticas de los capturistas.");
                return null;
            }

            _bitacora.LogInfo($"Estadisticas obtenidas correctamente");

            return respuesta.Payload;

        }
    }
}
