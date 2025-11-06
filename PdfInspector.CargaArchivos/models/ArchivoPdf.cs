namespace PdfInspector.CargaArchivos.models
{
    public enum EstadoRevision
    {
        Pendiente = 0,
        EnCurso = 1,
        Finalizada = 2,
        Cancelada = 3,
        SeparadoEnPdfs = 4
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
