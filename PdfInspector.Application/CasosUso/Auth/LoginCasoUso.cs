using PdfInspector.App.DTOs.Auth;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Abstractions.Bitacora;
using PdfInspector.Domain.Models.Auth;
using System;
using System.Threading.Tasks;

namespace PdfInspector.App.CasosUso.Auth
{
    public class LoginCasoUso
    {
        private readonly IBitacora _bitacora;
        private readonly IAuthService _authService;
        private readonly UsuarioSesion _sesion;

        public LoginCasoUso(IBitacora bitacora, IAuthService authService, UsuarioSesion sesion)
        {
            _bitacora = bitacora;
            _authService = authService;
            _sesion = sesion;
        }

        public async Task<string> Ejecutar(string username, string password)
        {
            _bitacora.LogInfo("Inicio Caso de Uso Login");

            var respuesta = await _authService.LoginAsync(username, password);

            if (!respuesta.Ok)
            {
                _bitacora.LogError(
                    $"Error en login. HttpCode={respuesta.HttpCode}. Mensaje={respuesta.Error?.Mensaje}",
                    new Exception(respuesta.Error?.Mensaje)
                );
                return null;
            }

            if (respuesta.Payload == null)
            {
                _bitacora.LogInfo("Login sin token (credenciales inválidas)");
                return null;
            }

            _sesion.Create(
                respuesta.Payload.access_token,
                respuesta.Payload.refresh_token,
                respuesta.Payload.expires_in
            );

            _bitacora.LogInfo("Login exitoso");
            return respuesta.Payload.access_token;
        }
    }
}