using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Domain.Models.Auth
{
    public class UsuarioSesion
    {
        public string AccesToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        
        public void SetExpirationFromExpireIn(int expiresIn)
        {
            Expiration = DateTime.UtcNow.AddSeconds(expiresIn);
        }

        public bool TokenValido()
        {
            return DateTime.UtcNow < Expiration.AddMinutes(-2);
        }

        public void Clear()
        {
            AccesToken = null;
            RefreshToken = null;
            Expiration = DateTime.MinValue;
        }
    }
}
