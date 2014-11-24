namespace OpenHtmlToPdf.Pdf
{
    public static class Compression
    {
        public static IPdfDocument Comressed(this IPdfDocument pdfDocument)
        {
            return pdfDocument.WithGlobalSetting("useCompression", "true");
        }
    }
}
