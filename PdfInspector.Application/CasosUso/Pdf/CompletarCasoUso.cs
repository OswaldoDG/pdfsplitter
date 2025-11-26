using PdfInspector.Application.CasosUso.Auth;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Domain.Models.Pdf;
using System;
using System.Threading.Tasks;

namespace PdfInspector.Application.CasosUso.Pdf
{
    public class CompletarCasoUso
    {
        private readonly IPdfService _pdfService;
        private readonly UsuarioSesion _usuarioSesion;
        private readonly RefrescarCasoUso _refrescarSesion;

        public CompletarCasoUso(IPdfService pdfService, UsuarioSesion usuarioSesion, RefrescarCasoUso refrescarCasoUso) 
        {
            _pdfService = pdfService;
            _usuarioSesion = usuarioSesion;
            _refrescarSesion = refrescarCasoUso;
        }

        public async Task<bool> ExecuteAsync(int id, DtoFinalizar dtoFinalizar)
        {
            bool sesionValida = await _refrescarSesion.EjecutarSiNecesarioAsync();
            if (!sesionValida)
            {
                throw new UnauthorizedAccessException("Sesión expirada. Por favor, inicie sesión de nuevo.");
            }
            return await _pdfService.FinalizarPorIdAsync(id, dtoFinalizar);
        }
    }
}
