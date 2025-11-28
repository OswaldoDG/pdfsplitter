using PdfInspector.Separador.models;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfInspector.Separador
{
    public static class PdfSplitterService
    {
        public static void SplitPdf(string originalPdfPath, List<(int startPage, int endPage)> pageRanges, string outputPdfPath)
        {
            try
            {
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

        public static void AgruparYSepararPartes(string pdfOrigen, string outputDir, List<ParteDocumental> partes, Dictionary<int, string> nombresTipoDocumento)
        {
            try
            {
                var grupos = new Dictionary<int, List<ParteDocumental>>();
                var sinGrupo = new List<ParteDocumental>();

                foreach (var parte in partes)
                {
                    if (parte.IdAgrupamiento == null)
                        sinGrupo.Add(parte);
                    else
                    {
                        int gid = parte.IdAgrupamiento.Value;
                        if (!grupos.ContainsKey(gid))
                            grupos[gid] = new List<ParteDocumental>();
                        grupos[gid].Add(parte);
                    }
                }

                foreach (var kvp in grupos)
                {
                    var partesGrupo = kvp.Value.OrderBy(p => p.PaginaInicio).ToList();
                    int tipoId = partesGrupo[0].TipoDocumentoId;
                    string nombreArchivo = nombresTipoDocumento.ContainsKey(tipoId) ? nombresTipoDocumento[tipoId] : $"Grupo_{kvp.Key}";
                    string outputPath = Path.Combine(outputDir, $"{nombreArchivo}.pdf");

                    var pageRanges = partesGrupo.Select(p => (p.PaginaInicio, p.PaginaFin)).ToList();
                    SplitPdf(pdfOrigen, pageRanges, outputPath);
                }

                foreach (var parte in sinGrupo)
                {
                    string nombreArchivo = nombresTipoDocumento.ContainsKey(parte.TipoDocumentoId) ? nombresTipoDocumento[parte.TipoDocumentoId] : $"Documento_{parte.PaginaInicio}-{parte.PaginaFin}";
                    string outputPath = Path.Combine(outputDir, $"{nombreArchivo}.pdf");
                    var rango = new List<(int startPage, int endPage)> { (parte.PaginaInicio, parte.PaginaFin) };
                    SplitPdf(pdfOrigen, rango, outputPath);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
            }
        }
    }
}
