using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace OpenHtmlToPdf.Tests.Helpers
{
    public static class PdfDocumentReader
    {
        public static string ToText(byte[] content)
        {
            using (PdfDocument pdfDocument = GetPdfDocument(content))
            {
                return string.Join(Environment.NewLine, GetLinesFrom(content));
            }
        }

        public static string Title(byte[] content)
        {
            using (PdfDocument pdfDocument = GetPdfDocument(content))
            {
                return pdfDocument.GetDocumentInfo().GetTitle();
            }
        }

        public static double WidthOfFirstPage(byte[] content)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                using (PdfReader pdfReader = new PdfReader(stream))
                {
                    return new PdfDocument(pdfReader).GetFirstPage()
                        .GetPageSize()
                        .GetWidth();
                }
            }
        }

        public static double HeightOfFirstPage(byte[] content)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                using (PdfReader pdfReader = new PdfReader(stream))
                {
                    return new PdfDocument(pdfReader).GetFirstPage()
                        .GetPageSize()
                        .GetHeight();
                }
            }
        }

        private static IEnumerable<string> GetLinesFrom(byte[] content)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                using (PdfReader pdfReader = new PdfReader(stream))
                {
                    PdfDocument document = new PdfDocument(pdfReader);
                    SimpleTextExtractionStrategy stategy = new SimpleTextExtractionStrategy();
                    return Enumerable.Range(1, document.GetNumberOfPages())
                        .Select(pageNum => document.GetPage(pageNum))
                        .Select(page => PdfTextExtractor.GetTextFromPage(page, stategy))
                        .ToList();
                }
            }
        }

        private static PdfDocument GetPdfDocument(byte[] content)
        {
            using (MemoryStream stream = new MemoryStream(content))
            {
                using (PdfReader pdfReader = new PdfReader(stream))
                {
                    return new PdfDocument(pdfReader);
                }
            }
        }
    }
}