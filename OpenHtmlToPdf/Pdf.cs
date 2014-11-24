namespace OpenHtmlToPdf
{
    public static class Pdf
    {
        private static readonly object SyncRoot = new object();

        public static byte[] From(string html)
        {
            return ConvertToPdf(html);
        }

        private static byte[] ConvertToPdf(string html)
        {
            lock (SyncRoot)
            {
                using (var htmlToPdfConverter = HtmlToPdfConverter.Create())
                {
                    return htmlToPdfConverter.ConvertToPdf(HtmlToPdfDocument.From(html));
                }
            }
        }
    }
}
