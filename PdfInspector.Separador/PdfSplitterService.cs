using Azure.Core.GeoJson;
using PdfInspector.Separador.models;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Security.Cryptography;

namespace PdfInspector.Separador
{
    public static class PdfSplitterService
    {
        public static void SplitPdf(string originalPdfPath, List<(int startPage, int endPage)> pageRanges, string outputPdfPath)
        {
            try
            {
                if (File.Exists(outputPdfPath))
                {
                    return;
                }



                using (PdfDocument originalDoc = PdfReader.Open(originalPdfPath, PdfDocumentOpenMode.Import))
                {
                    using (PdfDocument newDoc = new PdfDocument())
                    {
                        newDoc.Info.Title = Path.GetFileNameWithoutExtension(outputPdfPath);

                        foreach (var range in pageRanges)
                        {
                            for (int i = range.startPage - 1; i <= range.endPage - 1; i++)
                            {
                                if (i >= 0 && i < originalDoc.PageCount)
                                {
                                    newDoc.AddPage(originalDoc.Pages[i]);
                                }
                            }
                        }

                        if (newDoc.PageCount > 0)
                        {
                            newDoc.Save(outputPdfPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }

        private static string GetHash(string ruta)
        {
            using (FileStream stream = File.OpenRead(ruta))
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(stream);

                // Convert to Base64 string
                string base64Hash = Convert.ToBase64String(hashBytes);

                return base64Hash;
            }
        }

        public static void AgruparYSepararPartes(string pdfOrigen, string outputDir, List<ParteDocumental> partes, Dictionary<int, string> nombresTipoDocumento, string connString)
        {
            try
            {

                List<int> agrupamientos = [];

                if(partes.Any(p => p.IdAgrupamiento != null))
                {
                    agrupamientos = partes.Where(p => p.IdAgrupamiento != null).Select(p => p.IdAgrupamiento!.Value).Distinct().ToList();
                }

                List<int> tipos = partes.Where(p => p.IdAgrupamiento == null).Distinct().Select(p => p.TipoDocumentoId).ToList(); 
                foreach(var tipo in tipos)
                {
                    var partesTipo = partes.Where(p => p.TipoDocumentoId == tipo && p.IdAgrupamiento == null).ToList();
                    if(partesTipo.Count > 1)
                    {
                        // procesa partes duplicadas sin agrupar
                        int index = 1;
                        foreach (var parte in partesTipo)
                        {
                            string nombreArchivo = nombresTipoDocumento[tipo];
                            string precheck = Path.Combine(outputDir, $"{nombreArchivo}.pdf");
                            if(File.Exists(precheck))
                            {
                                File.Delete(precheck);
                            }
                            string outputPath = Path.Combine(outputDir, $"{nombreArchivo}-{index.ToString().PadLeft(3, '0')}.pdf");

                            if (File.Exists(outputPath))
                            {
                                File.Delete(outputPath);
                            }

                            var pageRanges = new List<(int startPage, int endPage)> { (parte.PaginaInicio, parte.PaginaFin) } ;
                            SplitPdf(pdfOrigen, pageRanges, outputPath);
                            DatabaseService.ActualizaRutaParte(parte.Id, outputPath, connString); 
                            index ++; 
                        }
                    }
                    else
                    {
                        var parte = partesTipo[0];
                        string nombreArchivo = nombresTipoDocumento[parte.TipoDocumentoId];
                            string outputPath = Path.Combine(outputDir, $"{nombreArchivo}.pdf");
                            var rango = new List<(int startPage, int endPage)> { (parte.PaginaInicio, parte.PaginaFin) };
                            SplitPdf(pdfOrigen, rango, outputPath);
                        DatabaseService.ActualizaRutaParte(parte.Id, outputPath, connString);

                    }
                }   

                if(agrupamientos.Count > 0)
                {
                    foreach(var id in agrupamientos)
                    {
                        var partesGrupo = partes.Where(p => p.IdAgrupamiento == id).OrderBy(p => p.PaginaInicio).ToList(); 
                        int tipoId = partesGrupo[0].TipoDocumentoId;
                        string nombreArchivo =  nombresTipoDocumento[tipoId];
                        string outputPath = Path.Combine(outputDir, $"{nombreArchivo}.pdf");
                        var pageRanges = partesGrupo.Select(p => (p.PaginaInicio, p.PaginaFin)).ToList();
                        SplitPdf(pdfOrigen, pageRanges, outputPath);
                        foreach(var parte in partesGrupo)
                        {
                            DatabaseService.ActualizaRutaParte(parte.Id, outputPath, connString);
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }
    }
}
