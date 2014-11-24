namespace OpenHtmlToPdf.Pdf
{
    public static class ContentSettings
    {
        public static IPdfDocument WithTitle(this IPdfDocument pdfDocument, string title)
        {
            return pdfDocument
                .BeforeRender(c => WkHtmlToX.WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "documentTitle", title));
        }

        public static IPdfDocument WithOutline(this IPdfDocument pdfDocument)
        {
            return pdfDocument
                .BeforeRender(c => WkHtmlToX.WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "outline", "true"));
        }
    }
}