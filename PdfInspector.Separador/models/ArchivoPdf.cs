using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Separador.models
{
    public class ArchivoPdf
    {
        public int Id { get; set; }

        required public string Nombre { get; set; }

        required public string Ruta { get; set; }

        public EstadoRevision Estado { get; set; }

        public string? IdProceso { get; set; }
    }
}
