using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using OpenHtmlToPdf.Assets;
using OpenHtmlToPdf.TuesPechkin;

namespace OpenHtmlToPdf.Wkhtmltopdf
{
    sealed class WkhtmltopdfDll : IDisposable
    {
        private static IntPtr _libraryHandle;
        private const string DllFilename = "wkhtmltox.dll";
        private const int UseX11Graphics = 0;

        public WkhtmltopdfDll()
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
            VerifyThatWkhtmltopdfLibraryExists(basePath);
            VerifyThatTocXslFilenameExists(basePath);
            LoadLibrary(basePath);
            InitializeWkhtmltopdf();
        }

        private static void VerifyThatWkhtmltopdfLibraryExists(string basePath)
        {
            var dllPath = Path.Combine(basePath, DllFilename);

            WriteStreamToFileIfFileDoesNotAlreadyExist(dllPath, new GZipStream(new MemoryStream(GetWkhtmltoxDllContentBytes()), CompressionMode.Decompress));
        }

        private static void VerifyThatTocXslFilenameExists(string basePath)
        {
            var tocXslFilename = Path.Combine(basePath, "toc.xsl");

            WriteStreamToFileIfFileDoesNotAlreadyExist(tocXslFilename, new MemoryStream(BundledFiles.toc));
        }

        private static void LoadLibrary(string basePath)
        {
            var dllPath = Path.Combine(basePath, DllFilename);

            _libraryHandle = WinApiHelper.LoadLibrary(dllPath);

            if (_libraryHandle == IntPtr.Zero)
                throw new InvalidOperationException(string.Format("Failed to load {0}", dllPath));
        }

        private static void InitializeWkhtmltopdf()
        {
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

        private static void WriteStreamToFileIfFileDoesNotAlreadyExist(string fileName, Stream stream)
        {
            if (!File.Exists(fileName))
                WriteStreamToFile(fileName, stream);

        }

        private static void WriteStreamToFile(string fileName, Stream stream)
        {
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
            { }
        }

    }
}