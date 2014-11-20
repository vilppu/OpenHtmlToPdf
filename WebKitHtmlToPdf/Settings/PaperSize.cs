using System;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace WebKitHtmlToPdf.Settings
{ 
    [Serializable]
    class PaperSize
    {
        private static readonly Dictionary<PaperKind, PaperSize> Dictionary = new Dictionary<PaperKind, PaperSize>
        {
            // paper sizes from http://msdn.microsoft.com/en-us/library/system.drawing.printing.paperkind.aspx
            { PaperKind.Letter, new PaperSize("8.5in", "11in") },
            { PaperKind.Legal, new PaperSize("8.5in", "14in") },
            { PaperKind.A4, new PaperSize("210mm", "297mm") },
            { PaperKind.CSheet, new PaperSize("17in", "22in") },
            { PaperKind.DSheet, new PaperSize("22in", "34in") },
            { PaperKind.ESheet, new PaperSize("34in", "44in") },
            { PaperKind.LetterSmall, new PaperSize("8.5in", "11in") },
            { PaperKind.Tabloid, new PaperSize("11in", "17in") },
            { PaperKind.Ledger, new PaperSize("17in", "11in") },
            { PaperKind.Statement, new PaperSize("5.5in", "8.5in") },
            { PaperKind.Executive, new PaperSize("7.25in", "10.5in") },
            { PaperKind.A3, new PaperSize("297mm", "420mm") },
            { PaperKind.A4Small, new PaperSize("210mm", "297mm") },
            { PaperKind.A5, new PaperSize("148mm", "210mm") },
            { PaperKind.B4, new PaperSize("250mm", "353mm") },
            { PaperKind.B5, new PaperSize("176mm", "250mm") },
            { PaperKind.Folio, new PaperSize("8.5in", "13in") },
            { PaperKind.Quarto, new PaperSize("215mm", "275mm") },
            { PaperKind.Standard10x14, new PaperSize("10in", "14in") },
            { PaperKind.Standard11x17, new PaperSize("11in", "17in") },
            { PaperKind.Note, new PaperSize("8.5in", "11in") },
            { PaperKind.Number9Envelope, new PaperSize("3.875in", "8.875in") },
            { PaperKind.Number10Envelope, new PaperSize("4.125in", "9.5in") },
            { PaperKind.Number11Envelope, new PaperSize("4.5in", "10.375in") },
            { PaperKind.Number12Envelope, new PaperSize("4.75in", "11in") },
            { PaperKind.Number14Envelope, new PaperSize("5in", "11.5in") },
            { PaperKind.DLEnvelope, new PaperSize("110mm", "220mm") },
            { PaperKind.C5Envelope, new PaperSize("162mm", "229mm") },
            { PaperKind.C3Envelope, new PaperSize("324mm", "458mm") },
            { PaperKind.C4Envelope, new PaperSize("229mm", "324mm") },
            { PaperKind.C6Envelope, new PaperSize("114mm", "162mm") },
            { PaperKind.C65Envelope, new PaperSize("114mm", "229mm") },
            { PaperKind.B4Envelope, new PaperSize("250mm", "353mm") },
            { PaperKind.B5Envelope, new PaperSize("176mm", "250mm") },
            { PaperKind.B6Envelope, new PaperSize("176mm", "125mm") },
            { PaperKind.ItalyEnvelope, new PaperSize("110mm", "230mm") },
            { PaperKind.MonarchEnvelope, new PaperSize("3.875in", "7.5in") },
            { PaperKind.PersonalEnvelope, new PaperSize("3.625in", "6.5in") },
            { PaperKind.USStandardFanfold, new PaperSize("14.875in", "11in") },
            { PaperKind.GermanStandardFanfold, new PaperSize("8.5in", "12in") },
            { PaperKind.GermanLegalFanfold, new PaperSize("8.5in", "13in") },
            { PaperKind.IsoB4, new PaperSize("250mm", "353mm") },
            { PaperKind.JapanesePostcard, new PaperSize("100mm", "148mm") },
            { PaperKind.Standard9x11, new PaperSize("9in", "11in") },
            { PaperKind.Standard10x11, new PaperSize("10in", "11in") },
            { PaperKind.Standard15x11, new PaperSize("15in", "11in") },
            { PaperKind.InviteEnvelope, new PaperSize("220mm", "220mm") },
            { PaperKind.LetterExtra, new PaperSize("9.275in", "12in") },
            { PaperKind.LegalExtra, new PaperSize("9.275in", "15in") },
            { PaperKind.TabloidExtra, new PaperSize("11.69in", "18in") },
            { PaperKind.A4Extra, new PaperSize("236mm", "322mm") },
            { PaperKind.LetterTransverse, new PaperSize("8.275in", "11in") },
            { PaperKind.A4Transverse, new PaperSize("210mm", "297mm") },
            { PaperKind.LetterExtraTransverse, new PaperSize("9.275in", "12in") },
            { PaperKind.APlus, new PaperSize("227mm", "356mm") },
            { PaperKind.BPlus, new PaperSize("305mm", "487mm") },
            { PaperKind.LetterPlus, new PaperSize("8.5in", "12.69in") },
            { PaperKind.A4Plus, new PaperSize("210mm", "330mm") },
            { PaperKind.A5Transverse, new PaperSize("148mm", "210mm") },
            { PaperKind.B5Transverse, new PaperSize("182mm", "257mm") },
            { PaperKind.A3Extra, new PaperSize("322mm", "445mm") },
            { PaperKind.A5Extra, new PaperSize("174mm", "235mm") },
            { PaperKind.B5Extra, new PaperSize("201mm", "276mm") },
            { PaperKind.A2, new PaperSize("420mm", "594mm") },
            { PaperKind.A3Transverse, new PaperSize("297mm", "420mm") },
            { PaperKind.A3ExtraTransverse, new PaperSize("322mm", "445mm") },
            { PaperKind.JapaneseDoublePostcard, new PaperSize("200mm", "148mm") },
            { PaperKind.A6, new PaperSize("105mm", "148mm") },
            { PaperKind.LetterRotated, new PaperSize("11in", "8.5in") },
            { PaperKind.A3Rotated, new PaperSize("420mm", "297mm") },
            { PaperKind.A4Rotated, new PaperSize("297mm", "210mm") },
            { PaperKind.A5Rotated, new PaperSize("210mm", "148mm") },
            { PaperKind.B4JisRotated, new PaperSize("364mm", "257mm") },
            { PaperKind.B5JisRotated, new PaperSize("257mm", "182mm") },
            { PaperKind.JapanesePostcardRotated, new PaperSize("148mm", "100mm") },
            { PaperKind.JapaneseDoublePostcardRotated, new PaperSize("148mm", "200mm") },
            { PaperKind.A6Rotated, new PaperSize("148mm", "105mm") },
            { PaperKind.B6Jis, new PaperSize("128mm", "182mm") },
            { PaperKind.B6JisRotated, new PaperSize("182mm", "128mm") },
            { PaperKind.Standard12x11, new PaperSize("12in", "11in") },
            { PaperKind.Prc16K, new PaperSize("146mm", "215mm") },
            { PaperKind.Prc32K, new PaperSize("97mm", "151mm") },
            { PaperKind.Prc32KBig, new PaperSize("97mm", "151mm") },
            { PaperKind.PrcEnvelopeNumber1, new PaperSize("102mm", "165mm") },
            { PaperKind.PrcEnvelopeNumber2, new PaperSize("102mm", "176mm") },
            { PaperKind.PrcEnvelopeNumber3, new PaperSize("125mm", "176mm") },
            { PaperKind.PrcEnvelopeNumber4, new PaperSize("110mm", "208mm") },
            { PaperKind.PrcEnvelopeNumber5, new PaperSize("110mm", "220mm") },
            { PaperKind.PrcEnvelopeNumber6, new PaperSize("120mm", "230mm") },
            { PaperKind.PrcEnvelopeNumber7, new PaperSize("160mm", "230mm") },
            { PaperKind.PrcEnvelopeNumber8, new PaperSize("120mm", "309mm") },
            { PaperKind.PrcEnvelopeNumber9, new PaperSize("229mm", "324mm") },
            { PaperKind.PrcEnvelopeNumber10, new PaperSize("324mm", "458mm") },
            { PaperKind.Prc16KRotated, new PaperSize("146mm", "215mm") },
            { PaperKind.Prc32KRotated, new PaperSize("97mm", "151mm") },
            { PaperKind.Prc32KBigRotated, new PaperSize("97mm", "151mm") },
            { PaperKind.PrcEnvelopeNumber1Rotated, new PaperSize("165mm", "102mm") },
            { PaperKind.PrcEnvelopeNumber2Rotated, new PaperSize("176mm", "102mm") },
            { PaperKind.PrcEnvelopeNumber3Rotated, new PaperSize("176mm", "125mm") },
            { PaperKind.PrcEnvelopeNumber4Rotated, new PaperSize("208mm", "110mm") },
            { PaperKind.PrcEnvelopeNumber5Rotated, new PaperSize("220mm", "110mm") },
            { PaperKind.PrcEnvelopeNumber6Rotated, new PaperSize("230mm", "120mm") },
            { PaperKind.PrcEnvelopeNumber7Rotated, new PaperSize("230mm", "160mm") },
            { PaperKind.PrcEnvelopeNumber8Rotated, new PaperSize("309mm", "120mm") },
            { PaperKind.PrcEnvelopeNumber9Rotated, new PaperSize("324mm", "229mm") },
            { PaperKind.PrcEnvelopeNumber10Rotated, new PaperSize("458mm", "324mm") },
        };

        public PaperSize(string width, string height)
        {
            Width = width;
            Height = height;
        }

        public string Height { get; private set; }

        public string Width { get; private set; }

        public static implicit operator PaperSize(PaperKind paperKind)
        {
            return Dictionary[paperKind];
        }
    }
}