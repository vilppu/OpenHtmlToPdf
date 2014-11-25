using System;

namespace OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX
{
    sealed class ConversionFailedException : Exception
    {
        public ConversionFailedException(string errorText)
            : base(errorText)
        {
        }
    }
}