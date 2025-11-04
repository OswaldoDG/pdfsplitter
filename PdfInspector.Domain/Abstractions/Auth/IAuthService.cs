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
        Task<TokenConnect> LoginAsync(string username, string password);
        Task<bool> RegistroAsync(string email, string password, string code);
    }
}
