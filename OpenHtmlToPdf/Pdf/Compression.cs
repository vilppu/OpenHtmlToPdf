using OpenHtmlToPdf.Interop;
using OpenHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf.Pdf
{
    public static class Compression
    {
        public static IPdfDocument Comressed(this IPdfDocument pdfDocument)
        {
            return pdfDocument
                .BeforeRender(c => WkHtmlToX.WkHtmlToPdf.wkhtmltopdf_set_global_setting(c.GlobalSettingsPointer, "useCompression", "true"));
        }
    }
}
