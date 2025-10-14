namespace PdfInspector.Infraestructure.Config
{
    public class EndpointConfig
    {
        public AuthApiConfig AuthApi { get; set; }
        public PdfApiConfig PdfApi { get; set; }
    }

    public class AuthApiConfig
    {
        public string BaseUrl { get; set; }
        public string Login { get; set; }
        public string Register { get; set; }
    }


    public class PdfApiConfig
    {
        public string BaseUrl { get; set; }
        public string DescargarPorId { get; set; }
        public string Siguiente { get; set; }
        public string FinalizarPorId { get; set; }
    }
}
