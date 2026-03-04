using Microsoft.Extensions.Configuration;
using PdfInspector.App.CasosUso.Auth;
using PdfInspector.Application.CasosUso.Auth;
using PdfInspector.Application.CasosUso.Pdf;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Abstractions.Bitacora;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Domain.Models.Auth;
using PdfInspector.Forms;
using PdfInspector.Infraestructure.Config;
using PdfInspector.Infraestructure.Services.Auth;
using PdfInspector.Infraestructure.Services.Bitacora;
using PdfInspector.Infraestructure.Services.Pdf;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;

namespace PdfInspector
{
    public  class DI
    {
        public static Container Configure()
        {
            var tempDir = Path.Combine(Path.GetTempPath(), "pdfsplitter");
            var settingsPath = Path.Combine(tempDir, "config.json");
            var sourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.prod.json");

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }

            if (!File.Exists(settingsPath) ||
                File.GetLastWriteTime(sourcePath) > File.GetLastWriteTime(settingsPath))
            {
                File.Copy(sourcePath, settingsPath, true);
            }

            var container = new Container();
            container.Options.EnableAutoVerification = false;

            var builder = new ConfigurationBuilder()
                .AddJsonFile(settingsPath, optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            var appConfig = new AppConfig();
            configuration.Bind(appConfig);

            container.RegisterInstance(appConfig);

            container.RegisterSingleton<UsuarioSesion>();
            container.RegisterSingleton<IBitacora, Bitacora>();

            container.RegisterSingleton<IAuthService>(() =>
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(appConfig.Endpoints.AuthApi.BaseUrl),
                    Timeout = Timeout.InfiniteTimeSpan
                };
                return new AuthService(appConfig, client);
            });


            container.Register<RefrescarCasoUso>();

            container.RegisterSingleton<HttpClient>(() =>
            {
                var sesion = container.GetInstance<UsuarioSesion>();
                var refrescarCasoUso = container.GetInstance<RefrescarCasoUso>();

                var handler = new AuthenticatedHttpHandler(refrescarCasoUso, sesion)
                {
                    InnerHandler = new HttpClientHandler()
                };

                var client = new HttpClient(handler)
                {
                    Timeout = Timeout.InfiniteTimeSpan
                };

                return client;
            });


            container.RegisterSingleton<IPdfService, PdfService>();

            container.Register<LoginCasoUso>();
            container.Register<RegistroCasoUso>();
            container.Register<ObtieneTipoDocumentosPdfCasoUso>();
            container.Register<CompletarCasoUso>();
            container.Register<SiguientePendienteCasoUso>();
            container.Register<MisEstadisticasCasoUso>();
            container.Register<ValidacionArchivoIdCasoUso>();

            container.Register<LoginForm>();
            container.Register<RegistroForm>();
            container.Register<Form1>();

            return container;
        }


    }
}
