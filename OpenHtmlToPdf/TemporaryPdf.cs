using System;
using System.IO;

namespace OpenHtmlToPdf
{
    public static class TemporaryPdf
    {
        public static byte[] ReadTemporaryFileContent(string temporaryFilename)
        {
            using (var temporaryFile = new FileStream(temporaryFilename, FileMode.Open, FileAccess.Read))
            {
                var content = new byte[temporaryFile.Length];

                temporaryFile.Read(content, 0, content.Length);

                return content;
            }
        }

        public static void DeleteTemporaryFile(string temporaryFilename)
        {
            try
            {
                if (File.Exists(temporaryFilename))
                    File.Delete(temporaryFilename);
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        public static string TemporaryFilePath()
        {
            return Path.Combine(Path.GetTempPath(), "OpenHtmlToPdf", TemporaryFilename());
        }

        private static string TemporaryFilename()
        {
            return Guid.NewGuid().ToString("N") + ".pdf";
        }
    }
}