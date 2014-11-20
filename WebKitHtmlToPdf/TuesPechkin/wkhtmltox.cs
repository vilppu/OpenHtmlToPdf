using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;
using WebKitHtmlToPdf.Assets;
using WebKitHtmlToPdf.Util;

namespace WebKitHtmlToPdf.TuesPechkin
{
    [Serializable]
    internal static class Wkhtmltox
    {
        private const string WkhtmltoxDllFilename = "wkhtmltox.dll";
        private const bool UseX11Graphics = false;
        private static string TocXslFilename { get; set; }

        static Wkhtmltox()
        {
            LoadUnmanagedWkhtmltox();
        }

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_add_object(IntPtr converter, IntPtr objectSettings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String data);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_add_object(IntPtr converter, IntPtr objectSettings, byte[] data);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_convert(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_create_converter(IntPtr globalSettings);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_create_global_settings();

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_create_object_settings();

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_current_phase(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_deinit();

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_destroy_converter(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_extended_qt();

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_get_global_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String name,
            [In]
            [Out]
            ref byte[] value, int valueSize);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_get_object_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String name,
            [In]
            [Out]
            ref byte[] value, int vs);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_get_output(IntPtr converter, out IntPtr data);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_http_error_code(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_init(int useGraphics);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_phase_count(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_phase_description(IntPtr converter, int phase);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_progress_string(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_error_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                 StringCallback callback);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_finished_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                    IntCallback callback);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_set_global_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String value);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_set_object_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            String value);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_phase_changed_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                         VoidCallback callback);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_progress_changed_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                            IntCallback callback);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_warning_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                   StringCallback callback);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern String wkhtmltopdf_version();

        private static void LoadUnmanagedWkhtmltox()
        {
            LoadUnmanagedWkhtmltoxTo(CurrentDirectory());
        }

        private static void LoadUnmanagedWkhtmltoxTo(string basePath)
        {
           var dllPath = Path.Combine(basePath, WkhtmltoxDllFilename);

           WriteStreamToFileIfFileDoesNotExist(dllPath,
                () => new GZipStream(new MemoryStream(GetWkhtmltoxDllContentBytes()), CompressionMode.Decompress));

            TocXslFilename = Path.Combine(basePath, "toc.xsl");

            WriteStreamToFileIfFileDoesNotExist(TocXslFilename, () => new MemoryStream(BundledFiles.toc));

            if (WinApiHelper.LoadLibrary(dllPath) == IntPtr.Zero)
                throw new InvalidOperationException(string.Format("Failed to load {0}", dllPath));

            wkhtmltopdf_init(UseX11Graphics ? 1 : 0);
        }

        private static string CurrentDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static byte[] GetWkhtmltoxDllContentBytes()
        {
            return (IntPtr.Size == 8) ? BundledFiles.wkhtmltox_64_dll : BundledFiles.wkhtmltox_32_dll;
        }

        private static void WriteStreamToFileIfFileDoesNotExist(string fileName, System.Func<Stream> streamFactory)
        {
            if (!File.Exists(fileName))
                WriteStreamToFile(fileName, streamFactory);

        }

        private static void WriteStreamToFile(string fileName, System.Func<Stream> streamFactory)
        {
            var stream = streamFactory();
            var writeBuffer = new byte[8192];

            using (var newFile = File.Open(fileName, FileMode.Create))
            {
                int writeLength;
                while ((writeLength = stream.Read(writeBuffer, 0, writeBuffer.Length)) > 0)
                {
                    newFile.Write(writeBuffer, 0, writeLength);
                }
            }
        }
    }
}