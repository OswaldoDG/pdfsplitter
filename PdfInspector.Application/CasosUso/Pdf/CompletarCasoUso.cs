using PdfInspector.Application.CasosUso.Auth;
using PdfInspector.Domain.Abstractions.Bitacora;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Domain.Models.Pdf;
using System;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class CompletarCasoUso
    {
        private readonly IBitacora _bitacora;
        private readonly IPdfService _pdfService;
        private readonly UsuarioSesion _usuarioSesion;
        private readonly RefrescarCasoUso _refrescarSesion;

        public CompletarCasoUso(IBitacora bitacora, IPdfService pdfService, UsuarioSesion usuarioSesion, RefrescarCasoUso refrescarCasoUso) 
        {
            _bitacora = bitacora;
            _pdfService = pdfService;
            _usuarioSesion = usuarioSesion;
            _refrescarSesion = refrescarCasoUso;
        }

        public async Task<bool> ExecuteAsync(int id, DtoFinalizar dtoFinalizar)
        {
            _bitacora.LogInfo("Inicio caso de uso CompletarCasoUso");
            bool sesionValida = await _refrescarSesion.EjecutarSiNecesarioAsync();

            if (!sesionValida)
            {
                _bitacora.LogError("Sesión expirada al completar PDF", new UnauthorizedAccessException());
                throw new UnauthorizedAccessException("Sesión expirada. Por favor, inicie sesión de nuevo.");
            }

            var respuesta = await _pdfService.FinalizarPorIdAsync(id, dtoFinalizar);

            if (!respuesta.Ok)
            {
                _bitacora.LogError(
                    $"Error al completar PDF ID={id}. HttpCode={respuesta.HttpCode}. Mensaje={respuesta.Error?.Mensaje}",
                    new Exception(respuesta.Error?.Mensaje)
                );
                return false;
            }

            return respuesta.Resultado;
        }
    }
}
