using System;
using System.IO;
using System.Reflection;

namespace OpenHtmlToPdf.WkHtmlToPdf.Assets
{
    sealed class BundledFile
    {
        private readonly string _bundledFilename;
        private readonly Func<byte[]> _fileContentProvider;

        private BundledFile(string bundledFilename, Func<byte[]> fileContentProvider)
        {
            _bundledFilename = bundledFilename;
            _fileContentProvider = fileContentProvider;
        }

        public static BundledFile From(string bundledFilename, Func<byte[]> fileContentProvider)
        {
            var bundledFile = new BundledFile(bundledFilename, fileContentProvider);

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
                Create(_fileContentProvider());
        }

        private void Create(byte[] fileContent)
        {
            try
            {
                if (!Directory.Exists(BundledFilesDirectory()))
                    Directory.CreateDirectory(BundledFilesDirectory());


                using (var file = File.Open(FullPathToBundledFile, FileMode.Create))
                {

                    file.Write(fileContent, 0, fileContent.Length);
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