using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Infraestructure.Config;

namespace PdfInspector.Infraestructure.Services.Pdf
{
    public class PdfServiceFactory : IPdfServiceFactory
    {
        private readonly EndpointConfig _config;

        public PdfServiceFactory(EndpointConfig config)
        {
            _config = config;
        }

        public IPdfService Create(string token)
        {
            return new PdfService(_config, token);
        }
    }
}
