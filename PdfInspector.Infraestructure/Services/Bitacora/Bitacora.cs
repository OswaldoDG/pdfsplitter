using PdfInspector.Domain.Abstractions.Bitacora;
using System;
using System.IO;

namespace PdfInspector.Infraestructure.Services.Bitacora
{
    public class Bitacora : IBitacora
    {

        private const int DIAS_LOG = 7;
        private readonly string _baseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "BitacoraSperto");

        public void LogError(string mensaje, Exception ex)
        {
            EscribirArchivo("ERROR", $"{mensaje}{Environment.NewLine}{ex}");
        }

        public void LogInfo(string mensaje)
        {
            EscribirArchivo("INFO", mensaje);
        }

        private void EscribirArchivo(string TipoLog, string contenido)
        {
            try
            {
                if (!Directory.Exists(_baseDir)) Directory.CreateDirectory(_baseDir);

                LimpiarLogsAntiguos();

                var logPath = Path.Combine(_baseDir, $"log_{DateTime.Now:yyyy-MM-dd}.txt");

                File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{TipoLog}]{Environment.NewLine}" + $"{contenido}{Environment.NewLine}" + new string('-', 100) + Environment.NewLine);
            }
            catch
            {

            }
        }

        private void LimpiarLogsAntiguos()
        {
            try
            {
                var archivos = Directory.GetFiles(_baseDir, "log_*.txt");

                foreach (var archivo in archivos)
                {
                    var info = new FileInfo(archivo);

                    if (info.CreationTime < DateTime.Now.AddDays(-DIAS_LOG))
                    {
                        info.Delete();
                    }
                }
            }
            catch
            {

            }
        }
    }
}
