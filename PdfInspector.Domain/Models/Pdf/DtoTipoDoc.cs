namespace PdfInspector.Domain.Models.Pdf
{
    /// <summary>
    /// Dto para el frontend que representa un archivo PDF.
    /// </summary>
    public class DtoTipoDoc
    {
        /// <summary>
        /// IDentificador unico del archivo.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del archivo.
        /// </summary>
        public string Nombre { get; set; }
    }
}