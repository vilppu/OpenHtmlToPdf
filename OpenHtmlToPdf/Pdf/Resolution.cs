using System.Globalization;
using OpenHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf.Pdf
{
    public static class Resolution
    {
        public static IPdfDocument WithResolution(this IPdfDocument pdfDocument, int dpi)
        {
            return pdfDocument
                .BeforeRender(c => WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "dpi", dpi.ToString(CultureInfo.InvariantCulture)));
        }
    }
}
