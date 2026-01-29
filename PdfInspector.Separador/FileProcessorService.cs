using PdfInspector.Separador.models;
using System.Collections.Specialized;


namespace PdfInspector.Separador
{


    public static class FileProcessorService
    {
        public static bool IsFileLocked(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            FileStream? stream = null;

            try
            {
                // Try to open with exclusive read/write access
                stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                return false; // If we got here, file is not locked
            }
            catch (IOException)
            {
                // IOException occurs if the file is in use by another process
                return true;
            }
            finally
            {
                stream?.Close();
            }
        }


        public static async Task<bool> ProcessFileAsync(string dbConn, string azureConn, string containerName, string encryptionKey, string tempPath, string outputPath, 
            string ProcesoId, StringDictionary tipos)
        {

            await DatabaseService.EliminaZombies(dbConn);
            var archivo = await DatabaseService.ObtieneArchivoFinalizado(dbConn);

            if (archivo == null)
            {
                Console.WriteLine("No hay archivos 'Finalizados' para procesar. Esperando...");
                return false;
            }

            

            Console.WriteLine($"Iniciando procesamiento para: {archivo.Nombre} (ID: {archivo.Id})");

            string tempEncryptedPath = Path.Combine(tempPath, archivo.Nombre);
            string tempDecryptedPath = Path.Combine(tempPath, $"decrypted_{archivo.Nombre}");
            
            if(IsFileLocked(tempDecryptedPath) || IsFileLocked(tempDecryptedPath))
            {
                Console.WriteLine($"Archivo en uso por otro proceso: {archivo.Nombre} (ID: {archivo.Id})");
                return true;
            }

            Console.WriteLine($"Estableciendo propieda del proceso: {archivo.Id} (ID: {ProcesoId}) ");

            await DatabaseService.ActualizaEstadoArchivo(archivo.Id, EstadoRevision.ProcesoPDF, dbConn, ProcesoId);


            try
            {
              var locked  = await DatabaseService.PorId(dbConn, archivo.Id);
                if(locked.IdProceso != ProcesoId)
                {
                    Console.WriteLine($"El archivo {archivo.Nombre} está siendo procesado por otro proceso. Saltando...");
                    return true;
                }

                Console.WriteLine($"Descargando '{archivo.Ruta}' desde Azure...");

                await AzureBlobService.DownloadBlobAsync(archivo.Ruta, tempEncryptedPath, azureConn, containerName);
                if(File.Exists(tempEncryptedPath) )
                {
                    Console.WriteLine("Desencriptando archivo...");
                    EncryptionService.DecryptFile(tempEncryptedPath, tempDecryptedPath, encryptionKey);
                    string originalFileName = Path.GetFileNameWithoutExtension(archivo.Nombre);
                    string outputDir = Path.Combine(outputPath, $"{originalFileName}");
                    Directory.CreateDirectory(outputDir);


                    var partes = await DatabaseService.ObtienePartesDocumental(archivo.Id, dbConn);


                    if (partes.Count == 0)
                    {
                        Console.WriteLine("El archivo no contiene partes definidas por lo que se exportará el archivo original. ");
                        await DatabaseService.ActualizaEstadoArchivo(archivo.Id, EstadoRevision.SeparadoEnPdfs, dbConn, ProcesoId);
                        File.Copy(tempDecryptedPath, Path.Combine(outputDir, archivo.Nombre), true);
                        if (File.Exists(tempEncryptedPath)) File.Delete(tempEncryptedPath);
                        if (File.Exists(tempDecryptedPath)) File.Delete(tempDecryptedPath);
                        return true;
                    }
                    else
                    {


                        var tipoDocumentoIds = partes.Select(p => p.TipoDocumentoId).Distinct().ToList();
                        var nombresTipoDocumento = new Dictionary<int, string>();
                        foreach (var id in tipoDocumentoIds)
                        {
                            string nombre = tipos[id.ToString()];
                            nombresTipoDocumento[id] = nombre;
                        }

                        PdfSplitterService.AgruparYSepararPartes(tempDecryptedPath, outputDir, partes, nombresTipoDocumento, dbConn);
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

                    await DatabaseService.ActualizaEstadoArchivo(archivo.Id, EstadoRevision.SeparadoEnPdfs, dbConn, ProcesoId);
                    Console.WriteLine($"Exportación completado para: {archivo.Nombre}");
                 
                }
                else
                {
                    Console.WriteLine($"No pudo descargarse el archivo : {locked.Nombre}");
                    await DatabaseService.ActualizaEstadoArchivo(archivo.Id, EstadoRevision.SinPDF, dbConn, ProcesoId);

                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fatal procesando {archivo.Nombre} . Regresando a 'Finalizada'.");
                await DatabaseService.ActualizaEstadoArchivo(archivo.Id, EstadoRevision.Finalizada, dbConn, ProcesoId);
                ErrorLog.Log(ex);
                return true;
            }
            finally
            {
                if (File.Exists(tempEncryptedPath)) File.Delete(tempEncryptedPath);
                if (File.Exists(tempDecryptedPath)) File.Delete(tempDecryptedPath);
               
            }
        }
    }
}
