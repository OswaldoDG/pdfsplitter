using PdfInspector.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Auth
{
    public class AuthenticatedHttpHandler : DelegatingHandler
    {
        private readonly RefrescarCasoUso _refrescarCasoUso;
        private readonly UsuarioSesion _usuarioSesion;

        public AuthenticatedHttpHandler(RefrescarCasoUso refrescarCasoUso, UsuarioSesion usuarioSesion)
        {
            _refrescarCasoUso = refrescarCasoUso;
            _usuarioSesion = usuarioSesion;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool ok = await _refrescarCasoUso.EjecutarSiNecesarioAsync();
            if (!ok)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    ReasonPhrase = "No se pudo refrescar el token"
                };
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _usuarioSesion.Token);

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (await _refrescarCasoUso.EjecutarSiNecesarioAsync())
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _usuarioSesion.Token);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }

            return response;
        }
    }

}
