using PdfInspector.App.DTOs.Auth;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Models.Auth;
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

        public async Task<bool> Ejecutar(string email, string password)
        {
            var tokenData = await _authService.LoginAsync(email, password);

            if (tokenData != null && !string.IsNullOrEmpty(tokenData.access_token))
            {
                _sesion.Create(tokenData.access_token, tokenData.expires_in);
                return true;
            }

            _sesion.Clear();
            return false;
        }
    }
}