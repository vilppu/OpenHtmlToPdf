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

        public static IPdfDocument WithMargins(this IPdfDocument pdfDocument, PaperMargins paperMargins)
        {
            return pdfDocument
                .WithGlobalSetting("margin.bottom", paperMargins.BottomSetting)
                .WithGlobalSetting("margin.left", paperMargins.LeftSetting)
                .WithGlobalSetting("margin.right", paperMargins.RightSetting)
                .WithGlobalSetting("margin.top", paperMargins.TopSetting);
        }
        
        public static IPdfDocument WithResolution(this IPdfDocument pdfDocument, int dpi)
        {
            return pdfDocument
                .WithGlobalSetting("dpi", dpi.ToString(CultureInfo.InvariantCulture));
        }
    }
}