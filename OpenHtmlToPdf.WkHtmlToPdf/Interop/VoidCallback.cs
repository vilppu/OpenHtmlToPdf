using System;
using System.Runtime.InteropServices;

namespace OpenHtmlToPdf.WkHtmlToPdf.Interop
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void VoidCallback(IntPtr converter);
}