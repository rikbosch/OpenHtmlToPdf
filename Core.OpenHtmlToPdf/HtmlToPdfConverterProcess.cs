using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Core.OpenHtmlToPdf.Assets;
using Core.OpenHtmlToPdf.WkHtmlToPdf;

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
            ProcessStartInfo processStartInfo = GetProcessStartInfo();
            Process process = Process.Start(processStartInfo);

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
                throw new PdfDocumentCreationFailedException(process.StandardError.ReadToEnd());
            }
        }

        private static void WriteToStandardInput(this Process process, ConversionSource conversionSource)
        {
            process.StandardInput.Write(SerializeToBase64EncodedString(conversionSource));
            process.StandardInput.Close();
        }

        private static string SerializeToBase64EncodedString(ConversionSource conversionSource)
        {
            string result = SerializeToJson(conversionSource);
            return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(result));
        }

        private static string SerializeToJson(ConversionSource conversionSource)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new DataContractJsonSerializer(typeof(ConversionSource)).WriteObject(stream, conversionSource);
                stream.Position = 0;
                return new StreamReader(stream).ReadToEnd();
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
            ConversionSource conversionSource = new ConversionSource
            {
                Html = html,
                GlobalSettings = globalSettings,
                ObjectSettings = objectSettings
            };
            return conversionSource;
        }
    }
}