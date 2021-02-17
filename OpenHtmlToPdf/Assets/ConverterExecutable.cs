using System;
using System.IO;
using System.Reflection;

namespace OpenHtmlToPdf.Assets
{
    sealed class ConverterExecutable
    {
        private const string ConverterExecutableFilename = "OpenHtmlToPdf.WkHtmlToPdf.exe";

        private ConverterExecutable()
        {
        }

        public static ConverterExecutable Get()
        {
            var bundledFile = new ConverterExecutable();

            bundledFile.CreateIfConverterExecutableDoesNotExist();

            return bundledFile;
        }

        public string FullConverterExecutableFilename
        {
            get { return ResolveFullPathToConverterExecutableFile(); }
        }

        private void CreateIfConverterExecutableDoesNotExist()
        {
            if (!File.Exists(FullConverterExecutableFilename))
                Create(GetConverterExecutableContent());
        }

        private static byte[] GetConverterExecutableContent()
        {
            using (var resourceStream = GetConverterExecutable())
            {
                var resource = new byte[resourceStream.Length];

                resourceStream.Read(resource, 0, resource.Length);

                return resource;
            }
        }

        private static Stream GetConverterExecutable()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OpenHtmlToPdf.Assets.OpenHtmlToPdf.WkHtmlToPdf.exe");
        }

        private void Create(byte[] fileContent)
        {
            try
            {
                if (!Directory.Exists(BundledFilesDirectory()))
                    Directory.CreateDirectory(BundledFilesDirectory());


                using (var file = File.Open(FullConverterExecutableFilename, FileMode.Create))
                {

                    file.Write(fileContent, 0, fileContent.Length);
                }
            }
            catch (IOException)
            {
            }
        }

        private static string ResolveFullPathToConverterExecutableFile()
        {
            return Path.Combine(BundledFilesDirectory(), ConverterExecutableFilename);
        }

        private static string BundledFilesDirectory()
        {
            return Path.Combine(TemporaryPdf.GetTempPath(), "OpenHtmlToPdf", Version());
        }

        private static string Version()
        {
            return string.Format("{0}_{1}",
                Assembly.GetExecutingAssembly().GetName().Version,
                Environment.Is64BitProcess ? 64 : 32);
        }
    }
}