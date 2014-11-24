using System;

namespace OpenHtmlToPdf.Pdf
{
    public sealed class DocumentCreationFailedException : Exception
    {
        internal DocumentCreationFailedException(string message)
            : base(message)
        {
        }
    }
}
