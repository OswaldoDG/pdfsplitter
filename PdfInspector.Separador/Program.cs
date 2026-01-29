using Microsoft.Extensions.Configuration;
using PdfInspector.Separador;
using System.Collections.Specialized;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Iniciando Servicio de División de PDFs...");

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfiguration config = builder.Build();

        string dbConn = config.GetConnectionString("DefaultConnection");
        string azureConn = config["azure:blob:connectionString"];
        string containerName = config["azure:blob:containerpdfs"];
        const string HardcodedEncryptionKey = "pruebademomensajeria12345";
        string tempPath = config["AppSettings:TempDownloadPath"];
        string outputPath = config["AppSettings:OutputSplitPath"];
        int loopDelay = int.Parse(config["AppSettings:LoopDelayMilliseconds"] ?? "5000");

        Console.WriteLine($"Buscando archivos 'Finalizados' cada {loopDelay / 1000} segundos.");
        Console.WriteLine($"Salida de PDFs divididos en: {outputPath}");

        bool pendientes = true;
        var tipos = await DatabaseService.ObtieneTiposDocumento(dbConn);
        if(!Directory.Exists(tempPath))
        {
            Directory.CreateDirectory(tempPath);
        }

        string ProcId = $"T{DateTime.Now.Ticks}";

        while (true)
        {
            try
            {
                pendientes = await FileProcessorService.ProcessFileAsync(
                    dbConn,
                    azureConn,
                    containerName,
                    HardcodedEncryptionKey,
                    tempPath,
                    outputPath,
                    ProcId,
                    tipos
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error inesperado en el bucle principal: {ex.Message}");
            }

            if(!pendientes)
            {
                await Task.Delay(loopDelay);
            }
                
        }
    }
}