using Microsoft.Extensions.Configuration;
using PdfInspector.App.CasosUso.Auth;
using PdfInspector.Application.CasosUso.Pdf;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Forms;
using PdfInspector.Infraestructure.Config;
using PdfInspector.Infraestructure.Services.Auth;
using PdfInspector.Infraestructure.Services.Pdf;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace PdfInspector
{
    public  class DI
    {
        public static Container Configure()
        {

            var container = new Container();
            container.Options.EnableAutoVerification = false;
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("config.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var settings = new EndpointConfig();
            configuration.GetSection("Endpoints").Bind(settings);

            container.RegisterInstance(settings);

            container.RegisterSingleton<UsuarioSesion>();

            container.RegisterSingleton<HttpClient>(() => new HttpClient());

            container.RegisterSingleton<IAuthService, AuthService>();
            container.RegisterSingleton<IPdfService, PdfService>();

            container.Register<LoginCasoUso>();
            container.Register<RegistroCasoUso>();
            container.Register<ObtieneTipoDocumentosPdfCasoUso>();
            container.Register<DescargarPdfCasoUso>();

            container.Register<LoginForm>();
            container.Register<RegistroForm>();
            container.Register<Form1>();
            return container;
        }
    }
}
