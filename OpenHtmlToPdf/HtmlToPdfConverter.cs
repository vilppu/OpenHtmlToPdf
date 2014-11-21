using System;
using OpenHtmlToPdf.Exceptions;
using OpenHtmlToPdf.Native;
using OpenHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf
{
    sealed class HtmlToPdfConverter : IDisposable
    {
        private readonly StringCallback _onErrorDelegate = OnError;
        private readonly WkHtmlToPdf _wkHtmlToPdf;
        private IntPtr _converter;

        private HtmlToPdfConverter(WkHtmlToPdf wkHtmlToPdf)
        {
            _wkHtmlToPdf = wkHtmlToPdf;
        }

        public static HtmlToPdfConverter Create()
        {
            return new HtmlToPdfConverter(WkHtmlToPdf.Create());
        }

        public void Dispose()
        {
            _wkHtmlToPdf.Dispose();
        }

        public byte[] ConvertToPdf(HtmlToPdfDocument document)
        {
            _converter = document.ApplyToConverter(_wkHtmlToPdf);

            _wkHtmlToPdf.SetErrorCallback(_converter, _onErrorDelegate);

            if (!_wkHtmlToPdf.PerformConversion(_converter))
            {
                return null;
            }

            var result = _wkHtmlToPdf.GetConverterResult(_converter);

            _wkHtmlToPdf.DestroyConverter(_converter);

            return result;
        }

        private static void OnError(IntPtr converter, string errorText)
        {
            throw new HtmlToPdfConversionFailedException(errorText);
        }
    }
}