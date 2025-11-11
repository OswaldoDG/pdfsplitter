using PdfInspector.Domain.Abstractions.Auth;
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
        private readonly IAuthService _authService;
        private readonly UsuarioSesion _usuarioSesion;
        private static readonly object _refreshLock = new object();
        private static bool _isRefreshing = false;

        public RefrescarCasoUso(IAuthService authService, UsuarioSesion usuarioSesion)
        {
            _authService = authService;
            _usuarioSesion = usuarioSesion;
        }

        public async Task<bool> EjecutarSiNecesarioAsync()
        {
            if (_usuarioSesion.IsAuthenticated && !_usuarioSesion.NeedsRefresh())
            {
                return true;
            }

            if (!string.IsNullOrEmpty(_usuarioSesion.RefreshToken))
            {
                lock (_refreshLock)
                {
                    if (_isRefreshing) return true;
                    _isRefreshing = true;
                }

                try
                {
                    var tokenConnect = await _authService.RefreshTokenAsync(_usuarioSesion.RefreshToken);
                    if (tokenConnect != null)
                    {
                        _usuarioSesion.Create(
                            tokenConnect.access_token,
                            tokenConnect.refresh_token,
                            tokenConnect.expires_in
                        );
                        return true;
                    }
                }
                catch (Exception)
                {

                }
                finally
                {
                    _isRefreshing = false;
                }
            }

            _usuarioSesion.Clear();
            return false;
        }
    }
}
