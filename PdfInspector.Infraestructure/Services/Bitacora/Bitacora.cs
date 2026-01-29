using PdfInspector.Domain.Abstractions.Bitacora;
using System;
using System.IO;

namespace PdfInspector.Infraestructure.Services.Bitacora
{
    public class Bitacora : IBitacora
    {

        private readonly string _logDir;
        private readonly string _logPath;

        public Bitacora()
        {
            _logDir = @"C:\BitacoraSperto";
            _logPath = Path.Combine(_logDir, "log.txt");

            if (!Directory.Exists(_logDir))
                Directory.CreateDirectory(_logDir);
        }

        public void LogError(Exception ex)
        {
            try
            {
                File.AppendAllText(
                    _logPath,
                    $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}{Environment.NewLine}{ex}{Environment.NewLine}{Environment.NewLine}"
                );
            }
            catch
            {
            }
        }
    }
}
