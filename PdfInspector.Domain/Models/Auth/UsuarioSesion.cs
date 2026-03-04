using System;

namespace PdfInspector.Domain.Models.Auth
{
    public class UsuarioSesion
    {
        public const int MARGEN_RELOJ = 5;
        public string Token { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime Expiration { get; private set; }

        public bool IsAuthenticated => !string.IsNullOrEmpty(Token) && DateTime.UtcNow < Expiration;

        public void Create(string token, string refreshToken, int expiresInSeconds)
        {
            Token = token;
            RefreshToken = refreshToken;
            Expiration = DateTime.UtcNow.AddSeconds(
                        Math.Max(0, expiresInSeconds - MARGEN_RELOJ));
        }

        public bool NeedsRefresh()
        {
            if (string.IsNullOrEmpty(RefreshToken)) return false;
            return DateTime.UtcNow >= Expiration.AddSeconds(-MARGEN_RELOJ);
        }

        public void Clear()
        {
            Token = null;
            RefreshToken = null;
            Expiration = DateTime.MinValue;
        }


    }
}
