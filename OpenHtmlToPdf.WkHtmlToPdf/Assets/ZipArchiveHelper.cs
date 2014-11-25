using System.IO;
using System.IO.Compression;
using System.Linq;

namespace OpenHtmlToPdf.WkHtmlToPdf.Assets
{
    static class ZipArchiveHelper
    {
        public static byte[] ReadFile(this ZipArchive zipArchive, string filename)
        {
            return zipArchive.Entries
                .Where(e => e.FullName == filename)
                .Select(Read).Single();
        }

        private static byte[] Read(this ZipArchiveEntry zipArchiveEntry)
        {
            using (var stream = zipArchiveEntry.Open())
            {
                return stream.Read(zipArchiveEntry.Length);
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