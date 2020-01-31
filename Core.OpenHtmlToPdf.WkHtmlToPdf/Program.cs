using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Core.OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX;

namespace Core.OpenHtmlToPdf.WkHtmlToPdf
{
    internal static class Program
    {
        private static int Main()
        {
            try
            {
                ConvertStandardInputToPdf();

                return 0;
            }
            catch (Exception ex)
            {
                WriteExceptionMessageToStandardError(ex);

                return -1;
            }
        }

        private static void WriteExceptionMessageToStandardError(Exception ex)
        {
            using (Stream standardErrort = Console.OpenStandardError())
            {
                using (StreamWriter writer = new StreamWriter(standardErrort))
                {
                    writer.WriteAsBase64EncodedString(ex.Message);
                }
            }
        }

        private static void ConvertStandardInputToPdf() => ConvertToPdf(ConversionSource());

        private static ConversionSource ConversionSource()
        {
            using (Stream standardInput = Console.OpenStandardInput())
            {
                using (StreamReader streamReader = new StreamReader(standardInput))
                {
                    return DeserializeBase64EncodedSource<ConversionSource>(streamReader.ReadToEnd());
                }
            }
        }

        private static T DeserializeBase64EncodedSource<T>(string base64EncodedObject)
        {
            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(base64EncodedObject)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
            }
        }

        private static void ConvertToPdf(ConversionSource conversionSource)
        {
            using (WkHtmlToPdfContext wkhtmlToPdfContext = WkHtmlToPdfContext.Create())
            {
                foreach (KeyValuePair<string, string> globalSetting in conversionSource.GlobalSettings)
                {
                    WkHtmlToX.WkHtmlToPdf.wkhtmltopdf_set_global_setting(wkhtmlToPdfContext.GlobalSettingsPointer,
                        globalSetting.Key,
                        globalSetting.Value);
                }

                foreach (KeyValuePair<string, string> objectSetting in conversionSource.ObjectSettings)
                {
                    WkHtmlToX.WkHtmlToPdf.wkhtmltopdf_set_object_setting(wkhtmlToPdfContext.ObjectSettingsPointer,
                        objectSetting.Key,
                        objectSetting.Value);
                }

                wkhtmlToPdfContext.Convert(conversionSource.Html);
            }
        }

        private static void WriteAsBase64EncodedString(this TextWriter writer, string str) => writer.Write(Convert.ToBase64String(Encoding.UTF8.GetBytes(str)));
    }
}
