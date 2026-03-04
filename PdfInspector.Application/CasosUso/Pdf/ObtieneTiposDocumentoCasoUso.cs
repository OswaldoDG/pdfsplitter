using PdfInspector.Application.CasosUso.Auth;
using PdfInspector.Domain.Abstractions.Bitacora;
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
        private readonly IBitacora _bitacora;
        private readonly IPdfService _pdfService;

        public ObtieneTipoDocumentosPdfCasoUso(IBitacora bitacora, IPdfService pdfService)
        {
            _bitacora = bitacora;
            _pdfService = pdfService;
        }

        public async Task<List<DtoTipoDoc>> ExecuteAsync()
        {
            _bitacora.LogInfo("Inicio caso de uso ObtieneTipoDocumentosPdfCasoUso");

            var respuesta =  await _pdfService.ObtieneTipoDocumentosAsync();

            if (!respuesta.Ok)
            {
                _bitacora.LogError(
                    $"Error en API PDF. HttpCode={respuesta.HttpCode}. Mensaje={respuesta.Error?.Mensaje}",
                    new Exception(respuesta.Error?.Mensaje)
                );
                throw new Exception("No fue posible obtener el los tipos de documento");
            }

            if (respuesta.Payload == null)
            {
                _bitacora.LogInfo("No hay tipos de documento en la base de datos");
                return null;
            }

            _bitacora.LogInfo($"Tipos de documentos obtenidos correctamente.");

            return respuesta.Payload;
        }

    }
}
