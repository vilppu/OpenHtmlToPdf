using System;
using System.Runtime.InteropServices;
using System.Text;
using OpenHtmlToPdf.Native;

namespace OpenHtmlToPdf.WkHtmlToX
{
    sealed class WkHtmlToPdf : IDisposable
    {
        private readonly NativeLibrary _wkHtmlToXLibrary;
        private const int UseX11Graphics = 0;

        private WkHtmlToPdf(NativeLibrary wkHtmlToXLibrary)
        {
            _wkHtmlToXLibrary = wkHtmlToXLibrary;
        }

        public void Dispose()
        {
            Wkhtmltox.wkhtmltopdf_deinit();

            _wkHtmlToXLibrary.Dispose();
        }

        public static WkHtmlToPdf Create()
        {
            var wkHtmlToPdf = new WkHtmlToPdf(WkHtmlToPdfLibrary.Load());

            Wkhtmltox.wkhtmltopdf_init(UseX11Graphics);

            return wkHtmlToPdf;
        }

        public IntPtr CreateGlobalSetting()
        {
            return Wkhtmltox.wkhtmltopdf_create_global_settings();
        }

        public IntPtr CreateObjectSettings()
        {
            return Wkhtmltox.wkhtmltopdf_create_object_settings();
        }

        public int SetGlobalSetting(IntPtr setting, string name, string value)
        {
            return Wkhtmltox.wkhtmltopdf_set_global_setting(setting, name, value);
        }

        public string GetGlobalSetting(IntPtr setting, string name)
        {
            var buf = new byte[2048];

            Wkhtmltox.wkhtmltopdf_get_global_setting(setting, name, ref buf, buf.Length);

            int walk = 0;
            while (walk < buf.Length && buf[walk] != 0)
            {
                walk++;
            }

            var buf2 = new byte[walk];
            Array.Copy(buf, 0, buf2, 0, walk);

            return Encoding.UTF8.GetString(buf2);
        }

        public int SetObjectSetting(IntPtr setting, string name, string value)
        {
            return Wkhtmltox.wkhtmltopdf_set_object_setting(setting, name, value);
        }

        public string GetObjectSetting(IntPtr setting, string name)
        {
            var buf = new byte[2048];

            Wkhtmltox.wkhtmltopdf_get_object_setting(setting, name, ref buf, buf.Length);

            int walk = 0;
            while (walk < buf.Length && buf[walk] != 0)
            {
                walk++;
            }

            var buf2 = new byte[walk];
            Array.Copy(buf, 0, buf2, 0, walk);

            return Encoding.UTF8.GetString(buf2);
        }

        public IntPtr CreateConverter(IntPtr globalSettings)
        {
            return Wkhtmltox.wkhtmltopdf_create_converter(globalSettings);
        }

        public void DestroyConverter(IntPtr converter)
        {
            Wkhtmltox.wkhtmltopdf_destroy_converter(converter);
        }

        public void SetWarningCallback(IntPtr converter, StringCallback callback)
        {
            Wkhtmltox.wkhtmltopdf_set_warning_callback(converter, callback);
        }

        public void SetErrorCallback(IntPtr converter, StringCallback callback)
        {
            Wkhtmltox.wkhtmltopdf_set_error_callback(converter, callback);
        }

        public void SetFinishedCallback(IntPtr converter, IntCallback callback)
        {
            Wkhtmltox.wkhtmltopdf_set_finished_callback(converter, callback);
        }

        public void SetPhaseChangedCallback(IntPtr converter, VoidCallback callback)
        {
            Wkhtmltox.wkhtmltopdf_set_phase_changed_callback(converter, callback);
        }

        public void SetProgressChangedCallback(IntPtr converter, IntCallback callback)
        {
            Wkhtmltox.wkhtmltopdf_set_progress_changed_callback(converter, callback);
        }

        public bool PerformConversion(IntPtr converter)
        {
            return Wkhtmltox.wkhtmltopdf_convert(converter) != 0;
        }

        public void AddObject(IntPtr converter, IntPtr objectConfig, string html)
        {
            Wkhtmltox.wkhtmltopdf_add_object(converter, objectConfig, html);
        }

        public void AddObject(IntPtr converter, IntPtr objectConfig, byte[] html)
        {
            Wkhtmltox.wkhtmltopdf_add_object(converter, objectConfig, html);
        }

        public int GetPhaseNumber(IntPtr converter)
        {
            return Wkhtmltox.wkhtmltopdf_current_phase(converter);
        }

        public int GetPhaseCount(IntPtr converter)
        {
            return Wkhtmltox.wkhtmltopdf_phase_count(converter);
        }

        public string GetPhaseDescription(IntPtr converter, int phase)
        {
            return Marshal.PtrToStringAnsi(Wkhtmltox.wkhtmltopdf_phase_description(converter, phase));
        }

        public string GetProgressDescription(IntPtr converter)
        {
            return Marshal.PtrToStringAnsi(Wkhtmltox.wkhtmltopdf_progress_string(converter));
        }

        public int GetHttpErrorCode(IntPtr converter)
        {
            return Wkhtmltox.wkhtmltopdf_http_error_code(converter);
        }

        public byte[] GetConverterResult(IntPtr converter)
        {
            IntPtr tmp;
            var len = Wkhtmltox.wkhtmltopdf_get_output(converter, out tmp);
            var output = new byte[len];
            Marshal.Copy(tmp, output, 0, output.Length);
            return output;
        }
    }
}