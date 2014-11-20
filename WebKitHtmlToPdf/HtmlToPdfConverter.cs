using System.Drawing.Printing;
using WebKitHtmlToPdf.TuesPechkin;

namespace WebKitHtmlToPdf
{
    public static class HtmlToPdfConverter
    {
        public static byte[] ConvertToPdf(string html)
        {
            var document = GetDocument(html);

            return new SimplePechkin().Convert(document);
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
