namespace OpenHtmlToPdf
{
    sealed class PdfDocument : IPdfDocument
    {
        private static readonly object SyncRoot = new object();
        private readonly string _html;

        public PdfDocument(string html)
        {
            _html = html;
        }

        public byte[] Content()
        {
            lock (SyncRoot)
            {
                using (var htmlToPdfConverter = HtmlToPdf.Create())
                {
                    return htmlToPdfConverter.ConvertToPdf(PdfDocumentSettings.From(_html));
                }
            }
        }
    }
}
