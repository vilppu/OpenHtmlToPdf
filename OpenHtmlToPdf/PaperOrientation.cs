namespace OpenHtmlToPdf
{
    public sealed class PaperOrientation
    {
        private readonly string _orientation;

        private PaperOrientation(string orientation)
        {
            _orientation = orientation;
        }

        public static PaperOrientation Default
        {

            get { return Portrait; }
        }

        public static PaperOrientation Portrait
        {

            get { return new PaperOrientation("Portrait"); }
        }
        public static PaperOrientation Landscape
        {

            get { return new PaperOrientation("Landscape"); }
        }

        public string Orientation
        {
            get { return _orientation; }
        }
    }
}
