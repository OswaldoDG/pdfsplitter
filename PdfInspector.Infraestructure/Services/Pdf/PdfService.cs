using Newtonsoft.Json;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Comunes;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Domain.Models.Pdf;
using PdfInspector.Infraestructure.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PdfInspector.Infraestructure.Services.Pdf
{
    public class PdfService : IPdfService
    {
        private readonly HttpClient _httpClient;
        private readonly EndpointConfig _config;
        private readonly UsuarioSesion _sesion;

        public PdfService(EndpointConfig config, HttpClient httpClient, UsuarioSesion sesion)
        {
            _config = config;
            _httpClient = httpClient;
            _sesion = sesion;
        }

        public async Task<RespuestaPayload<DtoArchivo>> SiguientePorId(int id)
        {
            RespuestaPayload<DtoArchivo> respuestaPayload = new RespuestaPayload<DtoArchivo>();
            try
            {
                var endpoint = _config.PdfApi.DescargarPorId.Replace("{id}", id.ToString());
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint));

                if (_sesion.IsAuthenticated)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.Token);
                }

                var response = await _httpClient.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    respuestaPayload.Payload = null;
                    respuestaPayload.HttpCode = HttpStatusCode.OK;
                    return respuestaPayload;
                }

                if (!response.IsSuccessStatusCode)
                {
                    respuestaPayload.Error = new ErrorProceso
                    {
                        Mensaje = $"Error HTTP {response.StatusCode}",
                        HttpCode = response.StatusCode
                    };
                    return respuestaPayload;
                }

                var dto = JsonConvert.DeserializeObject<DtoArchivo>(body);
                respuestaPayload.Payload = dto;

                return respuestaPayload;
            }
            catch (Exception ex)
            {
                respuestaPayload.Error = new ErrorProceso
                {
                    Mensaje = ex.ToString(),
                    HttpCode = HttpStatusCode.InternalServerError
                };
                return respuestaPayload;
            }
            
        }

        public async Task<RespuestaPayload<List<DtoTipoDoc>>> ObtieneTipoDocumentosAsync()
        {
            RespuestaPayload<List<DtoTipoDoc>> respuestaPayload = new RespuestaPayload<List<DtoTipoDoc>>();
            try
            {
                var endpoint = _config.PdfApi.ObtieneTipoDocumentos;
                var fullUrl = new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint);
                var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);

                if (_sesion.IsAuthenticated)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.Token);
                }

                var response = await _httpClient.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    respuestaPayload.Payload = null;
                    respuestaPayload.HttpCode = HttpStatusCode.OK;
                    return respuestaPayload;
                }

                if (!response.IsSuccessStatusCode)
                {
                    respuestaPayload.Error = new ErrorProceso
                    {
                        Mensaje = $"Error HTTP {response.StatusCode}",
                        HttpCode = response.StatusCode
                    };
                    return respuestaPayload;
                }

                var dto = JsonConvert.DeserializeObject<List<DtoTipoDoc>>(body);
                respuestaPayload.Payload = dto;

                return respuestaPayload;
            }
            catch (Exception ex)
            {
                respuestaPayload.Error = new ErrorProceso
                {
                    Mensaje = ex.ToString(),
                    HttpCode = HttpStatusCode.InternalServerError
                };
                return respuestaPayload;
            }
        }

        public async Task<RespuestaBoolean> FinalizarPorIdAsync(int id, DtoFinalizar dto)
        {
            RespuestaBoolean respuestaBoolean = new RespuestaBoolean();
            try
            {
                var endpoint = _config.PdfApi.FinalizarPorId.Replace("{id}", id.ToString());
                var requestUri = new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint);

                var request = new HttpRequestMessage(HttpMethod.Post, requestUri);

                if (_sesion.IsAuthenticated)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.Token);
                }

                var json = JsonConvert.SerializeObject(dto);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    respuestaBoolean.Resultado = true;
                    respuestaBoolean.Ok = true;
                    respuestaBoolean.HttpCode = HttpStatusCode.OK;
                }
                else
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    respuestaBoolean.Resultado = false;
                    respuestaBoolean.Error = new ErrorProceso
                    {
                        HttpCode = response.StatusCode,
                        Mensaje = $"Error al finalizar PDF (ID: {id}). Servidor respondió: {response.StatusCode} - {errorBody}"
                    };
                }
            }
            catch(Exception ex)
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

        public async Task<RespuestaPayload<DtoArchivo>> SiguientePendiente()
        {
            RespuestaPayload<DtoArchivo> respuestaPayload = new RespuestaPayload<DtoArchivo>();
            try
            {
                var endpoint = _config.PdfApi.Siguiente;
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint));

                if (_sesion.IsAuthenticated)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.Token);
                }

                var response = await _httpClient.SendAsync(request);

                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    respuestaPayload.Payload = null;
                    respuestaPayload.HttpCode = HttpStatusCode.OK;
                    return respuestaPayload;
                }

                if (!response.IsSuccessStatusCode)
                {
                    respuestaPayload.Error = new ErrorProceso
                    {
                        Mensaje = $"Error HTTP {response.StatusCode}",
                        HttpCode = response.StatusCode
                    };
                    return respuestaPayload;
                }

                var dto = JsonConvert.DeserializeObject<DtoArchivo>(body);
                respuestaPayload.Payload = dto;

                return respuestaPayload;
            }
            catch (Exception ex)
            {
                respuestaPayload.Error = new ErrorProceso
                {
                    Mensaje = ex.ToString(),
                    HttpCode = HttpStatusCode.InternalServerError
                };
                return respuestaPayload;
            }
        }

        public async Task<RespuestaPayload<List<DtoEstadisticasUsuario>>> EstadisticasUsuarioAsync()
        {
            RespuestaPayload<List<DtoEstadisticasUsuario>> respuestaPayload = new RespuestaPayload<List<DtoEstadisticasUsuario>>();
            try
            {
                var endpoint = _config.PdfApi.MisEstadisticas;
                var request = new HttpRequestMessage(HttpMethod.Get, new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint));

                if (_sesion.IsAuthenticated)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.Token);
                }

                var response = await _httpClient.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    respuestaPayload.Payload = null;
                    respuestaPayload.HttpCode = HttpStatusCode.OK;
                    return respuestaPayload;
                }

                if (!response.IsSuccessStatusCode)
                {
                    respuestaPayload.Error = new ErrorProceso
                    {
                        Mensaje = $"Error HTTP {response.StatusCode}",
                        HttpCode = response.StatusCode
                    };
                    return respuestaPayload;
                }

                var dto = JsonConvert.DeserializeObject<List<DtoEstadisticasUsuario>>(body);
                respuestaPayload.Payload = dto;

                return respuestaPayload;
            }
            catch (Exception ex)
            {
                respuestaPayload.Error = new ErrorProceso
                {
                    Mensaje = ex.ToString(),
                    HttpCode = HttpStatusCode.InternalServerError
                };
                return respuestaPayload;
            }
        }
    }
}
