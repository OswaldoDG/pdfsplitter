using System;

namespace PdfInspector.Domain.Models.Auth
{
    public class UsuarioSesion
    {
        public string Token { get; private set; }
        public DateTime Expiration { get; private set; }

        public bool IsAuthenticated => !string.IsNullOrEmpty(Token) && DateTime.UtcNow < Expiration;

        public void Create(string token, int expiresInSeconds)
        {
            Token = token;
            Expiration = DateTime.UtcNow.AddSeconds(expiresInSeconds - 60);
        }

        public void Clear()
        {
            Token = null;
            Expiration = DateTime.MinValue;
        }
    }
}
