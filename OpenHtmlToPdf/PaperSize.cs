namespace OpenHtmlToPdf
{
    public struct PaperSize
    {
        private readonly double _width;
        private readonly double _height;
        private readonly string _unitOfLength;

        public PaperSize(double width, double height, string unitOfLength)
        {
            _width = width;
            _height = height;
            _unitOfLength = unitOfLength;
        }

        public static PaperSize Letter { get { return new PaperSize(8.5, 11, "in"); } }
        public static PaperSize Legal { get { return new PaperSize(8.5, 14, "in"); } }
        public static PaperSize A4 { get { return new PaperSize(210, 297, "mm"); } }
        public static PaperSize CSheet { get { return new PaperSize(17, 22, "in"); } }
        public static PaperSize DSheet { get { return new PaperSize(22, 34, "in"); } }
        public static PaperSize ESheet { get { return new PaperSize(34, 44, "in"); } }
        public static PaperSize LetterSmall { get { return new PaperSize(8.5, 11, "in"); } }
        public static PaperSize Tabloid { get { return new PaperSize(11, 17, "in"); } }
        public static PaperSize Ledger { get { return new PaperSize(17, 11, "in"); } }
        public static PaperSize Statement { get { return new PaperSize(5.5, 8.5, "in"); } }
        public static PaperSize Executive { get { return new PaperSize(7.25, 10.5, "in"); } }
        public static PaperSize A3 { get { return new PaperSize(297, 420, "mm"); } }
        public static PaperSize A4Small { get { return new PaperSize(210, 297, "mm"); } }
        public static PaperSize A5 { get { return new PaperSize(148, 210, "mm"); } }
        public static PaperSize B4 { get { return new PaperSize(250, 353, "mm"); } }
        public static PaperSize B5 { get { return new PaperSize(176, 250, "mm"); } }
        public static PaperSize Folio { get { return new PaperSize(8.5, 13, "in"); } }
        public static PaperSize Quarto { get { return new PaperSize(215, 275, "mm"); } }
        public static PaperSize Standard10X14 { get { return new PaperSize(10, 14, "in"); } }
        public static PaperSize Standard11X17 { get { return new PaperSize(11, 17, "in"); } }
        public static PaperSize Note { get { return new PaperSize(8.5, 11, "in"); } }
        public static PaperSize Number9Envelope { get { return new PaperSize(3.875, 8.875, "in"); } }
        public static PaperSize Number10Envelope { get { return new PaperSize(4.125, 9.5, "in"); } }
        public static PaperSize Number11Envelope { get { return new PaperSize(4.5, 10.375, "in"); } }
        public static PaperSize Number12Envelope { get { return new PaperSize(4.75, 11, "in"); } }
        public static PaperSize Number14Envelope { get { return new PaperSize(5, 11.5, "in"); } }
        public static PaperSize DlEnvelope { get { return new PaperSize(110, 220, "mm"); } }
        public static PaperSize C5Envelope { get { return new PaperSize(162, 229, "mm"); } }
        public static PaperSize C3Envelope { get { return new PaperSize(324, 458, "mm"); } }
        public static PaperSize C4Envelope { get { return new PaperSize(229, 324, "mm"); } }
        public static PaperSize C6Envelope { get { return new PaperSize(114, 162, "mm"); } }
        public static PaperSize C65Envelope { get { return new PaperSize(114, 229, "mm"); } }
        public static PaperSize B4Envelope { get { return new PaperSize(250, 353, "mm"); } }
        public static PaperSize B5Envelope { get { return new PaperSize(176, 250, "mm"); } }
        public static PaperSize B6Envelope { get { return new PaperSize(176, 125, "mm"); } }
        public static PaperSize ItalyEnvelope { get { return new PaperSize(110, 230, "mm"); } }
        public static PaperSize MonarchEnvelope { get { return new PaperSize(3.875, 7.5, "in"); } }
        public static PaperSize PersonalEnvelope { get { return new PaperSize(3.625, 6.5, "in"); } }
        public static PaperSize UsStandardFanfold { get { return new PaperSize(14.875, 11, "in"); } }
        public static PaperSize GermanStandardFanfold { get { return new PaperSize(8.5, 12, "in"); } }
        public static PaperSize GermanLegalFanfold { get { return new PaperSize(8.5, 13, "in"); } }
        public static PaperSize IsoB4 { get { return new PaperSize(250, 353, "mm"); } }
        public static PaperSize JapanesePostcard { get { return new PaperSize(100, 148, "mm"); } }
        public static PaperSize Standard9X11 { get { return new PaperSize(9, 11, "in"); } }
        public static PaperSize Standard10X11 { get { return new PaperSize(10, 11, "in"); } }
        public static PaperSize Standard15X11 { get { return new PaperSize(15, 11, "in"); } }
        public static PaperSize InviteEnvelope { get { return new PaperSize(220, 220, "mm"); } }
        public static PaperSize LetterExtra { get { return new PaperSize(9.275, 12, "in"); } }
        public static PaperSize LegalExtra { get { return new PaperSize(9.275, 15, "in"); } }
        public static PaperSize TabloidExtra { get { return new PaperSize(11.69, 18, "in"); } }
        public static PaperSize A4Extra { get { return new PaperSize(236, 322, "mm"); } }
        public static PaperSize LetterTransverse { get { return new PaperSize(8.275, 11, "in"); } }
        public static PaperSize A4Transverse { get { return new PaperSize(210, 297, "mm"); } }
        public static PaperSize LetterExtraTransverse { get { return new PaperSize(9.275, 12, "in"); } }
        public static PaperSize APlus { get { return new PaperSize(227, 356, "mm"); } }
        public static PaperSize BPlus { get { return new PaperSize(305, 487, "mm"); } }
        public static PaperSize LetterPlus { get { return new PaperSize(8.5, 12.69, "in"); } }
        public static PaperSize A4Plus { get { return new PaperSize(210, 330, "mm"); } }
        public static PaperSize A5Transverse { get { return new PaperSize(148, 210, "mm"); } }
        public static PaperSize B5Transverse { get { return new PaperSize(182, 257, "mm"); } }
        public static PaperSize A3Extra { get { return new PaperSize(322, 445, "mm"); } }
        public static PaperSize A5Extra { get { return new PaperSize(174, 235, "mm"); } }
        public static PaperSize B5Extra { get { return new PaperSize(201, 276, "mm"); } }
        public static PaperSize A2 { get { return new PaperSize(420, 594, "mm"); } }
        public static PaperSize A3Transverse { get { return new PaperSize(297, 420, "mm"); } }
        public static PaperSize A3ExtraTransverse { get { return new PaperSize(322, 445, "mm"); } }
        public static PaperSize JapaneseDoublePostcard { get { return new PaperSize(200, 148, "mm"); } }
        public static PaperSize A6 { get { return new PaperSize(105, 148, "mm"); } }
        public static PaperSize LetterRotated { get { return new PaperSize(11, 8.5, "in"); } }
        public static PaperSize A3Rotated { get { return new PaperSize(420, 297, "mm"); } }
        public static PaperSize A4Rotated { get { return new PaperSize(297, 210, "mm"); } }
        public static PaperSize A5Rotated { get { return new PaperSize(210, 148, "mm"); } }
        public static PaperSize B4JisRotated { get { return new PaperSize(364, 257, "mm"); } }
        public static PaperSize B5JisRotated { get { return new PaperSize(257, 182, "mm"); } }
        public static PaperSize JapanesePostcardRotated { get { return new PaperSize(148, 100, "mm"); } }
        public static PaperSize JapaneseDoublePostcardRotated { get { return new PaperSize(148, 200, "mm"); } }
        public static PaperSize A6Rotated { get { return new PaperSize(148, 105, "mm"); } }
        public static PaperSize B6Jis { get { return new PaperSize(128, 182, "mm"); } }
        public static PaperSize B6JisRotated { get { return new PaperSize(182, 128, "mm"); } }
        public static PaperSize Standard12X11 { get { return new PaperSize(12, 11, "in"); } }
        public static PaperSize Prc16K { get { return new PaperSize(146, 215, "mm"); } }
        public static PaperSize Prc32K { get { return new PaperSize(97, 151, "mm"); } }
        public static PaperSize Prc32KBig { get { return new PaperSize(97, 151, "mm"); } }
        public static PaperSize PrcEnvelopeNumber1 { get { return new PaperSize(102, 165, "mm"); } }
        public static PaperSize PrcEnvelopeNumber2 { get { return new PaperSize(102, 176, "mm"); } }
        public static PaperSize PrcEnvelopeNumber3 { get { return new PaperSize(125, 176, "mm"); } }
        public static PaperSize PrcEnvelopeNumber4 { get { return new PaperSize(110, 208, "mm"); } }
        public static PaperSize PrcEnvelopeNumber5 { get { return new PaperSize(110, 220, "mm"); } }
        public static PaperSize PrcEnvelopeNumber6 { get { return new PaperSize(120, 230, "mm"); } }
        public static PaperSize PrcEnvelopeNumber7 { get { return new PaperSize(160, 230, "mm"); } }
        public static PaperSize PrcEnvelopeNumber8 { get { return new PaperSize(120, 309, "mm"); } }
        public static PaperSize PrcEnvelopeNumber9 { get { return new PaperSize(229, 324, "mm"); } }
        public static PaperSize PrcEnvelopeNumber10 { get { return new PaperSize(324, 458, "mm"); } }
        public static PaperSize Prc16KRotated { get { return new PaperSize(146, 215, "mm"); } }
        public static PaperSize Prc32KRotated { get { return new PaperSize(97, 151, "mm"); } }
        public static PaperSize Prc32KBigRotated { get { return new PaperSize(97, 151, "mm"); } }
        public static PaperSize PrcEnvelopeNumber1Rotated { get { return new PaperSize(165, 102, "mm"); } }
        public static PaperSize PrcEnvelopeNumber2Rotated { get { return new PaperSize(176, 102, "mm"); } }
        public static PaperSize PrcEnvelopeNumber3Rotated { get { return new PaperSize(176, 125, "mm"); } }
        public static PaperSize PrcEnvelopeNumber4Rotated { get { return new PaperSize(208, 110, "mm"); } }
        public static PaperSize PrcEnvelopeNumber5Rotated { get { return new PaperSize(220, 110, "mm"); } }
        public static PaperSize PrcEnvelopeNumber6Rotated { get { return new PaperSize(230, 120, "mm"); } }
        public static PaperSize PrcEnvelopeNumber7Rotated { get { return new PaperSize(230, 160, "mm"); } }
        public static PaperSize PrcEnvelopeNumber8Rotated { get { return new PaperSize(309, 120, "mm"); } }
        public static PaperSize PrcEnvelopeNumber9Rotated { get { return new PaperSize(324, 229, "mm"); } }
        public static PaperSize PrcEnvelopeNumber10Rotated { get { return new PaperSize(458, 324, "mm"); } }

        public string Width
        {
            get { return string.Format("{0}{1}", _width, _unitOfLength); }
        }

        public string Height
        {
            get { return string.Format("{0}{1}", _height, _unitOfLength); }
        }

        public string UnitOfLength
        {
            get { return _unitOfLength; }
        }
    }
}