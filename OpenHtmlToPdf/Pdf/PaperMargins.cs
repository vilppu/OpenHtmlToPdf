using System.Globalization;

namespace OpenHtmlToPdf.Pdf
{
    public struct PaperMargins
    {
        private readonly double _top;
        private readonly double _right;
        private readonly double _bottom;
        private readonly double _left;
        private readonly string _unitOfLength;

        public PaperMargins(double top, double right, double bottom, double left, string unitOfLength)
        {
            _top = top;
            _right = right;
            _bottom = bottom;
            _left = left;
            _unitOfLength = unitOfLength;
        }

        public PaperMargins(double all, string unitOfLength)
            : this(all, all, all, all, unitOfLength)
        {
        }

        public string Top
        {
            get { return string.Format("{0}{1}", _top.ToString("0.##", CultureInfo.InvariantCulture), _unitOfLength); }
        }

        public string Right
        {
            get { return string.Format("{0}{1}", _right.ToString("0.##", CultureInfo.InvariantCulture), _unitOfLength); }
        }

        public string Bottom
        {
            get { return string.Format("{0}{1}", _bottom.ToString("0.##", CultureInfo.InvariantCulture), _unitOfLength); }
        }

        public string Left
        {
            get { return string.Format("{0}{1}", _left.ToString("0.##", CultureInfo.InvariantCulture), _unitOfLength); }
        }
    }
}