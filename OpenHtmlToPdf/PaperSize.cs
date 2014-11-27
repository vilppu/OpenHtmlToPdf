namespace OpenHtmlToPdf
{
    public sealed class PaperSize
    {
        private readonly Length _width;
        private readonly Length _height;

        public PaperSize(Length width, Length height)
        {
            _width = width;
            _height = height;
        }

        public static PaperSize Letter { get { return new PaperSize(8.5.Inches(), 11.Inches()); } }
        public static PaperSize Legal { get { return new PaperSize(8.5.Inches(), 14.Inches()); } }
        public static PaperSize A4 { get { return new PaperSize(210.Millimeters(), 297.Millimeters()); } }
        public static PaperSize CSheet { get { return new PaperSize(17.Inches(), 22.Inches()); } }
        public static PaperSize DSheet { get { return new PaperSize(22.Inches(), 34.Inches()); } }
        public static PaperSize ESheet { get { return new PaperSize(34.Inches(), 44.Inches()); } }
        public static PaperSize LetterSmall { get { return new PaperSize(8.5.Inches(), 11.Inches()); } }
        public static PaperSize Tabloid { get { return new PaperSize(11.Inches(), 17.Inches()); } }
        public static PaperSize Ledger { get { return new PaperSize(17.Inches(), 11.Inches()); } }
        public static PaperSize Statement { get { return new PaperSize(5.5.Inches(), 8.5.Inches()); } }
        public static PaperSize Executive { get { return new PaperSize(7.25.Inches(), 10.5.Inches()); } }
        public static PaperSize A3 { get { return new PaperSize(297.Millimeters(), 420.Millimeters()); } }
        public static PaperSize A4Small { get { return new PaperSize(210.Millimeters(), 297.Millimeters()); } }
        public static PaperSize A5 { get { return new PaperSize(148.Millimeters(), 210.Millimeters()); } }
        public static PaperSize B4 { get { return new PaperSize(250.Millimeters(), 353.Millimeters()); } }
        public static PaperSize B5 { get { return new PaperSize(176.Millimeters(), 250.Millimeters()); } }
        public static PaperSize Folio { get { return new PaperSize(8.5.Inches(), 13.Inches()); } }
        public static PaperSize Quarto { get { return new PaperSize(215.Millimeters(), 275.Millimeters()); } }
        public static PaperSize Standard10X14 { get { return new PaperSize(10.Inches(), 14.Inches()); } }
        public static PaperSize Standard11X17 { get { return new PaperSize(11.Inches(), 17.Inches()); } }
        public static PaperSize Note { get { return new PaperSize(8.5.Inches(), 11.Inches()); } }
        public static PaperSize Number9Envelope { get { return new PaperSize(3.875.Inches(), 8.875.Inches()); } }
        public static PaperSize Number10Envelope { get { return new PaperSize(4.125.Inches(), 9.5.Inches()); } }
        public static PaperSize Number11Envelope { get { return new PaperSize(4.5.Inches(), 10.375.Inches()); } }
        public static PaperSize Number12Envelope { get { return new PaperSize(4.75.Inches(), 11.Inches()); } }
        public static PaperSize Number14Envelope { get { return new PaperSize(5.Inches(), 11.5.Inches()); } }
        public static PaperSize DlEnvelope { get { return new PaperSize(110.Millimeters(), 220.Millimeters()); } }
        public static PaperSize C5Envelope { get { return new PaperSize(162.Millimeters(), 229.Millimeters()); } }
        public static PaperSize C3Envelope { get { return new PaperSize(324.Millimeters(), 458.Millimeters()); } }
        public static PaperSize C4Envelope { get { return new PaperSize(229.Millimeters(), 324.Millimeters()); } }
        public static PaperSize C6Envelope { get { return new PaperSize(114.Millimeters(), 162.Millimeters()); } }
        public static PaperSize C65Envelope { get { return new PaperSize(114.Millimeters(), 229.Millimeters()); } }
        public static PaperSize B4Envelope { get { return new PaperSize(250.Millimeters(), 353.Millimeters()); } }
        public static PaperSize B5Envelope { get { return new PaperSize(176.Millimeters(), 250.Millimeters()); } }
        public static PaperSize B6Envelope { get { return new PaperSize(176.Millimeters(), 125.Millimeters()); } }
        public static PaperSize ItalyEnvelope { get { return new PaperSize(110.Millimeters(), 230.Millimeters()); } }
        public static PaperSize MonarchEnvelope { get { return new PaperSize(3.875.Inches(), 7.5.Inches()); } }
        public static PaperSize PersonalEnvelope { get { return new PaperSize(3.625.Inches(), 6.5.Inches()); } }
        public static PaperSize UsStandardFanfold { get { return new PaperSize(14.875.Inches(), 11.Inches()); } }
        public static PaperSize GermanStandardFanfold { get { return new PaperSize(8.5.Inches(), 12.Inches()); } }
        public static PaperSize GermanLegalFanfold { get { return new PaperSize(8.5.Inches(), 13.Inches()); } }
        public static PaperSize IsoB4 { get { return new PaperSize(250.Millimeters(), 353.Millimeters()); } }
        public static PaperSize JapanesePostcard { get { return new PaperSize(100.Millimeters(), 148.Millimeters()); } }
        public static PaperSize Standard9X11 { get { return new PaperSize(9.Inches(), 11.Inches()); } }
        public static PaperSize Standard10X11 { get { return new PaperSize(10.Inches(), 11.Inches()); } }
        public static PaperSize Standard15X11 { get { return new PaperSize(15.Inches(), 11.Inches()); } }
        public static PaperSize InviteEnvelope { get { return new PaperSize(220.Millimeters(), 220.Millimeters()); } }
        public static PaperSize LetterExtra { get { return new PaperSize(9.275.Inches(), 12.Inches()); } }
        public static PaperSize LegalExtra { get { return new PaperSize(9.275.Inches(), 15.Inches()); } }
        public static PaperSize TabloidExtra { get { return new PaperSize(11.69.Inches(), 18.Inches()); } }
        public static PaperSize A4Extra { get { return new PaperSize(236.Millimeters(), 322.Millimeters()); } }
        public static PaperSize LetterTransverse { get { return new PaperSize(8.275.Inches(), 11.Inches()); } }
        public static PaperSize A4Transverse { get { return new PaperSize(210.Millimeters(), 297.Millimeters()); } }
        public static PaperSize LetterExtraTransverse { get { return new PaperSize(9.275.Inches(), 12.Inches()); } }
        public static PaperSize APlus { get { return new PaperSize(227.Millimeters(), 356.Millimeters()); } }
        public static PaperSize BPlus { get { return new PaperSize(305.Millimeters(), 487.Millimeters()); } }
        public static PaperSize LetterPlus { get { return new PaperSize(8.5.Inches(), 12.69.Inches()); } }
        public static PaperSize A4Plus { get { return new PaperSize(210.Millimeters(), 330.Millimeters()); } }
        public static PaperSize A5Transverse { get { return new PaperSize(148.Millimeters(), 210.Millimeters()); } }
        public static PaperSize B5Transverse { get { return new PaperSize(182.Millimeters(), 257.Millimeters()); } }
        public static PaperSize A3Extra { get { return new PaperSize(322.Millimeters(), 445.Millimeters()); } }
        public static PaperSize A5Extra { get { return new PaperSize(174.Millimeters(), 235.Millimeters()); } }
        public static PaperSize B5Extra { get { return new PaperSize(201.Millimeters(), 276.Millimeters()); } }
        public static PaperSize A2 { get { return new PaperSize(420.Millimeters(), 594.Millimeters()); } }
        public static PaperSize A3Transverse { get { return new PaperSize(297.Millimeters(), 420.Millimeters()); } }
        public static PaperSize A3ExtraTransverse { get { return new PaperSize(322.Millimeters(), 445.Millimeters()); } }
        public static PaperSize JapaneseDoublePostcard { get { return new PaperSize(200.Millimeters(), 148.Millimeters()); } }
        public static PaperSize A6 { get { return new PaperSize(105.Millimeters(), 148.Millimeters()); } }
        public static PaperSize LetterRotated { get { return new PaperSize(11.Inches(), 8.5.Inches()); } }
        public static PaperSize A3Rotated { get { return new PaperSize(420.Millimeters(), 297.Millimeters()); } }
        public static PaperSize A4Rotated { get { return new PaperSize(297.Millimeters(), 210.Millimeters()); } }
        public static PaperSize A5Rotated { get { return new PaperSize(210.Millimeters(), 148.Millimeters()); } }
        public static PaperSize B4JisRotated { get { return new PaperSize(364.Millimeters(), 257.Millimeters()); } }
        public static PaperSize B5JisRotated { get { return new PaperSize(257.Millimeters(), 182.Millimeters()); } }
        public static PaperSize JapanesePostcardRotated { get { return new PaperSize(148.Millimeters(), 100.Millimeters()); } }
        public static PaperSize JapaneseDoublePostcardRotated { get { return new PaperSize(148.Millimeters(), 200.Millimeters()); } }
        public static PaperSize A6Rotated { get { return new PaperSize(148.Millimeters(), 105.Millimeters()); } }
        public static PaperSize B6Jis { get { return new PaperSize(128.Millimeters(), 182.Millimeters()); } }
        public static PaperSize B6JisRotated { get { return new PaperSize(182.Millimeters(), 128.Millimeters()); } }
        public static PaperSize Standard12X11 { get { return new PaperSize(12.Inches(), 11.Inches()); } }
        public static PaperSize Prc16K { get { return new PaperSize(146.Millimeters(), 215.Millimeters()); } }
        public static PaperSize Prc32K { get { return new PaperSize(97.Millimeters(), 151.Millimeters()); } }
        public static PaperSize Prc32KBig { get { return new PaperSize(97.Millimeters(), 151.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber1 { get { return new PaperSize(102.Millimeters(), 165.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber2 { get { return new PaperSize(102.Millimeters(), 176.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber3 { get { return new PaperSize(125.Millimeters(), 176.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber4 { get { return new PaperSize(110.Millimeters(), 208.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber5 { get { return new PaperSize(110.Millimeters(), 220.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber6 { get { return new PaperSize(120.Millimeters(), 230.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber7 { get { return new PaperSize(160.Millimeters(), 230.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber8 { get { return new PaperSize(120.Millimeters(), 309.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber9 { get { return new PaperSize(229.Millimeters(), 324.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber10 { get { return new PaperSize(324.Millimeters(), 458.Millimeters()); } }
        public static PaperSize Prc16KRotated { get { return new PaperSize(146.Millimeters(), 215.Millimeters()); } }
        public static PaperSize Prc32KRotated { get { return new PaperSize(97.Millimeters(), 151.Millimeters()); } }
        public static PaperSize Prc32KBigRotated { get { return new PaperSize(97.Millimeters(), 151.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber1Rotated { get { return new PaperSize(165.Millimeters(), 102.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber2Rotated { get { return new PaperSize(176.Millimeters(), 102.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber3Rotated { get { return new PaperSize(176.Millimeters(), 125.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber4Rotated { get { return new PaperSize(208.Millimeters(), 110.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber5Rotated { get { return new PaperSize(220.Millimeters(), 110.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber6Rotated { get { return new PaperSize(230.Millimeters(), 120.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber7Rotated { get { return new PaperSize(230.Millimeters(), 160.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber8Rotated { get { return new PaperSize(309.Millimeters(), 120.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber9Rotated { get { return new PaperSize(324.Millimeters(), 229.Millimeters()); } }
        public static PaperSize PrcEnvelopeNumber10Rotated { get { return new PaperSize(458.Millimeters(), 324.Millimeters()); } }

        public string Width
        {
            get { return _width.SettingString; }
        }

        public string Height
        {
            get { return _height.SettingString; }
        }
    }
}