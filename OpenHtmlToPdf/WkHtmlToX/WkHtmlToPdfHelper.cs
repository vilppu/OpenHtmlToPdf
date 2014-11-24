using System;
using System.Runtime.InteropServices;
using OpenHtmlToPdf.Interop;
using OpenHtmlToPdf.Pdf;

namespace OpenHtmlToPdf.WkHtmlToX
{
    public static class WkHtmlToPdfHelper
    {
        public static byte[] Render(this WkHtmlToPdfContext wkHtmlToPdfContext, string html)
        {
            StringCallback onErrorDelegate = (converter, errorText) =>
            {
                throw new DocumentCreationFailedException(errorText);
            };

            WkHtmlToPdf.wkhtmltopdf_set_error_callback(wkHtmlToPdfContext.ConverterPointer, onErrorDelegate);
            WkHtmlToPdf.wkhtmltopdf_add_object(wkHtmlToPdfContext.ConverterPointer, wkHtmlToPdfContext.ObjectSettingsPointer, html);
            WkHtmlToPdf.wkhtmltopdf_convert(wkHtmlToPdfContext.ConverterPointer);

            return wkHtmlToPdfContext.ConvertedDocument();
        }

        private static byte[] ConvertedDocument(this WkHtmlToPdfContext wkHtmlToPdfContext)
        {
            IntPtr outputPointer;

            var outputLength = WkHtmlToPdf.wkhtmltopdf_get_output(wkHtmlToPdfContext.ConverterPointer, out outputPointer);

            return GetBytesFromUnmanagedArray(outputPointer, outputLength);
        }

        private static byte[] GetBytesFromUnmanagedArray(IntPtr pointerToArray, int arrayLength)
        {
            var bytes = new byte[arrayLength];

            Marshal.Copy(pointerToArray, bytes, 0, bytes.Length);

            return bytes;
        }
    }
}