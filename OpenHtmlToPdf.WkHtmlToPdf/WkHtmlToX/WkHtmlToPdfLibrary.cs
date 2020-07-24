using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Ionic.Zip;
using OpenHtmlToPdf.WkHtmlToPdf.Assets;
using OpenHtmlToPdf.WkHtmlToPdf.Interop;

namespace OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX
{
    sealed class WkHtmlToPdfLibrary
    {
        private const string LibraryFilename = "wkhtmltox.dll";
        private const string Compressed32BitLibraryFilename = "wkhtmltox_32.dll";
        private const string Compressed64BitLibraryFilename = "wkhtmltox_64.dll";

        public static NativeLibrary Load()
        {
            return NativeLibrary.Load(LibraryFilename, LoadLibraryContent);
        }

        private static byte[] LoadLibraryContent()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                throw new PlatformNotSupportedException(String.Format("Platform {0} is not supported", Platform()));

            using (var wkhtmltoxZipArchive = WkHtmlToXZipFile())
            {
                return wkhtmltoxZipArchive.ReadFile(CompressedLibraryFilename());
            }
        }

        private static ZipFile WkHtmlToXZipFile()
        {
            ZipFile zipFile = null;
            using (zipFile = new ZipFile())
            {
                zipFile.Save(GetManifestResourceStream());
            }
            return zipFile;
        }

        private static Stream GetManifestResourceStream()
        {
            return Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("OpenHtmlToPdf.WkHtmlToPdf.Assets.wkhtmltox.zip");
        }

        private static string CompressedLibraryFilename()
        {
            return Environment.Is64BitProcess
                ? Compressed64BitLibraryFilename
                : Compressed32BitLibraryFilename;
        }

        private static string Platform()
        {
            return Enum.GetName(typeof(PlatformID), Environment.OSVersion.Platform);
        }
    }
}