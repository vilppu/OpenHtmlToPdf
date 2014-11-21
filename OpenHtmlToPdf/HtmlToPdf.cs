using System;
using System.Runtime.InteropServices;
using OpenHtmlToPdf.Exceptions;
using OpenHtmlToPdf.Native;
using OpenHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf
{
    sealed class HtmlToPdf : IDisposable
    {
        private readonly StringCallback _onErrorDelegate = OnError;
        private readonly NativeLibrary _wkHtmlToXLibrary;
        private const int UseX11Graphics = 0;
        private IntPtr _converter;

        private HtmlToPdf(NativeLibrary wkHtmlToXLibrary)
        {
            _wkHtmlToXLibrary = wkHtmlToXLibrary;
        }

        public static HtmlToPdf Create()
        {
            var wkHtmlToXLibrary = WkHtmlToPdfLibrary.Load();

            Wkhtmltox.wkhtmltopdf_init(UseX11Graphics);

            return new HtmlToPdf(wkHtmlToXLibrary);
        }

        public void Dispose()
        {
            Wkhtmltox.wkhtmltopdf_deinit();

            _wkHtmlToXLibrary.Dispose();
        }

        public byte[] ConvertToPdf(PdfDocumentSettings documentSettings)
        {
            _converter = documentSettings.ApplyToConverter();

            Wkhtmltox.wkhtmltopdf_set_error_callback(_converter, _onErrorDelegate);

            if (Wkhtmltox.wkhtmltopdf_convert(_converter) == 0)
            {
                return null;
            }

            IntPtr tmp;
            var len = Wkhtmltox.wkhtmltopdf_get_output(_converter, out tmp);
            var result = new byte[len];
            Marshal.Copy(tmp, result, 0, result.Length);
            Wkhtmltox.wkhtmltopdf_destroy_converter(_converter);

            return result;
        }

        private static void OnError(IntPtr converter, string errorText)
        {
            throw new HtmlToPdfConversionFailedException(errorText);
        }
    }
}