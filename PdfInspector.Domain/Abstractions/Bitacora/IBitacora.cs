using System;

namespace PdfInspector.Domain.Abstractions.Bitacora
{
    /// <summary>
    /// Bitacora de todos los errores que lanza la aplicación.
    /// </summary>
    public interface IBitacora
    {
        void LogError(Exception exception);
    }
}
