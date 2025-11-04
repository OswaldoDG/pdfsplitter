using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Infraestructure.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PdfInspector.Infraestructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly EndpointConfig _config;
        private readonly HttpClient _httpClient;
        public AuthService(EndpointConfig config)
        {
            _config = config;
            _httpClient = new HttpClient { BaseAddress = new Uri(_config.AuthApi.BaseUrl) };
        }

        public async Task<TokenConnect> LoginAsync(string username, string password)
        {
            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string,string>("client_id","mensajeriamedica-password"),
                new KeyValuePair<string,string>("username", username),
                new KeyValuePair<string,string>("password", password),
                new KeyValuePair<string,string>("scope","offline_access")
            });

            var respuesta = await _httpClient.PostAsync(_config.AuthApi.Login, form);
            respuesta.EnsureSuccessStatusCode();

            var json = JObject.Parse(await respuesta.Content.ReadAsStringAsync());
            return new TokenConnect
            {
                access_token = json["access_token"].ToString(),
                refresh_token = json["refresh_token"].ToString(),
                expires_in = (int)json["expires_in"],
                token_type = json["token_type"].ToString()
            };
        }

        public async Task<bool> RegistroAsync(string email, string password, string code)
        {
            var obj = new { Email = email, Password = password, Code = code };
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuesta = await _httpClient.PostAsync(_config.AuthApi.Register, content);
            return respuesta.IsSuccessStatusCode;
        }
    }
}
