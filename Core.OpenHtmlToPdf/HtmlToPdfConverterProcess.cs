using Core.OpenHtmlToPdf.Assets;
using Core.OpenHtmlToPdf.WkHtmlToPdf;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Core.OpenHtmlToPdf
{
    internal static class HtmlToPdfConverterProcess
    {
        public static void ConvertToPdf(
            string html,
            IDictionary<string, string> globalSettings,
            IDictionary<string, string> objectSettings) => Convert(ToConversionSource(html, globalSettings, objectSettings));

        private static void Convert(ConversionSource conversionSource)
        {
            var processStartInfo = GetProcessStartInfo();
            var process = Process.Start(processStartInfo);

            process.Convert(conversionSource);
        }

        private static void Convert(this Process process, ConversionSource conversionSource)
        {
            process.WriteToStandardInput(conversionSource);
            process.WaitForExit();
            RaiseExceptionIfErrorOccured(process);
        }

        private static void RaiseExceptionIfErrorOccured(Process process)
        {
            if (process.ExitCode != 0)
            {
                var errorMessageBase64 = process.StandardError.ReadToEnd();
                var errorDecoded = Encoding.UTF8.GetString(System.Convert.FromBase64String(errorMessageBase64));

                throw new PdfDocumentCreationFailedException(errorDecoded);
            }
        }

        private static void WriteToStandardInput(this Process process, ConversionSource conversionSource)
        {
            process.StandardInput.Write(SerializeToBase64EncodedString(conversionSource));
            process.StandardInput.Close();
        }

        private static string SerializeToBase64EncodedString(ConversionSource conversionSource)
        {
            var result = SerializeToJson(conversionSource);
            var args = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(result));
            return args;
        }

        private static string SerializeToJson(ConversionSource conversionSource)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(typeof(ConversionSource)).WriteObject(ms, conversionSource);

                ms.Position = 0;
                return Encoding.UTF8.GetString(ms.GetBuffer());
            }
        }

        private static ProcessStartInfo GetProcessStartInfo() => new ProcessStartInfo
        {
            FileName = ConverterExecutable.Get().FullConverterExecutableFilename,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        private static ConversionSource ToConversionSource(string html, IDictionary<string, string> globalSettings, IDictionary<string, string> objectSettings)
        {
            var conversionSource = new ConversionSource
            {
                Html = html,
                GlobalSettings = globalSettings,
                ObjectSettings = objectSettings
            };
            return conversionSource;
        }
    }
}