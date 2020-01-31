using System;
using System.Runtime.InteropServices;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf.Interop
{
    internal static class Kernel32
    {
        [DllImport("kernel32", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr LoadLibrary(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            string filename);
    }
}