using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf.Assets
{
    internal static class ZipArchiveHelper
    {
        public static byte[] ReadFile(this ZipArchive zipArchive, string filename) => zipArchive.Entries
            .Where(e => e.FullName == filename)
            .Select(Read).Single();

        private static byte[] Read(this ZipArchiveEntry zipArchiveEntry)
        {
            using (Stream stream = zipArchiveEntry.Open())
            {
                return stream.Read(zipArchiveEntry.Length);
            }
        }

        private static byte[] Read(this Stream stream, long length)
        {
            byte[] wkhtmltoxContent = new byte[length];

            stream.Read(wkhtmltoxContent, 0, (int)length);

            return wkhtmltoxContent;
        }
    }
}