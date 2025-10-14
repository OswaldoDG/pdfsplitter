using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Domain.Models.Pdf
{

    /// <summary>
    /// Representa una parte documental asociada a un PDF.
    /// </summary>
    public class ParteDocumental
    {
        /// <summary>
        /// Identificador unico de la parte documental.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Archivo PDF al que pertenece la parte documental.
        /// </summary>
        public int ArchivoPdfId { get; set; }

        /// <summary>
        /// Numero de pagina en donde comienza la parte deocumental.
        /// </summary>
        public int PaginaInicio { get; set; }


        /// <summary>
        /// Numero de pagina en donde finaliza la parte deocumental.
        /// </summary>
        public int PaginaFin { get; set; }

        /// <summary>
        /// Identificador unico de tipo de documento.
        /// </summary>
        public int TipoDocumentoId { get; set; }
    }

}
