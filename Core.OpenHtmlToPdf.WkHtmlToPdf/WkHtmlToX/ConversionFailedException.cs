using System;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX
{
    internal sealed class ConversionFailedException : Exception
    {
        public ConversionFailedException(string errorText) : base(errorText) { }
    }
}