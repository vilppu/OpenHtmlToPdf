using System;
using System.IO;
using System.IO.Compression;
using OpenHtmlToPdf.Assets;
using OpenHtmlToPdf.Native;

namespace OpenHtmlToPdf.WkHtmlToX
{
    sealed class WkHtmlToPdfLibrary
    {
        private const string LibraryFilename = "wkhtmltox.dll";
        private const string Compressed32BitLibraryFilename = "wkhtmltox_32.dll";
        private const string Compressed64BitLibraryFilename = "wkhtmltox_64.dll";

        public static NativeLibrary Load()
        {
            return NativeLibrary.Load(LibraryFilename, LibraryContent());
        }

        private static byte[] LibraryContent()
        {
            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                throw new PlatformNotSupportedException(String.Format("Platform {0} is not supported", Platform()));

            using (var wkhtmltoxZipArchive = WkhtmltoxZipArchive())
            {
                return wkhtmltoxZipArchive.ReadFile(CompressedLibraryFilename());
            }
        }

        private static ZipArchive WkhtmltoxZipArchive()
        {
            return new ZipArchive(new MemoryStream(BundledFiles.wkhtmltox));
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