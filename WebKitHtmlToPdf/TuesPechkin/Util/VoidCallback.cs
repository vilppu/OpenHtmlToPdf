using System;
using System.Runtime.InteropServices;

namespace WebKitHtmlToPdf.Util
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void VoidCallback(IntPtr converter);
}