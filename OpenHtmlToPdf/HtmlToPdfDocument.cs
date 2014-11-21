using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using OpenHtmlToPdf.Settings;
using OpenHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf
{
    class HtmlToPdfDocument
    {
        private readonly GlobalSettings _globalSettings = new GlobalSettings();
        private readonly List<ObjectSettings> _objectSettings = new List<ObjectSettings>();

        public static HtmlToPdfDocument From(string html)
        {
            return new HtmlToPdfDocument
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


        internal IntPtr ApplyToConverter(WkHtmlToPdf wkHtmlToPdf)
        {
            var config = wkHtmlToPdf.CreateGlobalSetting();

            wkHtmlToPdf.ApplySettings(config, _globalSettings, true);

            var converter = wkHtmlToPdf.CreateConverter(config);

            foreach (var setting in _objectSettings)
                setting.ApplyToConverter(wkHtmlToPdf, converter);

            return converter;
        }
    }
}