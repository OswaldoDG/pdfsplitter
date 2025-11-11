using System;

namespace PdfInspector.Domain.Models.Auth
{
    public class UsuarioSesion
    {
        public string Token { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime Expiration { get; private set; }

        public bool IsAuthenticated => !string.IsNullOrEmpty(Token) && DateTime.UtcNow < Expiration;

        public void Create(string token, string refreshToken, int expiresInSeconds)
        {
            Token = token;
            RefreshToken = refreshToken;
            Expiration = DateTime.UtcNow.AddSeconds(expiresInSeconds - 60);
        }

        public bool NeedsRefresh()
        {
            if (string.IsNullOrEmpty(RefreshToken)) return false;
            return DateTime.UtcNow >= Expiration.AddMinutes(-5);
        }

        public void Clear()
        {
            Token = null;
            RefreshToken = null;
            Expiration = DateTime.MinValue;
        }


    }
}
