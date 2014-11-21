using System;
using System.Globalization;

namespace OpenHtmlToPdf.Settings
{
    [Serializable]
    class GlobalSettings
    {
        public enum DocumentColorMode
        {
            Color,
            Grayscale
        }

        public enum DocumentOutputFormat
        {
            Pdf,
            Ps
        }

        public enum PaperOrientation
        {
            Portrait,
            Landscape
        }

        private MarginSettings _margins = new MarginSettings();

        public GlobalSettings()
        {
            ColorMode = DocumentColorMode.Color;
            Orientation = PaperOrientation.Portrait;
            Copies = 1;
            OutputFormat = DocumentOutputFormat.Pdf;
            UseCompression = true;
            OutlineDepth = 4;
        }

        /// <summary>
        ///     Whether to collate the copies. (Default: false)
        /// </summary>
        [Setting("collate")]
        public bool? Collate { get; set; }

        /// <summary>
        ///     Whether to print in color or grayscale. (Default: color)
        /// </summary>
        public DocumentColorMode ColorMode { get; set; }

        /// <summary>
        ///     The path of a file used to store cookies.
        /// </summary>
        [Setting("load.cookieJar")]
        public string CookieJar { get; set; }

        /// <summary>
        ///     How many copies to print. (Default: 1)
        /// </summary>
        [Setting("copies")]
        public int Copies { get; set; }

        /// <summary>
        ///     The title of the PDF document.
        /// </summary>
        [Setting("documentTitle")]
        public string DocumentTitle { get; set; }

        /// <summary>
        ///     The DPI to use when printing.
        /// </summary>
        [Setting("dpi")]
        public int? Dpi { get; set; }

        /// <summary>
        ///     The path of a file to dump an XML outline of the document to.
        /// </summary>
        [Setting("dumpOutline")]
        public string DumpOutline { get; set; }

        /// <summary>
        ///     The maximum DPI to use for images printed in the document.
        /// </summary>
        [Setting("imageDPI")]
        public int? ImageDpi { get; set; }

        [Setting("imageQuality")]
        public int? ImageQuality { get; set; }

        /// <summary>
        ///     The margins to use throughout the document.
        /// </summary>
        public MarginSettings Margins
        {
            get { return _margins; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _margins = value;
            }
        }

        /// <summary>
        ///     The orientation of the output document, either Portrait or Landscape. (Default: Portrait)
        /// </summary>
        [Setting("orientation")]
        public PaperOrientation Orientation { get; set; }

        /// <summary>
        ///     The maximum depth of the outline. (Default: 4)
        /// </summary>
        [Setting("outlineDepth")]
        public int? OutlineDepth { get; set; }

        /// <summary>
        ///     A path to output the converted document to.
        /// </summary>
        [Setting("out")]
        public string OutputFile { get; set; }

        /// <summary>
        ///     Whether to output PDF or PostScript. (Default: PDF)
        /// </summary>
        [Setting("outputFormat")]
        public DocumentOutputFormat OutputFormat { get; set; }

        /// <summary>
        ///     A number that is added to all page numbers when printing headers, footers and table of content.
        /// </summary>
        [Setting("pageOffset")]
        public int? PageOffset { get; set; }

        /// <summary>
        ///     The size of the output document.
        /// </summary>
        public PaperSize PaperSize { get; set; }

        /// <summary>
        ///     Whether to generate an outline for the document. (Default: false)
        /// </summary>
        [Setting("outline")]
        public bool? ProduceOutline { get; set; }

        /// <summary>
        ///     Whether to use lossless compression when creating the pdf file. (Default: true)
        /// </summary>
        [Setting("useCompression")]
        public bool? UseCompression { get; set; }

        [Setting("margin.bottom")]
        internal string MarginBottom
        {
            get { return GetMarginValue(_margins.Bottom); }
        }

        [Setting("margin.left")]
        internal string MarginLeft
        {
            get { return GetMarginValue(_margins.Left); }
        }

        [Setting("margin.right")]
        internal string MarginRight
        {
            get { return GetMarginValue(_margins.Right); }
        }

        [Setting("margin.top")]
        internal string MarginTop
        {
            get { return GetMarginValue(_margins.Top); }
        }

        /// <summary>
        ///     The height of the output document, e.g. "12in".
        /// </summary>
        [Setting("size.height")]
        internal string PaperHeight
        {
            get { return PaperSize == null ? null : PaperSize.Height; }
        }

        /// <summary>
        ///     The with of the output document, e.g. "4cm".
        /// </summary>
        [Setting("size.width")]
        internal string PaperWidth
        {
            get { return PaperSize == null ? null : PaperSize.Width; }
        }

        [Setting("colorMode")]
        internal string StringColorMode
        {
            get { return ColorMode == DocumentColorMode.Color ? "grayscale" : "color"; }
        }

        private string GetMarginValue(double value)
        {
            string strUnit = "in";

            switch (_margins.Unit)
            {
                case (Unit.Centimeters):
                    strUnit = "cm";
                    break;
                case (Unit.Millimeters):
                    strUnit = "mm";
                    break;
            }

            return String.Format("{0}{1}", value.ToString("0.##", CultureInfo.InvariantCulture), strUnit);
        }
    }
}