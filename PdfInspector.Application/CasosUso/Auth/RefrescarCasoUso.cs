using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Abstractions.Bitacora;
using PdfInspector.Domain.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Auth
{
    public class RefrescarCasoUso
    {
        private readonly IBitacora _bitacora;
        private readonly IAuthService _authService;
        private readonly UsuarioSesion _usuarioSesion;
        private static readonly object _refreshLock = new object();
        private static bool _isRefreshing = false;

        public RefrescarCasoUso(IBitacora bitacora,IAuthService authService, UsuarioSesion usuarioSesion)
        {
            _bitacora = bitacora;
            _authService = authService;
            _usuarioSesion = usuarioSesion;
        }

        public async Task<bool> EjecutarSiNecesarioAsync()
        {
            if (_usuarioSesion.IsAuthenticated && !_usuarioSesion.NeedsRefresh())
                return true;

            if (string.IsNullOrEmpty(_usuarioSesion.RefreshToken))
            {
                _usuarioSesion.Clear();
                return false;
            }

            lock (_refreshLock)
            {
                if (_isRefreshing) return true;
                _isRefreshing = true;
            }

            try
            {
                var respuesta = await _authService.RefreshTokenAsync(_usuarioSesion.RefreshToken);

                if (!respuesta.Ok || respuesta.Payload == null)
                {
                    _usuarioSesion.Clear();
                    return false;
                }

                _usuarioSesion.Create(
                    respuesta.Payload.access_token,
                    respuesta.Payload.refresh_token,
                    respuesta.Payload.expires_in
                );

                return true;
            }
            finally
            {
                _isRefreshing = false;
            }
        }
    }
}
