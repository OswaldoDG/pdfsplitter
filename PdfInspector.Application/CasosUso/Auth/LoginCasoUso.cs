using PdfInspector.App.DTOs.Auth;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Models.Auth;
using System.Threading.Tasks;

namespace PdfInspector.App.CasosUso.Auth
{
    public  class LoginCasoUso
    {
        private readonly IAuthService _loginService;

        public LoginCasoUso(IAuthService loginService)
        {
            _loginService = loginService;
        }

        public async Task<TokenConnect> ExecuteAsync(LoginRequest request)
        {
            return await _loginService.LoginAsync(request.Username, request.Password);
        }
    }
}