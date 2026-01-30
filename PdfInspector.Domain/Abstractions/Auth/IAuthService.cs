using PdfInspector.Domain.Comunes;
using PdfInspector.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Domain.Abstractions.Auth
{
    public  interface IAuthService
    {
        Task<RespuestaPayload<TokenConnect>> LoginAsync(string username, string password);
        Task<RespuestaPayload<TokenConnect>> RefreshTokenAsync(string refreshToken);
        Task<RespuestaBoolean> RegistroAsync(string email, string password, string code);
    }
}
