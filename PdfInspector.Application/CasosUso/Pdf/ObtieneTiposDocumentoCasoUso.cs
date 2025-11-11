using PdfInspector.Application.CasosUso.Auth;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Domain.Models.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class ObtieneTipoDocumentosPdfCasoUso
    {
        private readonly IPdfService _pdfService;
        private readonly UsuarioSesion _usuarioSesion;
        private readonly RefrescarCasoUso _refrescarSesion;

        public ObtieneTipoDocumentosPdfCasoUso(IPdfService pdfService, UsuarioSesion usuarioSesion, RefrescarCasoUso refrescarCasoUso)
        {
            _pdfService = pdfService;
            _usuarioSesion = usuarioSesion;
            _refrescarSesion = refrescarCasoUso;
        }

        public async Task<List<DtoTipoDoc>> ExecuteAsync()
        {
            bool sesionValida = await _refrescarSesion.EjecutarSiNecesarioAsync();
            if (!sesionValida)
            {
                throw new UnauthorizedAccessException("Sesión expirada. Por favor, inicie sesión de nuevo.");
            }
            return await _pdfService.ObtieneTipoDocumentosAsync();
        }

    }
}
