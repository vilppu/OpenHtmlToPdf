namespace OpenHtmlToPdf
{
    public sealed class Pdf
    {
        public static IPdfDocument From(string html)
        {
            return new PdfDocument(html);
        }
    }
}