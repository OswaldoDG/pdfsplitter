using Azure.Storage.Blobs;
using PdfInspector.CargaArchivos.models;
using System.Security.Cryptography;
using System.Text;

public static class FileProcessor
{
    public static async Task ProcessFileAsync(string filePath, string dbConn, string azureConn, string containerName, string encryptionKey, string folderName)
    {
        var fileName = Path.GetFileName(filePath);
        var blobPath = Path.Combine(folderName, fileName).Replace(Path.DirectorySeparatorChar, '/');

        if (DatabaseService.ArchivoYaProcesado(fileName, dbConn))
        {
            Console.WriteLine($"El archivo '{fileName}' ya fue procesado. Omitiendo.");
            return;
        }

        Console.WriteLine($"Procesando nuevo archivo: '{fileName}'...");

        Console.WriteLine("Encriptando archivo...");
        byte[] encryptedData = EncryptFile(filePath, encryptionKey);

        Console.WriteLine("Subiendo a Azure Blob Storage...");
        await UploadToBlobAsync(fileName, encryptedData, azureConn, containerName, folderName);

        Console.WriteLine("Registrando en la base de datos con la nueva entidad...");

        var nuevoArchivo = new ArchivoPdf
        {
            Id = 0,
            Nombre = fileName,
            Ruta = blobPath,
            Prioridad = 1,
            Estado = EstadoRevision.Pendiente,
            TotalPaginas = 0
        };

        DatabaseService.MarcarArchivoComoProcesado(nuevoArchivo, dbConn);

        Console.WriteLine($"Archivo '{fileName}' procesado y subido exitosamente");
    }

    private static async Task UploadToBlobAsync(string fileName, byte[] data, string azureConn, string containerName, string folderName)
    {
        string blobFullPath = Path.Combine(folderName, fileName).Replace(Path.DirectorySeparatorChar, '/');
        var blobServiceClient = new BlobServiceClient(azureConn);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        using (var memoryStream = new MemoryStream(data))
        {
            var blobClient = containerClient.GetBlobClient(blobFullPath);
            await blobClient.UploadAsync(memoryStream, overwrite: true);
        }
    }

    private static byte[] EncryptFile(string filePath, string key)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath);
        using (var aes = Aes.Create())
        {
            var keyBytes = new byte[32];
            var configKeyBytes = Encoding.UTF8.GetBytes(key);
            Array.Copy(configKeyBytes, keyBytes, Math.Min(keyBytes.Length, configKeyBytes.Length));

            aes.Key = keyBytes;
            aes.GenerateIV();

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var resultStream = new MemoryStream())
            {
                resultStream.Write(aes.IV, 0, aes.IV.Length);
                using (var cryptoStream = new CryptoStream(resultStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(fileBytes, 0, fileBytes.Length);
                    cryptoStream.FlushFinalBlock();
                }
                return resultStream.ToArray();
            }
        }
    }
}