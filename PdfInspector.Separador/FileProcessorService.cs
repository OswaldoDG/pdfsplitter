using PdfInspector.Separador.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Separador
{
    public static class FileProcessorService
    {
        public static async Task ProcessFileAsync(string dbConn, string azureConn, string containerName, string encryptionKey, string tempPath, string outputPath)
        {
            var archivo = await DatabaseService.ObtieneArchivoFinalizado(dbConn);
            if (archivo == null)
            {
                Console.WriteLine("No hay archivos 'Finalizados' para procesar. Esperando...");
                return;
            }

            Console.WriteLine($"Iniciando procesamiento para: {archivo.Nombre} (ID: {archivo.Id})");

            string tempEncryptedPath = Path.Combine(tempPath, archivo.Nombre);
            string tempDecryptedPath = Path.Combine(tempPath, $"decrypted_{archivo.Nombre}");

            try
            {
                var partes = await DatabaseService.ObtienePartesDocumental(archivo.Id, dbConn);
                if (partes.Count == 0)
                {
                    Console.WriteLine($"Error: El archivo {archivo.Nombre} no tiene 'partes' definidas. Regresando a 'Finalizada'.");
                    return;
                }

                Directory.CreateDirectory(tempPath);

                Console.WriteLine($"Descargando '{archivo.Ruta}' desde Azure...");
                await AzureBlobService.DownloadBlobAsync(archivo.Ruta, tempEncryptedPath, azureConn, containerName);

                Console.WriteLine("Desencriptando archivo...");
                EncryptionService.DecryptFile(tempEncryptedPath, tempDecryptedPath, encryptionKey);

                if (File.Exists(tempEncryptedPath))
                {
                    File.Delete(tempEncryptedPath);
                    Console.WriteLine("Archivo encriptado temporal eliminado.");
                }

                string originalFileName = Path.GetFileNameWithoutExtension(archivo.Nombre);
                string outputDir = Path.Combine(outputPath, originalFileName);
                Directory.CreateDirectory(outputDir);

                foreach (var parte in partes)
                {
                    string tipoNombre = await DatabaseService.ObtieneNombreTipoDocumento(parte.TipoDocumentoId, dbConn);
                    string basePdfName = $"{originalFileName}_{tipoNombre}";

                    string outputPdfPath = Path.Combine(outputDir, $"{basePdfName}.pdf");
                    int counter = 1;

                    while (File.Exists(outputPdfPath))
                    {
                        string newFileName = $"{basePdfName} ({counter}).pdf";
                        outputPdfPath = Path.Combine(outputDir, newFileName);
                        counter++;
                    }

                    PdfSplitterService.SplitPdf(
                        tempDecryptedPath,
                        parte.PaginaInicio,
                        parte.PaginaFin,
                        outputPdfPath);
                }

                if (File.Exists(tempDecryptedPath))
                {
                    File.Delete(tempDecryptedPath);
                    Console.WriteLine("Archivo desencriptado temporal eliminado.");
                }

                await DatabaseService.ActualizaEstadoArchivo(archivo.Id, EstadoRevision.SeparadoEnPdfs, dbConn);
                Console.WriteLine($"Separación completado para: {archivo.Nombre}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fatal procesando {archivo.Nombre}: {ex.Message}. Regresando a 'Finalizada'.");
            }
            finally
            {
                if (File.Exists(tempEncryptedPath)) File.Delete(tempEncryptedPath);
                if (File.Exists(tempDecryptedPath)) File.Delete(tempDecryptedPath);
            }
        }
    }
}
