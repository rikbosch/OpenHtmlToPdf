using System;

namespace Core.OpenHtmlToPdf
{
    public sealed class DocumentCreationFailedException : Exception
    {
        internal DocumentCreationFailedException(string message)
            : base(message)
        {
        }
    }
}
