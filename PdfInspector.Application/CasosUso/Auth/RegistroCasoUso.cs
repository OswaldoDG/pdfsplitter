using PdfInspector.App.DTOs.Auth;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Abstractions.Bitacora;
using System;
using System.Threading.Tasks;

namespace PdfInspector.App.CasosUso.Auth
{
    public  class RegistroCasoUso
    {
        private readonly IBitacora _bitacora;
        private readonly IAuthService _registroService;

        public RegistroCasoUso(IBitacora bitacora,IAuthService registroService)
        {
            _bitacora = bitacora;
            _registroService = registroService;
        }

        public async Task<bool?> ExecuteAsync(RegistroRequest request)
        {
            _bitacora.LogInfo("Inicio Caso de Uso Registro");

            var respuesta = await _registroService.RegistroAsync(
                request.Email,
                request.Password,
                request.Code
            );

            if (!respuesta.Ok)
            {
                _bitacora.LogError(
                    $"Error en registro. HttpCode={respuesta.HttpCode}. Mensaje={respuesta.Error?.Mensaje}",
                    new Exception(respuesta.Error?.Mensaje)
                );
                return false;
            }

            _bitacora.LogInfo("Registro exitoso");
            return respuesta.Resultado;
        }
    }
}
