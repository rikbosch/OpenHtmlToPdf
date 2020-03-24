using System;

namespace Core.OpenHtmlToPdf
{
    public sealed class PdfDocumentCreationFailedException : Exception
    {
        public PdfDocumentCreationFailedException(string error)
            : base(error)
        {
        }
    }
}