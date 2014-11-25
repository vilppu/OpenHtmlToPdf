using System;

namespace OpenHtmlToPdf.Exceptions
{
    public sealed class HtmlToPdfConversionFailedException : Exception
    {
        internal HtmlToPdfConversionFailedException(string message)
            : base(message)
        {
        }
    }
}
