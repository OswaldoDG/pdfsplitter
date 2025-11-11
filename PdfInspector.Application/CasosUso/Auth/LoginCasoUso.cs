using PdfInspector.App.DTOs.Auth;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Models.Auth;
using System;
using System.Threading.Tasks;

namespace PdfInspector.App.CasosUso.Auth
{
    public class LoginCasoUso
    {
        private readonly IAuthService _authService;
        private readonly UsuarioSesion _sesion;

        public LoginCasoUso(IAuthService authService, UsuarioSesion sesion)
        {
            _authService = authService;
            _sesion = sesion;
        }

        public async Task<string> Ejecutar(string username, string password)
        {
            try
            {
                var tokenConnect = await _authService.LoginAsync(username, password);
                if (tokenConnect != null)
                {
                    _sesion.Create(
                        tokenConnect.access_token,
                        tokenConnect.refresh_token,
                        tokenConnect.expires_in
                    );
                    return tokenConnect.access_token;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}