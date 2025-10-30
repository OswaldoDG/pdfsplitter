using Newtonsoft.Json;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Domain.Models.Pdf;
using PdfInspector.Infraestructure.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<DtoArchivo> SiguientePorId(int id)
        {
            var endpoint = _config.PdfApi.DescargarPorId.Replace("{id}", id.ToString());
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint));

            if (_sesion.IsAuthenticated)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.Token);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DtoArchivo>(jsonResponse);
        }

        public async Task<List<DtoTipoDoc>> ObtieneTipoDocumentosAsync()
        {
            var endpoint = _config.PdfApi.ObtieneTipoDocumentos;
            var fullUrl = new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint);
            var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);

            if (_sesion.IsAuthenticated)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.Token);
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DtoTipoDoc>>(jsonResponse) ?? new List<DtoTipoDoc>();
        }

        public async Task<bool> FinalizarPorIdAsync(int id, DtoFinalizar dto)
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

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al finalizar PDF (ID: {id}): {errorMessage}");
            }

            return response.IsSuccessStatusCode;
        }

        public async Task<DtoArchivo> SiguientePendiente()
        {
            var endpoint = _config.PdfApi.Siguiente;
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint));

            if (_sesion.IsAuthenticated)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _sesion.Token);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DtoArchivo>(jsonResponse);
        }
    }
}
