using System;
using OpenHtmlToPdf.Interop;

namespace OpenHtmlToPdf.WkHtmlToX
{
    public sealed class WkHtmlToPdfContext : MarshalByRefObject, IDisposable
    {
        private const int UseX11Graphics = 0;
        private readonly IntPtr _globalSettingsPointer;
        private readonly IntPtr _converterPointer;
        private readonly NativeLibrary _wkHtmlToXLibrary;
        private readonly IntPtr _objectSettingsPointer;

        private WkHtmlToPdfContext(IntPtr globalSettingsPointer, IntPtr converterPointer, NativeLibrary wkHtmlToXLibrary, IntPtr objectSettingsPointer)
        {
            _globalSettingsPointer = globalSettingsPointer;
            _converterPointer = converterPointer;
            _wkHtmlToXLibrary = wkHtmlToXLibrary;
            _objectSettingsPointer = objectSettingsPointer;
        }

        public static WkHtmlToPdfContext Create()
        {
            var wkHtmlToXLibrary = WkHtmlToPdfLibrary.Load();

            WkHtmlToPdf.wkhtmltopdf_init(UseX11Graphics);

            var globalSettingsPointer = WkHtmlToPdf.wkhtmltopdf_create_global_settings();
            var converterPointer = WkHtmlToPdf.wkhtmltopdf_create_converter(globalSettingsPointer);
            var objectSettingsPointer = WkHtmlToPdf.wkhtmltopdf_create_object_settings();

            return new WkHtmlToPdfContext(globalSettingsPointer, converterPointer, wkHtmlToXLibrary, objectSettingsPointer);
        }

        public void Dispose()
        {
            if (_converterPointer != IntPtr.Zero)
                WkHtmlToPdf.wkhtmltopdf_destroy_converter(_converterPointer);

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

        public IntPtr ObjectSettingsPointer
        {
            get { return _objectSettingsPointer; }
        }
    }
}