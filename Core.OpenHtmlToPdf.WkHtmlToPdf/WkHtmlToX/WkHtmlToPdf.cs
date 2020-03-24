using System;
using System.Runtime.InteropServices;
using Core.OpenHtmlToPdf.WkHtmlToPdf.Interop;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX
{
    [Serializable]
    internal static class WkHtmlToPdf
    {
        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_add_object(
            IntPtr converter,
            IntPtr objectSettings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            string data);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_add_object(IntPtr converter, IntPtr objectSettings, byte[] data);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_convert(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_create_converter(IntPtr globalSettings);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_create_global_settings();

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_create_object_settings();

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_current_phase(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_deinit();

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_destroy_converter(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_extended_qt();

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_get_global_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            string name,
            [In]
            [Out]
            ref byte[] value, int valueSize);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_get_object_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            string name,
            [In]
            [Out]
            ref byte[] value, int vs);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_get_output(IntPtr converter, out IntPtr data);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_http_error_code(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_init(int useGraphics);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_phase_count(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_phase_description(IntPtr converter, int phase);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr wkhtmltopdf_progress_string(IntPtr converter);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_error_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                 StringCallback callback);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_finished_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                    IntCallback callback);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_set_global_setting(IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            string name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            string value);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern int wkhtmltopdf_set_object_setting(
            IntPtr settings,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            string name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8Marshaler))]
            string value);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_phase_changed_callback(
            IntPtr converter,
            [MarshalAs(UnmanagedType.FunctionPtr)]
            VoidCallback callback);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_progress_changed_callback(
            IntPtr converter,
            [MarshalAs(UnmanagedType.FunctionPtr)]
            IntCallback callback);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void wkhtmltopdf_set_warning_callback(IntPtr converter, [MarshalAs(UnmanagedType.FunctionPtr)]
                                                                   StringCallback callback);

        [DllImport("wkhtmltox.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string wkhtmltopdf_version();
    }
}