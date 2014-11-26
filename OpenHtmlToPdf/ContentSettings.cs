namespace OpenHtmlToPdf
{
    public static class ContentSettings
    {
        public static IPdfDocument Comressed(this IPdfDocument pdfDocument)
        {
            return pdfDocument.WithGlobalSetting("useCompression", "true");
        }

        public static IPdfDocument WithTitle(this IPdfDocument pdfDocument, string title)
        {
            return pdfDocument.WithGlobalSetting("documentTitle", title);
        }

        public static IPdfDocument WithOutline(this IPdfDocument pdfDocument)
        {
            return pdfDocument.WithGlobalSetting("outline", "true");
        }

        public static IPdfDocument EncodedWith(this IPdfDocument pdfDocument, string encoding)
        {
            return pdfDocument.WithObjectSetting("web.defaultEncoding", encoding);
        }
    }
}