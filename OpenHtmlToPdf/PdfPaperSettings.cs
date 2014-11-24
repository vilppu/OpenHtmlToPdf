using OpenHtmlToPdf.Native;

namespace OpenHtmlToPdf
{
    public static class PdfPaperSettings
    {
        public static IPdf WithPaperOrientation(this IPdf pdf, PaperOrientation paperOrientation)
        {
            return pdf
                .BeforeRender(c => Wkhtmltox.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "orientation", paperOrientation.Orientation));
        }

        public static IPdf WithPaperSize(this IPdf pdf, PaperSize paperSize)
        {
            return pdf
                .BeforeRender(c => Wkhtmltox.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "size.width", paperSize.Width))
                .BeforeRender(c => Wkhtmltox.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "size.height", paperSize.Height));
        }

        public static IPdf WithMargins(this IPdf pdf, PaperMargins paperMargins)
        {
            return pdf
                .BeforeRender(c => Wkhtmltox.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "margin.bottom", paperMargins.Bottom))
                .BeforeRender(c => Wkhtmltox.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "margin.left", paperMargins.Left))
                .BeforeRender(c => Wkhtmltox.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "margin.right", paperMargins.Right))
                .BeforeRender(c => Wkhtmltox.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "margin.top", paperMargins.Top));
        }
    }
}