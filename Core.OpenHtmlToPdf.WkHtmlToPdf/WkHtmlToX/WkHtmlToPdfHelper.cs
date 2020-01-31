using System;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX
{
    public static class WkHtmlToPdfHelper
    {
        public static void Convert(this WkHtmlToPdfContext wkHtmlToPdfContext, string html)
        {
            void errorCallback(IntPtr converter, string errorText) => throw new ConversionFailedException(errorText);

            WkHtmlToPdf.wkhtmltopdf_set_error_callback(wkHtmlToPdfContext.ConverterPointer, errorCallback);
            WkHtmlToPdf.wkhtmltopdf_add_object(wkHtmlToPdfContext.ConverterPointer, wkHtmlToPdfContext.ObjectSettingsPointer, html);
            WkHtmlToPdf.wkhtmltopdf_convert(wkHtmlToPdfContext.ConverterPointer);
        }
    }
}