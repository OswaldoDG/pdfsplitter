using Microsoft.Extensions.Configuration;
using PdfInspector.App.CasosUso.Auth;
using PdfInspector.Domain.Abstractions.Auth;
using PdfInspector.Domain.Abstractions.Pdf;
using PdfInspector.Forms;
using PdfInspector.Infraestructure.Config;
using PdfInspector.Infraestructure.Services.Auth;
using PdfInspector.Infraestructure.Services.Pdf;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System;

namespace PdfInspector
{
    public  class DI
    {
        public static Container Configure()
        {
            var container = new Container();

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("config.json");

            var configuration = builder.Build();

            var settings = new EndpointConfig();
            configuration.GetSection("Endpoints").Bind(settings);


            container.RegisterInstance(settings);
            container.RegisterSingleton<IAuthService>(() => new AuthService(settings));
            container.RegisterSingleton<IPdfServiceFactory>(() => new PdfServiceFactory(settings));

            container.Register<LoginCasoUso>(Lifestyle.Singleton);
            container.Register<RegistroCasoUso>(Lifestyle.Singleton);

            container.Register<LoginForm>(Lifestyle.Singleton);
            container.Register<RegistroForm>(Lifestyle.Singleton);

            container.Options.SuppressLifestyleMismatchVerification = true;

            container.Verify();
            return container;
        }
    }
}
