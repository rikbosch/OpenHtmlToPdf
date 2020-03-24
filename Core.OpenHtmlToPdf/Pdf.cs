using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.OpenHtmlToPdf
{
    public sealed class Pdf
    {
        public static IPdfDocument From(string html) => DocumentBuilder.Containing(html);

        private sealed class DocumentBuilder : IPdfDocument
        {
            private readonly string _html;
            private readonly IDictionary<string, string> _globalSettings;
            private readonly IDictionary<string, string> _objectSettings;

            private DocumentBuilder(string html, IDictionary<string, string> globalSettings, IDictionary<string, string> objectSettings)
            {
                _html = html;
                _globalSettings = globalSettings;
                _objectSettings = objectSettings;
            }

            public static DocumentBuilder Containing(string html) => new DocumentBuilder(
                    html,
                    new Dictionary<string, string>(),
                    new Dictionary<string, string>());

            public IPdfDocument WithGlobalSetting(string key, string value)
            {
                Dictionary<string, string> globalSettings = _globalSettings.ToDictionary(e => e.Key, e => e.Value);

                globalSettings[key] = value;

                return new DocumentBuilder(_html, globalSettings, _objectSettings);
            }

            public IPdfDocument WithObjectSetting(string key, string value)
            {
                Dictionary<string, string> objectSetting = _objectSettings.ToDictionary(e => e.Key, e => e.Value);

                objectSetting[key] = value;

                return new DocumentBuilder(_html, _globalSettings, objectSetting);
            }

            public async Task WriteToStream(Stream target)
            {
                var temporaryFilename = TemporaryPdf.TemporaryFilePath();
                _globalSettings["out"] = temporaryFilename;

                HtmlToPdfConverterProcess.ConvertToPdf(_html, _globalSettings, _objectSettings);
                try
                {
                    await TemporaryPdf.CopyToAsync(temporaryFilename, target);
                }
                finally
                {
                    TemporaryPdf.DeleteTemporaryFile(temporaryFilename);
                }
            }

            public Task<byte[]> Content() => ReadContentUsingTemporaryFile(TemporaryPdf.TemporaryFilePath());

            private async Task<byte[]> ReadContentUsingTemporaryFile(string temporaryFilename)
            {
                _globalSettings["out"] = temporaryFilename;

                HtmlToPdfConverterProcess.ConvertToPdf(_html, _globalSettings, _objectSettings);
                try
                {
                    return await TemporaryPdf.ReadTemporaryFileContent(temporaryFilename);

                }
                finally
                {
                    TemporaryPdf.DeleteTemporaryFile(temporaryFilename);
                }


            }
        }
    }
}
