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
                Console.WriteLine($"Error al dividir PDF: {ex.Message}. PDF de origen: {originalPdfPath}");
                throw;
            }
        }
    }
}
