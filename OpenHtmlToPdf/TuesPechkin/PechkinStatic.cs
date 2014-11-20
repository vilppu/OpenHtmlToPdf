using System;
using System.Runtime.InteropServices;
using System.Text;
using OpenHtmlToPdf.Util;
using OpenHtmlToPdf.Wkhtmltopdf;

namespace OpenHtmlToPdf.TuesPechkin
{
    /// <summary>
    ///     Static class with utility methods for the interface.
    ///     Acts mostly as a facade over PechkinBindings with log tracing.
    /// </summary>
    [Serializable]
    internal static class PechkinStatic
    {
        public static IntPtr CreateGlobalSetting()
        {
            return WkhtmltoxApi.wkhtmltopdf_create_global_settings();
        }

        public static IntPtr CreateObjectSettings()
        {
            return WkhtmltoxApi.wkhtmltopdf_create_object_settings();
        }

        public static int SetGlobalSetting(IntPtr setting, string name, string value)
        {
            return WkhtmltoxApi.wkhtmltopdf_set_global_setting(setting, name, value);
        }

        public static string GetGlobalSetting(IntPtr setting, string name)
        {
            var buf = new byte[2048];

            WkhtmltoxApi.wkhtmltopdf_get_global_setting(setting, name, ref buf, buf.Length);

            int walk = 0;
            while (walk < buf.Length && buf[walk] != 0)
            {
                walk++;
            }

            var buf2 = new byte[walk];
            Array.Copy(buf, 0, buf2, 0, walk);

            return Encoding.UTF8.GetString(buf2);
        }

        public static int SetObjectSetting(IntPtr setting, string name, string value)
        {
            return WkhtmltoxApi.wkhtmltopdf_set_object_setting(setting, name, value);
        }

        public static string GetObjectSetting(IntPtr setting, string name)
        {
            var buf = new byte[2048];

            WkhtmltoxApi.wkhtmltopdf_get_object_setting(setting, name, ref buf, buf.Length);

            int walk = 0;
            while (walk < buf.Length && buf[walk] != 0)
            {
                walk++;
            }

            var buf2 = new byte[walk];
            Array.Copy(buf, 0, buf2, 0, walk);

            return Encoding.UTF8.GetString(buf2);
        }

        public static IntPtr CreateConverter(IntPtr globalSettings)
        {
            return WkhtmltoxApi.wkhtmltopdf_create_converter(globalSettings);
        }

        public static void DestroyConverter(IntPtr converter)
        {
            WkhtmltoxApi.wkhtmltopdf_destroy_converter(converter);
        }

        public static void SetWarningCallback(IntPtr converter, StringCallback callback)
        {
            WkhtmltoxApi.wkhtmltopdf_set_warning_callback(converter, callback);
        }

        public static void SetErrorCallback(IntPtr converter, StringCallback callback)
        {
            WkhtmltoxApi.wkhtmltopdf_set_error_callback(converter, callback);
        }

        public static void SetFinishedCallback(IntPtr converter, IntCallback callback)
        {
            WkhtmltoxApi.wkhtmltopdf_set_finished_callback(converter, callback);
        }

        public static void SetPhaseChangedCallback(IntPtr converter, VoidCallback callback)
        {
            WkhtmltoxApi.wkhtmltopdf_set_phase_changed_callback(converter, callback);
        }

        public static void SetProgressChangedCallback(IntPtr converter, IntCallback callback)
        {
            WkhtmltoxApi.wkhtmltopdf_set_progress_changed_callback(converter, callback);
        }

        public static bool PerformConversion(IntPtr converter)
        {
            return WkhtmltoxApi.wkhtmltopdf_convert(converter) != 0;
        }

        public static void AddObject(IntPtr converter, IntPtr objectConfig, string html)
        {
            WkhtmltoxApi.wkhtmltopdf_add_object(converter, objectConfig, html);
        }

        public static void AddObject(IntPtr converter, IntPtr objectConfig, byte[] html)
        {
            WkhtmltoxApi.wkhtmltopdf_add_object(converter, objectConfig, html);
        }

        public static int GetPhaseNumber(IntPtr converter)
        {
            return WkhtmltoxApi.wkhtmltopdf_current_phase(converter);
        }

        public static int GetPhaseCount(IntPtr converter)
        {
            return WkhtmltoxApi.wkhtmltopdf_phase_count(converter);
        }

        public static string GetPhaseDescription(IntPtr converter, int phase)
        {
            return Marshal.PtrToStringAnsi(WkhtmltoxApi.wkhtmltopdf_phase_description(converter, phase));
        }

        public static string GetProgressDescription(IntPtr converter)
        {
            return Marshal.PtrToStringAnsi(WkhtmltoxApi.wkhtmltopdf_progress_string(converter));
        }

        public static int GetHttpErrorCode(IntPtr converter)
        {
            return WkhtmltoxApi.wkhtmltopdf_http_error_code(converter);
        }

        public static byte[] GetConverterResult(IntPtr converter)
        {
            IntPtr tmp;
            int len = WkhtmltoxApi.wkhtmltopdf_get_output(converter, out tmp);
            var output = new byte[len];
            Marshal.Copy(tmp, output, 0, output.Length);
            return output;
        }
    }
}