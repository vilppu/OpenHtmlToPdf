using System;
using System.Collections.Generic;
using System.Linq;
using iTextSharp.text.pdf.parser;

namespace OpenHtmlToPdf.Tests.Helpers
{
    public static class PdfDocument
    {
        public static string ToText(byte[] pdfDocument)
        {
            using (var pdfReader = new iTextSharp.text.pdf.PdfReader(pdfDocument))
            {
                return string.Join(Environment.NewLine, GetLinesFrom(pdfReader));
            }
        }

        public static string Title(byte[] pdfDocument)
        {
            using (var pdfReader = new iTextSharp.text.pdf.PdfReader(pdfDocument))
            {
                return pdfReader.Info["Title"];
            }
        }

        public static float WidthOfFirstPage(byte[] pdfDocument)
        {
            using (var pdfReader = new iTextSharp.text.pdf.PdfReader(pdfDocument))
            {
                return pdfReader.GetPageSize(1).Width;
            }
        }

        public static float HeightOfFirstPage(byte[] pdfDocument)
        {
            using (var pdfReader = new iTextSharp.text.pdf.PdfReader(pdfDocument))
            {
                return pdfReader.GetPageSize(1).Height;
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