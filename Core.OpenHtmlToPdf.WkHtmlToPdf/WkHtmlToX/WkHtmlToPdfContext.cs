using System;
using Core.OpenHtmlToPdf.WkHtmlToPdf.Interop;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX
{
    public sealed class WkHtmlToPdfContext : IDisposable
    {
        private const int UseX11Graphics = 0;
        private readonly NativeLibrary _wkHtmlToXLibrary;

        public IntPtr GlobalSettingsPointer { get; }
        public IntPtr ConverterPointer { get; }
        public IntPtr ObjectSettingsPointer { get; }

        private WkHtmlToPdfContext(NativeLibrary wkHtmlToXLibrary, IntPtr globalSettingsPointer, IntPtr converterPointer, IntPtr objectSettingsPointer)
        {
            _wkHtmlToXLibrary = wkHtmlToXLibrary;
            GlobalSettingsPointer = globalSettingsPointer;
            ConverterPointer = converterPointer;
            ObjectSettingsPointer = objectSettingsPointer;
        }

        public static WkHtmlToPdfContext Create()
        {
            NativeLibrary wkHtmlToXLibrary = WkHtmlToPdfLibrary.Load();

            WkHtmlToPdf.wkhtmltopdf_init(UseX11Graphics);

            IntPtr globalSettingsPointer = WkHtmlToPdf.wkhtmltopdf_create_global_settings();
            IntPtr converterPointer = WkHtmlToPdf.wkhtmltopdf_create_converter(globalSettingsPointer);
            IntPtr objectSettingsPointer = WkHtmlToPdf.wkhtmltopdf_create_object_settings();

            return new WkHtmlToPdfContext(wkHtmlToXLibrary, globalSettingsPointer, converterPointer, objectSettingsPointer);
        }

        public void Dispose()
        {
            if (ConverterPointer != IntPtr.Zero)
            {
                WkHtmlToPdf.wkhtmltopdf_destroy_converter(ConverterPointer);
            }

            WkHtmlToPdf.wkhtmltopdf_deinit();
            _wkHtmlToXLibrary.Dispose();
        }
    }
}