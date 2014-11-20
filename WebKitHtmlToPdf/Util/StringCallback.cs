using System;
using System.Runtime.InteropServices;

namespace WebKitHtmlToPdf.Util
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void StringCallback(IntPtr converter, [MarshalAs(UnmanagedType.LPStr)] String str);
}