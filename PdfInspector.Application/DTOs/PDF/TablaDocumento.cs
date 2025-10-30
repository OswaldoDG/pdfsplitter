using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Application.DTOs.PDF
{
    /// <summary>
    /// Representa cada parte en la ListView.
    /// </summary>
    public class TablaDocumento
    {
        public int Id {  get; set; }
        public string TipoDocumento { get; set; }
        public int PagInicio { get; set; }
        public int PagFinal { get; set;}
    }
}
