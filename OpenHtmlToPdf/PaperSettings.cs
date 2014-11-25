using System.Globalization;

namespace OpenHtmlToPdf
{
    public static class PaperSettings
    {
        public static IPdfDocument Oriented(this IPdfDocument pdfDocument, PaperOrientation paperOrientation)
        {
            return pdfDocument
                .WithGlobalSetting("orientation", paperOrientation.Orientation);
        }

        public static IPdfDocument OfSize(this IPdfDocument pdfDocument, PaperSize paperSize)
        {
            return pdfDocument
                .WithGlobalSetting("size.width", paperSize.Width)
                .WithGlobalSetting("size.height", paperSize.Height);
        }

        public static IPdfDocument With(this IPdfDocument pdfDocument, PaperMargins paperMargins)
        {
            return pdfDocument
                .WithGlobalSetting("margin.bottom", paperMargins.Bottom)
                .WithGlobalSetting("margin.left", paperMargins.Left)
                .WithGlobalSetting("margin.right", paperMargins.Right)
                .WithGlobalSetting("margin.top", paperMargins.Top);
        }
        
        public static IPdfDocument WithResolution(this IPdfDocument pdfDocument, int dpi)
        {
            return pdfDocument
                .WithGlobalSetting("dpi", dpi.ToString(CultureInfo.InvariantCulture));
        }
    }
}