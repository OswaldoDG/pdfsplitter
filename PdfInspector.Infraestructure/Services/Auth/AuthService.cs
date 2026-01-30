using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Comunes;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Infraestructure.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public async Task<RespuestaPayload<TokenConnect>> LoginAsync(string username, string password)
        {
            RespuestaPayload<TokenConnect> respuestaPayload = new RespuestaPayload<TokenConnect>();
            try
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
                
                if (!respuesta.IsSuccessStatusCode)
                {
                    respuestaPayload.Error = new ErrorProceso
                    {
                        HttpCode = respuesta.StatusCode,
                        Mensaje = $"Login fallido: {respuesta.ReasonPhrase}"
                    };
                    return respuestaPayload;
                }

                var json = JObject.Parse(await respuesta.Content.ReadAsStringAsync());

                respuestaPayload.Payload = new TokenConnect
                {
                    access_token = json["access_token"].ToString(),
                    refresh_token = json["refresh_token"].ToString(),
                    expires_in = (int)json["expires_in"],
                    token_type = json["token_type"].ToString()
                };

            }
            catch (Exception ex)
            {
                respuestaPayload.Error = new ErrorProceso
                {
                    HttpCode = HttpStatusCode.InternalServerError,
                    Mensaje = ex.Message
                };
            }

            return respuestaPayload;
        }

        public async Task<RespuestaPayload<TokenConnect>> RefreshTokenAsync(string refreshToken)
        {
            RespuestaPayload<TokenConnect> respuestaPayload = new RespuestaPayload<TokenConnect>();
            try
            {
                var form = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string,string>("grant_type", "refresh_token"),
                    new KeyValuePair<string,string>("client_id", "mensajeriamedica-password"),
                    new KeyValuePair<string,string>("refresh_token", refreshToken)
                });

                var respuesta = await _httpClient.PostAsync(_config.AuthApi.Login, form);

                if (!respuesta.IsSuccessStatusCode)
                {
                    respuestaPayload.Error = new ErrorProceso
                    {
                        HttpCode = respuesta.StatusCode,
                        Mensaje = $"Refresh token fallido: {respuesta.ReasonPhrase}"
                    };
                    return respuestaPayload;
                }

                var json = JObject.Parse(await respuesta.Content.ReadAsStringAsync());
                respuestaPayload.Payload = new TokenConnect
                {
                    access_token = json["access_token"].ToString(),
                    refresh_token = json["refresh_token"].ToString(),
                    expires_in = (int)json["expires_in"],
                    token_type = json["token_type"].ToString()
                };
            }
            catch (Exception ex)
            {
                respuestaPayload.Error = new ErrorProceso
                {
                    HttpCode = HttpStatusCode.InternalServerError,
                    Mensaje = ex.Message
                };
            }

            return respuestaPayload;
        }

        public async Task<RespuestaBoolean> RegistroAsync(string email, string password, string code)
        {
            RespuestaBoolean respuestaBoolean = new RespuestaBoolean();
            try
            {
                var obj = new { Email = email, Password = password, Code = code };
                var json = JsonConvert.SerializeObject(obj);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = await _httpClient.PostAsync(_config.AuthApi.Register, content);
                if (respuesta.IsSuccessStatusCode)
                {
                    respuestaBoolean.Resultado = true;
                    respuestaBoolean.Ok = true;
                    respuestaBoolean.HttpCode = HttpStatusCode.OK;
                }
                else
                {
                    var errorBody = await respuesta.Content.ReadAsStringAsync();
                    respuestaBoolean.Resultado = false;
                    respuestaBoolean.Error = new ErrorProceso
                    {
                        HttpCode = respuesta.StatusCode,
                        Mensaje = $"Registro fallido: {errorBody}"
                    };
                }
            }
            catch (Exception ex)
            {
                respuestaBoolean.Resultado = false;
                respuestaBoolean.Error = new ErrorProceso
                {
                    HttpCode = HttpStatusCode.InternalServerError,
                    Mensaje = ex.Message
                };
            }

            return respuestaBoolean;
        }
    }
}
