using System;
using System.IO;
using System.Threading.Tasks;

namespace Core.OpenHtmlToPdf
{
    public static class TemporaryPdf
    {
        public static async Task<byte[]> ReadTemporaryFileContent(string temporaryFilename)
        {
            using (FileStream temporaryFile = new FileStream(temporaryFilename, FileMode.Open, FileAccess.Read))
            {
                byte[] content = new byte[temporaryFile.Length];

                await temporaryFile.ReadAsync(content, 0, content.Length);

                return content;
            }
        }

        public static async Task CopyToAsync(string temporaryFilename, Stream target)
        {
            using (FileStream temporaryFile = new FileStream(temporaryFilename, FileMode.Open, FileAccess.Read))
            {
                await temporaryFile.CopyToAsync(target);
            }
        }

        public static void DeleteTemporaryFile(string temporaryFilename)
        {
            try
            {
                if (File.Exists(temporaryFilename))
                {
                    File.Delete(temporaryFilename);
                }
            }
            catch (IOException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        public static string TemporaryFilePath() => Path.Combine(Path.GetTempPath(), "Core.OpenHtmlToPdf", TemporaryFilename());

        private static string TemporaryFilename() => Guid.NewGuid().ToString("N") + ".pdf";
    }
}