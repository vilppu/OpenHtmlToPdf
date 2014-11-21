using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text.pdf.parser;

namespace OpenHtmlToPdf.Tests.Helpers
{
    public static class PdfToTextConverter
    {
        public static string ToText(byte[] pdfDocument)
        {
            using (var pdfReader = new iTextSharp.text.pdf.PdfReader(pdfDocument))
            {
                return string.Join(Environment.NewLine, GetLinesFrom(pdfReader));
            }
        }

        private static IEnumerable<string> GetLinesFrom(iTextSharp.text.pdf.PdfReader pdfReader)
        {
            var strategy = new SimpleTextExtractionStrategy();

            return Enumerable
                .Range(1, pdfReader.NumberOfPages)
                .Select(p => PdfTextExtractor.GetTextFromPage(pdfReader, p, strategy));
        }
    }
}