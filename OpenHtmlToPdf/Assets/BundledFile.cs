using System;
using System.IO;
using System.Reflection;

namespace OpenHtmlToPdf.Assets
{
    sealed class BundledFile
    {
        private readonly string _bundledFilename;
        private readonly byte[] _fileContent;

        private BundledFile(string bundledFilename, byte[] fileContent)
        {
            _bundledFilename = bundledFilename;
            _fileContent = fileContent;
        }

        public static BundledFile From(string bundledFilename, byte[] fileContent)
        {
            var bundledFile = new BundledFile(bundledFilename, fileContent);

            bundledFile.CreateIfNotExist();

            return bundledFile;
        }

        public string FullPathToBundledFile
        {
            get { return ResolveFullPathToBundledFile(); }
        }

        private void CreateIfNotExist()
        {
            if (!File.Exists(FullPathToBundledFile))
                Create();
        }

        private void Create()
        {
            try
            {
                if (!Directory.Exists(BundledFilesDirectory()))
                    Directory.CreateDirectory(BundledFilesDirectory());

                using (var file = File.Open(FullPathToBundledFile, FileMode.Create))
                {
                    file.Write(_fileContent, 0, _fileContent.Length);
                }
            }
            catch (IOException)
            {
            }
        }

        private string ResolveFullPathToBundledFile()
        {
            return Path.Combine(BundledFilesDirectory(), _bundledFilename);
        }

        private static string BundledFilesDirectory()
        {
            return Path.Combine(Path.GetTempPath(), CurrentAssemblyNameAndVersion());
        }

        private static string CurrentAssemblyNameAndVersion()
        {
            return string.Format("{0}_{1}_{2}",
                Assembly.GetExecutingAssembly().GetName().Name,
                Assembly.GetExecutingAssembly().GetName().Version,
                Environment.Is64BitProcess ? 64 : 32);
        }
    }
}