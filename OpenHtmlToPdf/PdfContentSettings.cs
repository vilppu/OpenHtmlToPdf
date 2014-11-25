using OpenHtmlToPdf.Native;

namespace OpenHtmlToPdf
{
    public static class PdfContentSettings
    {
        public static IPdf WithTitle(this IPdf pdf, string title)
        {
            return pdf
                .BeforeRender(c => Wkhtmltox.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "documentTitle", title));
        }

        public static IPdf WithOutline(this IPdf pdf)
        {
            return pdf
                .BeforeRender(c => Wkhtmltox.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "outline", "true"));
        }
    }
}