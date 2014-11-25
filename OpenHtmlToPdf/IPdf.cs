using System;
using OpenHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf
{
    public interface IPdf
    {
        IPdf BeforeRender(Action<WkhtmlToPdfContext> beforeRender);
        byte[] Content();
    }
}