namespace PdfInspector.CargaArchivos.models
{
    public enum EstadoRevision
    {
        Pendiente,
        Procesando,
        Completado,
        Error
    }

    public class ArchivoPdf
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required string Ruta { get; set; }
        public EstadoRevision Estado { get; set; } = EstadoRevision.Pendiente;
        public DateTime? UltimaRevision { get; set; }
        public int TotalPaginas { get; set; } = 0;
        public int Prioridad { get; set; }
    }
}
