using System;
using System.Collections.Generic;
using System.Linq;
using OpenHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf
{
    public sealed class Pdf
    {
        public static IPdf From(string html)
        {
            return PdfDocumentBuilder.With(html);
        }

        private sealed class PdfDocumentBuilder : IPdf
        {
            private static readonly object SyncRoot = new object();
            private readonly string _html;
            private readonly IEnumerable<Action<WkhtmlToPdfContext>> _beforeRender;
            private PdfDocumentBuilder(string html, IEnumerable<Action<WkhtmlToPdfContext>> beforeRender)
            {
                _html = html;
                _beforeRender = beforeRender;
            }

            public static PdfDocumentBuilder With(string html)
            {
                return new PdfDocumentBuilder(html,
                    Enumerable.Empty<Action<WkhtmlToPdfContext>>());
            }

            public IPdf BeforeRender(Action<WkhtmlToPdfContext> beforeRender)
            {
                return new PdfDocumentBuilder(
                    _html,
                    _beforeRender.Concat(new[] { beforeRender }));
            }

            public byte[] Content()
            {
                lock (SyncRoot)
                {
                    using (var wkhtmlToPdfContext = WkhtmlToPdfContext.CreateWith(_html))
                    {
                        foreach (var beforeRender in _beforeRender)
                            beforeRender(wkhtmlToPdfContext);

                        return wkhtmlToPdfContext.RenderPdf();
                    }
                }
            }
        }
    }
}
