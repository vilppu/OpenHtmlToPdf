using System;
using System.IO;
using System.Reflection;

namespace OpenHtmlToPdf.Assets
{
    sealed class ConverterExecutable
    {
        private const string ConverterExecutableFilename = "OpenHtmlToPdf.WkHtmlToPdf.exe";
        private const string ConverterExecutableReferenceFilename = "DotNetZip.dll";

        private ConverterExecutable()
        {
        }

        public static ConverterExecutable Get()
        {
            var bundledFile = new ConverterExecutable();

            bundledFile.CreateIfConverterExecutableDoesNotExist();

            return bundledFile;
        }


        public string FullConverterExecutableReferenceFilename
        {
            get { return ResolveFullPathToConverterExecutableReferenceFile(); }
        }


        public string FullConverterExecutableFilename
        {
            get { return ResolveFullPathToConverterExecutableFile(); }
        }

        private void CreateIfConverterExecutableDoesNotExist()
        {
            if (!File.Exists(FullConverterExecutableReferenceFilename))
                Create(GetConverterExecutableContent(GetConverterReferenceExecutable()), FullConverterExecutableReferenceFilename);

            if (!File.Exists(FullConverterExecutableFilename))
                Create(GetConverterExecutableContent(GetConverterExecutable()), FullConverterExecutableFilename);

        }

        private static byte[] GetConverterExecutableContent(Stream stream)
        {
            using (var resourceStream = stream)
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

        private static Stream GetConverterReferenceExecutable()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("OpenHtmlToPdf.Assets.DotNetZip.dll");
        }



        private void Create(byte[] fileContent, string Name)
        {
            try
            {
                if (!Directory.Exists(BundledFilesDirectory()))
                    Directory.CreateDirectory(BundledFilesDirectory());


                using (var file = File.Open(Name, FileMode.Create))
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

        private static string ResolveFullPathToConverterExecutableReferenceFile()
        {
            return Path.Combine(BundledFilesDirectory(), ConverterExecutableReferenceFilename);
        }

        private static string BundledFilesDirectory()
        {
            return Path.Combine(Path.GetTempPath(), "OpenHtmlToPdf", Version());
        }

        private static string Version()
        {
            return string.Format("{0}_{1}",
                Assembly.GetExecutingAssembly().GetName().Version,
                Environment.Is64BitProcess ? 64 : 32);
        }
    }
}