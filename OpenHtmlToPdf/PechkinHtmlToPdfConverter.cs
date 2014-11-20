using System.Drawing.Printing;
using OpenHtmlToPdf.Settings;
using OpenHtmlToPdf.TuesPechkin;

namespace OpenHtmlToPdf
{
    static class PechkinHtmlToPdfConverter
    {
        public static byte[] Convert(string html)
        {
            var document = GetDocument(html);

            using (new WkhtmltoxLibrary())
            {
                return new SimplePechkin().Convert(document);
            }
        }

        private static HtmlToPdfDocument GetDocument(string html)
        {
            return new HtmlToPdfDocument
            {
                GlobalSettings =
                {
                    ProduceOutline = true,
                    DocumentTitle = "",
                    PaperSize = PaperKind.A4,
                    Margins =
                    {
                        All = 1.375,
                        Unit = Unit.Centimeters
                    }
                },
                Objects = 
                {
                    new ObjectSettings { HtmlText = html },
                }
            };
        }
    }
}