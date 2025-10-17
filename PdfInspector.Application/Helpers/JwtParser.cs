using System;
using Newtonsoft.Json.Linq;

namespace PdfInspector.Application.Helpers
{
    public static class JwtParser
    {
        public static DateTime ParseExpiration(string token)
        {
            try
            {
                string payloadJson = DecodePayload(token);
                JObject payload = JObject.Parse(payloadJson);

                JToken expClaim = payload["exp"];
                if (expClaim != null && expClaim.Type == JTokenType.Integer)
                {
                    long expSeconds = expClaim.Value<long>();
                    return DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime;
                }

                return DateTime.UtcNow.AddDays(-1);
            }
            catch
            {
                return DateTime.UtcNow.AddDays(-1);
            }
        }

        private static string DecodePayload(string token)
        {
            var parts = token.Split('.');
            if (parts.Length < 2) throw new ArgumentException("Token JWT inválido.");

            var payload = parts[1];
            var payloadBytes = Convert.FromBase64String(PadBase64(payload));
            return System.Text.Encoding.UTF8.GetString(payloadBytes);
        }

        private static string PadBase64(string base64)
        {
            int padding = base64.Length % 4;
            if (padding > 0)
            {
                base64 += new string('=', 4 - padding);
            }
            return base64;
        }
    }
}

