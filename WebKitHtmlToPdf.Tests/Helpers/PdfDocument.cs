using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace WebKitHtmlToPdf.Tests.Helpers
{
    public static class PdfDocument
    {
        public static string ReadDocumentContent(byte[] pdfDocument)
        {
            using (var pdfReader = new PdfReader(pdfDocument))
            {
                return string.Join(Environment.NewLine, GetLinesFrom(pdfReader));
            }
        }

        private static IEnumerable<string> GetLinesFrom(PdfReader pdfReader)
        {
            var strategy = new SimpleTextExtractionStrategy();

            return Enumerable
                .Range(1, pdfReader.NumberOfPages)
                .Select(p => PdfTextExtractor.GetTextFromPage(pdfReader, p, strategy));
        }
    }
}