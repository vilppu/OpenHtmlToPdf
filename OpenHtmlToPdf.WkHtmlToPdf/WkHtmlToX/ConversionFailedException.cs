using System;

namespace OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX
{
    public sealed class ConversionFailedException : Exception
    {
        public ConversionFailedException(string errorText)
            : base(errorText)
        {
        }
    }
}