using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenHtmlToPdf.Pdf
{
    public sealed class Document
    {
        public static IPdfDocument From(string html)
        {
            return DocumentBuilder.Containing(html);
        }

        private sealed class DocumentBuilder : IPdfDocument
        {
            private static readonly object SyncRoot = new object();
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
                lock (SyncRoot)
                {
                    var domain = AnotherDomain();

                    try
                    {
                        return RenderOperationIn(domain).Render(_html, _globalSettings, _objectSettings);
                    }
                    finally
                    {
                        AppDomain.Unload(domain);
                    }
                }
            }

            private static AppDomain AnotherDomain()
            {
                return AppDomain.CreateDomain(
                    Guid.NewGuid().ToString("N"),
                    AppDomain.CurrentDomain.Evidence,
                    AppDomain.CurrentDomain.SetupInformation);
            }

            private static RenderOperation RenderOperationIn(AppDomain domain)
            {
                return (RenderOperation)domain.CreateInstanceAndUnwrap(
                    Assembly.GetExecutingAssembly().FullName,
                    typeof(RenderOperation).FullName
                    );
            }
        }
    }
}
