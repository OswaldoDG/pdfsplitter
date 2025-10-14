using PdfInspector.Domain.Models.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Domain.Abstractions.Pdf
{
    public interface IPdfService
    {
        Task<string> DescargarPdfPorId(int id);
        Task<bool> Siguiente();

        Task<bool> FinalizarPorId(int id, DtoFinalizar dtoFinalizar);
    }
}
