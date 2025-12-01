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
            await DatabaseService.EliminaZombies(dbConn);
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
                await DatabaseService.ActualizaEstadoArchivo(archivo.Id, EstadoRevision.ProcesoPDF, dbConn);

                var partes = await DatabaseService.ObtienePartesDocumental(archivo.Id, dbConn);


                Directory.CreateDirectory(tempPath);

                Console.WriteLine($"Descargando '{archivo.Ruta}' desde Azure...");
                await AzureBlobService.DownloadBlobAsync(archivo.Ruta, tempEncryptedPath, azureConn, containerName);

                if (partes.Count == 0)
                {
                    Console.WriteLine("El archivo no contiene partes definidas por lo que se exportará el archivo original. ");
                    string outputDir = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(archivo.Nombre));
                    Directory.CreateDirectory(outputDir);
                    string outPutDirFile = Path.Combine(outputDir, archivo.Nombre);
                    EncryptionService.DecryptFile(tempEncryptedPath, outPutDirFile, encryptionKey);
                }
                else
                {
                    Console.WriteLine("Desencriptando archivo...");
                    EncryptionService.DecryptFile(tempEncryptedPath, tempDecryptedPath, encryptionKey);
                    string originalFileName = Path.GetFileNameWithoutExtension(archivo.Nombre);
                    string outputDir = Path.Combine(outputPath, originalFileName);
                    Directory.CreateDirectory(outputDir);

                    var tipoDocumentoIds = partes.Select(p => p.TipoDocumentoId).Distinct().ToList();
                    var nombresTipoDocumento = new Dictionary<int, string>();
                    foreach (var id in tipoDocumentoIds)
                    {
                        string nombre = await DatabaseService.ObtieneNombreTipoDocumento(id, dbConn);
                        nombresTipoDocumento[id] = nombre;
                    }

                    PdfSplitterService.AgruparYSepararPartes(tempDecryptedPath, outputDir, partes, nombresTipoDocumento);
                }

                if (File.Exists(tempDecryptedPath))
                {
                    File.Delete(tempDecryptedPath);
                    Console.WriteLine("Archivo desencriptado temporal eliminado.");
                }

                if (File.Exists(tempEncryptedPath))
                {
                    File.Delete(tempEncryptedPath);
                    Console.WriteLine("Archivo encriptado temporal eliminado.");
                }

                await DatabaseService.ActualizaEstadoArchivo(archivo.Id, EstadoRevision.SeparadoEnPdfs, dbConn);
                Console.WriteLine($"Exportación completado para: {archivo.Nombre}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fatal procesando {archivo.Nombre} . Regresando a 'Finalizada'.");
                await DatabaseService.ActualizaEstadoArchivo(archivo.Id, EstadoRevision.Finalizada, dbConn);
                ErrorLog.Log(ex);
            }
            finally
            {
                if (File.Exists(tempEncryptedPath)) File.Delete(tempEncryptedPath);
                if (File.Exists(tempDecryptedPath)) File.Delete(tempDecryptedPath);
            }
        }
    }
}
