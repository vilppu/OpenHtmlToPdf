using System;
using System.Collections.Generic;
using System.Linq;
using OpenHtmlToPdf.WkHtmlToX;

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
            private readonly IEnumerable<Action<WkHtmlToPdfContext>> _beforeRender;

            private DocumentBuilder(string html, IEnumerable<Action<WkHtmlToPdfContext>> beforeRender)
            {
                _html = html;
                _beforeRender = beforeRender;
            }

            public static DocumentBuilder Containing(string html)
            {
                return new DocumentBuilder(html,
                    Enumerable.Empty<Action<WkHtmlToPdfContext>>());
            }

            public IPdfDocument BeforeRender(Action<WkHtmlToPdfContext> beforeRender)
            {
                return new DocumentBuilder(
                    _html,
                    _beforeRender.Concat(new[] { beforeRender }));
            }

            public byte[] Content()
            {
                lock (SyncRoot)
                {
                    using (var wkhtmlToPdfContext = WkHtmlToPdfContext.Create())
                    {
                        foreach (var beforeRender in _beforeRender)
                            beforeRender(wkhtmlToPdfContext);

                        return wkhtmlToPdfContext.Render(_html);
                    }
                }
            }
        }
    }
}
