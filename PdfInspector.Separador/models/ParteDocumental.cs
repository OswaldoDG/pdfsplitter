using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Separador.models
{
    public class ParteDocumental
    {
        public int Id { get; set; }

        public int ArchivoPdfId { get; set; }

        public int PaginaInicio { get; set; }

        public int PaginaFin { get; set; }

        public int TipoDocumentoId { get; set; }
        /// <summary>
        /// Especifica si el elemento pertenee a un grupo.
        /// </summary>
        public int? IdAgrupamiento { get; set; }
    }
}
