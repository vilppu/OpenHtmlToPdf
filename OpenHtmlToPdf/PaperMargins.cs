namespace OpenHtmlToPdf
{
    public sealed class PaperMargins
    {
        private readonly Length _top;
        private readonly Length _right;
        private readonly Length _bottom;
        private readonly Length _left;

        private PaperMargins(Length allMargins)
            : this(allMargins, allMargins, allMargins, allMargins)
        {
        }

        private PaperMargins(Length top, Length right, Length bottom, Length left)
        {
            _top = top;
            _right = right;
            _bottom = bottom;
            _left = left;
        }

        public static PaperMargins All(Length allMargins)
        {
            return new PaperMargins(allMargins);
        }

        public static PaperMargins None()
        {
            return new PaperMargins(Length.Zero());
        }

        public PaperMargins Top(Length top)
        {
            return new PaperMargins(top, _right, _bottom, _left);
        }

        public PaperMargins Right(Length right)
        {
            return new PaperMargins(_top, right, _bottom, _left);
        }

        public PaperMargins Botton(Length botton)
        {
            return new PaperMargins(_top, _right, botton, _left);
        }

        public PaperMargins Left(Length left)
        {
            return new PaperMargins(_top, _right, _bottom, left);
        }

        public static implicit operator PaperMargins(Length allMargins)
        {
            return new PaperMargins(allMargins);
        }

        public string TopSetting
        {
            get { return _top.SettingString; }
        }

        public string RightSetting
        {
            get { return _right.SettingString; }
        }

        public string BottomSetting
        {
            get { return _bottom.SettingString; }
        }

        public string LeftSetting
        {
            get { return _left.SettingString; }
        }
    }
}