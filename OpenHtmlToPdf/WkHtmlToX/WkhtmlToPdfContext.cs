using System;
using OpenHtmlToPdf.Interop;

namespace OpenHtmlToPdf.WkHtmlToX
{
    public sealed class WkHtmlToPdfContext : IDisposable
    {
        private const int UseX11Graphics = 0;
        private readonly IntPtr _globalSettingsPointer;
        private readonly IntPtr _converterPointer;
        private readonly NativeLibrary _wkHtmlToXLibrary;
        private readonly IntPtr _objectSettings;

        private WkHtmlToPdfContext(IntPtr globalSettingsPointer, IntPtr converterPointer, NativeLibrary wkHtmlToXLibrary, IntPtr objectSettings)
        {
            _globalSettingsPointer = globalSettingsPointer;
            _converterPointer = converterPointer;
            _wkHtmlToXLibrary = wkHtmlToXLibrary;
            _objectSettings = objectSettings;
        }

        public static WkHtmlToPdfContext Create()
        {
            WkHtmlToPdf.wkhtmltopdf_init(UseX11Graphics);

            var wkHtmlToXLibrary = WkHtmlToPdfLibrary.Load();
            var globalSettingsPointer = WkHtmlToPdf.wkhtmltopdf_create_global_settings();
            var converterPointer = WkHtmlToPdf.wkhtmltopdf_create_converter(globalSettingsPointer);
            var objectSettings = WkHtmlToPdf.wkhtmltopdf_create_object_settings();

            return new WkHtmlToPdfContext(globalSettingsPointer, converterPointer, wkHtmlToXLibrary, objectSettings);
        }

        public void Dispose()
        {
            WkHtmlToPdf.wkhtmltopdf_deinit();
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

        public IntPtr ObjectSettings
        {
            get { return _objectSettings; }
        }
    }
}