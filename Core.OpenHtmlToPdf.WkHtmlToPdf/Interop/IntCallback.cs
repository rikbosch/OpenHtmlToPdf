using System;
using System.Runtime.InteropServices;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf.Interop
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void IntCallback(IntPtr converter, int str);
}