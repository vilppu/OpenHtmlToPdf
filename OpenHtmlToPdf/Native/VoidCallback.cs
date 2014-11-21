using System;
using System.Runtime.InteropServices;

namespace OpenHtmlToPdf.Native
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void VoidCallback(IntPtr converter);
}