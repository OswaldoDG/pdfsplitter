using Microsoft.Extensions.Configuration;

IConfiguration rootConfig = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

//Clave hardcodeada
const string HardcodedEncryptionKey = "pruebademomensajeria12345";

var dbConn = rootConfig.GetConnectionString("DefaultConnection");
var azureConn = rootConfig["azure:blob:connectionString"];
var containerName = rootConfig["azure:blob:containerpdfs"];
var folderName = rootConfig["azure:blob:folderpdfs"];
var sourceFolderPath = rootConfig["AppSettings:SourceFolderPath"];

if (string.IsNullOrEmpty(sourceFolderPath) || !Directory.Exists(sourceFolderPath))
{
    Console.WriteLine("Error: La ruta de la carpeta de origen no es válida o no existe. Revisa appsettings.json.");
    return;
}

Console.WriteLine($"Iniciando proceso en: {sourceFolderPath}");
Console.WriteLine($"Contenedor de Azure: {containerName}");

try
{
    var pdfFiles = Directory.GetFiles(sourceFolderPath, "*.pdf");

    if (pdfFiles.Length == 0)
    {
        Console.WriteLine("No se encontraron archivos PDF para procesar.");
        return;
    }

    foreach (var filePath in pdfFiles)
    {
        await FileProcessor.ProcessFileAsync(
            filePath,
            dbConn,
            azureConn,
            containerName,
            HardcodedEncryptionKey,
            folderName
        );
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ocurrió un error general durante el proceso: {ex.Message} ❌");
}

Console.WriteLine("\nProceso finalizado. Presiona cualquier tecla para salir.");
Console.ReadKey();