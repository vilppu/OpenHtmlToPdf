using Ionic.Zip;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace OpenHtmlToPdf.WkHtmlToPdf.Assets
{
    static class ZipArchiveHelper
    {
        public static byte[] ReadFile(this ZipFile zipFile, string filename)
        {
            return zipFile.Entries
                .Where(e => e.FileName == filename)
                .Select(Read).Single();
        }

        private static byte[] Read(this ZipEntry zipEntry)
        {
            using (var stream = zipEntry.OpenReader())
            {
                return stream.Read(zipEntry.UncompressedSize);
            }
        }

        private static byte[] Read(this Stream stream, long length)
        {
            var wkhtmltoxContent = new byte[length];

            stream.Read(wkhtmltoxContent, 0, (int)length);

            return wkhtmltoxContent;
        }
    }
}