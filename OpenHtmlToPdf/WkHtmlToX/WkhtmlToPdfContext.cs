using System;
using System.Runtime.InteropServices;
using OpenHtmlToPdf.Exceptions;
using OpenHtmlToPdf.Native;

namespace OpenHtmlToPdf.WkHtmlToX
{
    public sealed class WkhtmlToPdfContext : IDisposable
    {
        private const int UseX11Graphics = 0;
        private readonly IntPtr _globalSettingsPointer;
        private readonly IntPtr _converterPointer;
        private static NativeLibrary _wkHtmlToXLibrary;
        private readonly StringCallback _onErrorDelegate = OnError;

        private WkhtmlToPdfContext(IntPtr globalSettingsPointer, IntPtr converterPointer)
        {
            _globalSettingsPointer = globalSettingsPointer;
            _converterPointer = converterPointer;
        }

        public static WkhtmlToPdfContext CreateWith(string html)
        {
            _wkHtmlToXLibrary = WkHtmlToPdfLibrary.Load();

            if (Wkhtmltox.wkhtmltopdf_init(UseX11Graphics) == 0)
                throw new HtmlToPdfConversionFailedException("wkhtmltopdf_init failed");

            var globalSettingsPointer = Wkhtmltox.wkhtmltopdf_create_global_settings();
            var converterPointer = Wkhtmltox.wkhtmltopdf_create_converter(globalSettingsPointer);
            var objectSettings = Wkhtmltox.wkhtmltopdf_create_object_settings();

            Wkhtmltox.wkhtmltopdf_add_object(converterPointer, objectSettings, html);

            return new WkhtmlToPdfContext(globalSettingsPointer, converterPointer);
        }

        public byte[] RenderPdf()
        {
            Wkhtmltox.wkhtmltopdf_set_error_callback(_converterPointer, _onErrorDelegate);

            if (Wkhtmltox.wkhtmltopdf_convert(_converterPointer) == 0)
                throw new HtmlToPdfConversionFailedException("wkhtmltopdf_convert failed");

            IntPtr outputPointer;

            var outputLength = Wkhtmltox.wkhtmltopdf_get_output(_converterPointer, out outputPointer);
            
            return GetBytesFromUnmanagedArray(outputPointer, outputLength);
        }

        public void Dispose()
        {
            Wkhtmltox.wkhtmltopdf_deinit();
            _wkHtmlToXLibrary.Dispose();
        }

        public IntPtr GlobalSettingsPointer
        {
            get { return _globalSettingsPointer; }
        }

        public IntPtr ConverterPointer
        {
            get { return _converterPointer; }
        }

        private static byte[] GetBytesFromUnmanagedArray(IntPtr pointerToArray, int arrayLength)
        {
            var bytes = new byte[arrayLength];

            Marshal.Copy(pointerToArray, bytes, 0, bytes.Length);

            return bytes;
        }

        private static void OnError(IntPtr converter, string errorText)
        {
            throw new HtmlToPdfConversionFailedException(errorText);
        }
    }
}