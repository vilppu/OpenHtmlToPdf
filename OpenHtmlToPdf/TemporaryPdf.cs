using System;
using System.IO;

namespace OpenHtmlToPdf
{
    public static class TemporaryPdf
    {
        /// <summary>
        /// Setting this value overrides the default value for the temporary files folder.
        /// </summary>
        public static string TempFilesPath { set; get; }

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

        public static string GetTempPath() => TempFilesPath ?? Path.GetTempPath();

        public static string TemporaryFilePath()
        {
            return Path.Combine(GetTempPath(), "OpenHtmlToPdf", TemporaryFilename());
        }

        private static string TemporaryFilename()
        {
            return Guid.NewGuid().ToString("N") + ".pdf";
        }
    }
}