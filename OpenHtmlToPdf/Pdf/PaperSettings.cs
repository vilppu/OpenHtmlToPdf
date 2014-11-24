using OpenHtmlToPdf.Interop;
using OpenHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf.Pdf
{
    public static class PaperSettings
    {
        public static IPdfDocument Oriented(this IPdfDocument pdfDocument, PaperOrientation paperOrientation)
        {
            return pdfDocument
                .BeforeRender(c => WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "orientation", paperOrientation.Orientation));
        }

        public static IPdfDocument OfSize(this IPdfDocument pdfDocument, PaperSize paperSize)
        {
            return pdfDocument
                .BeforeRender(c => WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "size.width", paperSize.Width))
                .BeforeRender(c => WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "size.height", paperSize.Height));
        }

        public static IPdfDocument With(this IPdfDocument pdfDocument, PaperMargins paperMargins)
        {
            return pdfDocument
                .BeforeRender(c => WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "margin.bottom", paperMargins.Bottom))
                .BeforeRender(c => WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "margin.left", paperMargins.Left))
                .BeforeRender(c => WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "margin.right", paperMargins.Right))
                .BeforeRender(c => WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "margin.top", paperMargins.Top));
        }
    }
}