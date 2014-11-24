using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using OpenHtmlToPdf.Native;
using OpenHtmlToPdf.Settings;

namespace OpenHtmlToPdf
{
    class PdfDocumentSettings
    {
        private readonly GlobalSettings _globalSettings = new GlobalSettings();
        private readonly List<ObjectSettings> _objectSettings = new List<ObjectSettings>();

        public static PdfDocumentSettings From(string html)
        {
            return new PdfDocumentSettings
            {
                _globalSettings =
                {
                    ProduceOutline = true,
                    DocumentTitle = "",
                    PaperSize = PaperKind.A4,
                    Margins =
                    {
                        All = 1.375,
                        Unit = Unit.Centimeters
                    }
                },
                _objectSettings = 
                {
                    new ObjectSettings { HtmlText = html },
                }
            };
        }


        public IntPtr ApplyToConverter()
        {
            var config = Wkhtmltox.wkhtmltopdf_create_global_settings();

            WkHtmlToPdfSettings.ApplySettings(config, _globalSettings, true);

            var converter =  Wkhtmltox.wkhtmltopdf_create_converter(config);

            foreach (var setting in _objectSettings)
                setting.ApplyToConverter(converter);

            return converter;
        }
    }
}