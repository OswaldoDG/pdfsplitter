using PdfInspector.App.DTOs.Auth;
using PdfInspector.Domain.Abstractions.Auth;
using System.Threading.Tasks;

namespace PdfInspector.App.CasosUso.Auth
{
    public  class RegistroCasoUso
    {
        private readonly IAuthService _registroService;

        public RegistroCasoUso(IAuthService registroService)
        {
            _registroService = registroService;
        }

        public async Task<bool?> ExecuteAsync(RegistroRequest request)
        {
            return await _registroService.RegistroAsync(request.Email, request.Password);
        }
    }
}
