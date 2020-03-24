using System;
using System.Runtime.InteropServices;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf.Interop
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void StringCallback(IntPtr converter, [MarshalAs(UnmanagedType.LPStr)] string str);
}