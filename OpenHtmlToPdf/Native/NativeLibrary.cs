using System;
using OpenHtmlToPdf.Assets;

namespace OpenHtmlToPdf.Native
{
    sealed class NativeLibrary
    {
        private readonly Lazy<BundledFile> _libraryFile;
        private IntPtr _libraryHandle;

        private NativeLibrary(string libraryFilename, Func<byte[]> libraryContentProvider)
        {
            _libraryFile = new Lazy<BundledFile>(() => BundledFile.From(libraryFilename, libraryContentProvider));
        }

        public static NativeLibrary Load(string libraryFilename, Func<byte[]> libraryContentProvider)
        {
            var nativeLibrary = new NativeLibrary(libraryFilename, libraryContentProvider);

            nativeLibrary.LoadWindowsNativeLibrary();

            return nativeLibrary;
        }

        public void Dispose()
        {
            if (_libraryHandle != IntPtr.Zero)
                FreeWindowsNativeLibrary();
        }

        private void FreeWindowsNativeLibrary()
        {
            Kernel32.FreeLibrary(_libraryHandle);

            _libraryHandle = IntPtr.Zero;
        }

        private void LoadWindowsNativeLibrary()
        {
            _libraryHandle = Kernel32.LoadLibrary(LibraryFile.FullPathToBundledFile);

            if (_libraryHandle == IntPtr.Zero)
                throw new InvalidOperationException(string.Format("Failed to load {0}", LibraryFile.FullPathToBundledFile));
        }

        private BundledFile LibraryFile
        {
            get { return _libraryFile.Value; }
        }
    }
}