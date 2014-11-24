using System;
using OpenHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf.Pdf
{
    public interface IPdfDocument
    {
        IPdfDocument BeforeRender(Action<WkHtmlToPdfContext> beforeRender);

        byte[] Content();
    }
}