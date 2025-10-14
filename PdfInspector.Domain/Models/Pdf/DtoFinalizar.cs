using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Domain.Models.Pdf
{
    /// <summary>
    /// Dto de finalizacion de separacion.
    /// </summary>
    public class DtoFinalizar
    {
        /// <summary>
        /// Lista de partes documentales que componen el PDF.
        /// </summary>
        public List<ParteDocumental> Partes { get; set; }

        /// <summary>
        /// Total de paginas del PDF que se esta separando.
        /// </summary>
        public int TotalPaginas { get; set; }
    }
}
