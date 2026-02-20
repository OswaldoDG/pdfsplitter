using PdfInspector.Application.CasosUso.Auth;
using PdfInspector.Domain.Abstractions.Bitacora;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Comunes;
using PdfInspector.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class ValidacionArchivoIdCasoUso
    {
        private readonly IBitacora _bitacora;
        private readonly IPdfService _pdfService;
        private readonly UsuarioSesion _usuarioSesion;
        private readonly RefrescarCasoUso _refrescarCasoUso;

        public ValidacionArchivoIdCasoUso(IBitacora bitacora, IPdfService pdfService, UsuarioSesion usuarioSesion, RefrescarCasoUso refrescarCasoUso) 
        {
            _bitacora = bitacora;
            _pdfService = pdfService;
            _usuarioSesion = usuarioSesion;
            _refrescarCasoUso = refrescarCasoUso;
        }

        public async Task<ResultadoValidacion> ExecuteAsync(int archivoId)
        {
            _bitacora.LogInfo($"Inicio caso de uso ValidacionArchivoId para ID: {archivoId}");

            bool sesionValida = await _refrescarCasoUso.EjecutarSiNecesarioAsync();

            var respuesta = await _pdfService.ValidacionAsignacionAsync(archivoId);

            if (respuesta.Ok)
            {
                return ResultadoValidacion.Exito;
            }

            if (respuesta.Error?.HttpCode == HttpStatusCode.Conflict)
            {
                _bitacora.LogError($"Conflicto de concurrencia al validar PDF ID = {archivoId}. Se requiere reintento.");
                return ResultadoValidacion.ConflictoConcurrencia;
            }

            _bitacora.LogError(
                $"Error al validar asignación del PDF ID = {archivoId}. HttpCode={respuesta.Error?.HttpCode}. Mensaje={respuesta.Error?.Mensaje}",
                new Exception(respuesta.Error?.Mensaje)
            );

            return ResultadoValidacion.ErrorCritico;
        }
    }
}
