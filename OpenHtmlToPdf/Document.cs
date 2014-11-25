using System.Collections.Generic;
using System.Linq;

namespace OpenHtmlToPdf
{
    public sealed class Document
    {
        public static IPdfDocument From(string html)
        {
            return DocumentBuilder.Containing(html);
        }

        private sealed class DocumentBuilder : IPdfDocument
        {
            private readonly string _html;
            private readonly IDictionary<string, string> _globalSettings;
            private readonly IDictionary<string, string> _objectSettings;

            private DocumentBuilder(string html, IDictionary<string, string> globalSettings, IDictionary<string, string> objectSettings)
            {
                _html = html;
                _globalSettings = globalSettings;
                _objectSettings = objectSettings;
            }

            public static DocumentBuilder Containing(string html)
            {
                return new DocumentBuilder(
                    html,
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>());
            }

            public IPdfDocument WithGlobalSetting(string key, string value)
            {
                var globalSettings = _globalSettings.ToDictionary(e => e.Key, e => e.Value);

                globalSettings[key] = value;

                return new DocumentBuilder(_html, globalSettings, _objectSettings);
            }

            public IPdfDocument WithObjectSetting(string key, string value)
            {
                var objectSetting = _objectSettings.ToDictionary(e => e.Key, e => e.Value);

                objectSetting[key] = value;

                return new DocumentBuilder(_html, _globalSettings, objectSetting);
            }

            public byte[] Content()
            {
                return HtmlToPdfConverterProcess.ConvertToPdf(_html, _globalSettings, _objectSettings);
            }
        }
    }
}
