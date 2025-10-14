using Newtonsoft.Json;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Pdf;
using PdfInspector.Infraestructure.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Infraestructure.Services.Pdf
{
    public class PdfService : IPdfService
    {
        private readonly HttpClient _httpClient;
        private readonly EndpointConfig _config;

        public PdfService(EndpointConfig config, string token)
        {
            _config = config;
            _httpClient = new HttpClient { BaseAddress = new Uri(_config.AuthApi.BaseUrl) };
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer");
        }

        public async Task<string> DescargarPdfPorId(int id)
        {
            var endpoint = _config.PdfApi.DescargarPorId.Replace("{id}", id.ToString());
            var url = new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint).ToString();
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                throw new Exception("No se pudo descargar el PDF");
            var bytes = await response.Content.ReadAsByteArrayAsync();

            var fileName = $"archivo_{id}.pdf";
            if (response.Content.Headers.ContentDisposition != null)
            {
                fileName = response.Content.Headers.ContentDisposition.FileName?.Trim('\"') ?? fileName;
            }

            var carpetaDestino = @"C:\medica\repositorio";
            if (!Directory.Exists(carpetaDestino))
                Directory.CreateDirectory(carpetaDestino);

            var ruta = Path.Combine(carpetaDestino, fileName);
            if(!Directory.Exists(ruta))
                File.WriteAllBytes(ruta, bytes);

            return ruta;
        }

        public async Task<bool> Siguiente()
        {
            var endpoint = _config.PdfApi.Siguiente;
            var url = new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint).ToString();
            var response = await _httpClient.GetAsync(url);

            return response.IsSuccessStatusCode;

        }

        public async Task<bool> FinalizarPorId(int id, DtoFinalizar dto)
        {
            var endpoint = _config.PdfApi.FinalizarPorId.Replace("{id}", id.ToString());
            var url = new Uri(new Uri(_config.PdfApi.BaseUrl), endpoint).ToString();

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            return response.IsSuccessStatusCode;
        }

    }
}
