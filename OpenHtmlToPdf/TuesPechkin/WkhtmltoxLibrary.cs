using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using OpenHtmlToPdf.Assets;

namespace OpenHtmlToPdf.TuesPechkin
{
    sealed class WkhtmltoxLibrary : IDisposable
    {
        private static IntPtr _libraryHandle;
        private const string WkhtmltoxDllFilename = "wkhtmltox.dll";
        private const int UseX11Graphics = 0;

        public WkhtmltoxLibrary()
        {
            LoadUnmanagedWkhtmltox();
        }

        public void Dispose()
        {
            if (_libraryHandle != IntPtr.Zero)
            {
                WinApiHelper.FreeLibrary(_libraryHandle);
                _libraryHandle = IntPtr.Zero;
            }
        }

        private static void LoadUnmanagedWkhtmltox()
        {
            LoadUnmanagedWkhtmltoxTo(CurrentDirectory());
        }

        private static void LoadUnmanagedWkhtmltoxTo(string basePath)
        {
            var dllPath = Path.Combine(basePath, WkhtmltoxDllFilename);

            WriteStreamToFile(dllPath,
                () => new GZipStream(new MemoryStream(GetWkhtmltoxDllContentBytes()), CompressionMode.Decompress));

           var tocXslFilename = Path.Combine(basePath, "toc.xsl");

            WriteStreamToFile(tocXslFilename, () => new MemoryStream(BundledFiles.toc));

            _libraryHandle = WinApiHelper.LoadLibrary(dllPath);

            if (_libraryHandle == IntPtr.Zero)
                throw new InvalidOperationException(string.Format("Failed to load {0}", dllPath));

            WkhtmltoxApi.wkhtmltopdf_init(UseX11Graphics);
        }

        private static string CurrentDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static byte[] GetWkhtmltoxDllContentBytes()
        {
            return (IntPtr.Size == 8) ? BundledFiles.wkhtmltox_64_dll : BundledFiles.wkhtmltox_32_dll;
        }

        private static void WriteStreamToFile(string fileName, Func<Stream> streamFactory)
        {
            var stream = streamFactory();
            var writeBuffer = new byte[8192];
            try
            {
                using (var newFile = File.Open(fileName, FileMode.Create))
                {
                    int writeLength;
                    while ((writeLength = stream.Read(writeBuffer, 0, writeBuffer.Length)) > 0)
                    {
                        newFile.Write(writeBuffer, 0, writeLength);
                    }
                }
            }
            catch (IOException)
            {

            }
        }
    }
}