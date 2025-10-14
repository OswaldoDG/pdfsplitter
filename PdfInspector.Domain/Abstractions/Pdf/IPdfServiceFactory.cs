using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Domain.Abstractions.Pdf
{
    public interface IPdfServiceFactory
    {
        IPdfService Create(string token);
    }
}
