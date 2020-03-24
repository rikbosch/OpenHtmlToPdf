using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using OpenHtmlToPdf.Tests.Helpers;
using Xunit;

namespace Core.OpenHtmlToPdf.Tests
{
    public class TestHtmlToPdf
    {
        private const string _htmlDocumentFormat = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title>Title</title></head><body>{0}</body></html>";
        private const double _letterShortLengthInPostScriptPoints = 612;
        private const double _letterLongLengthInPostScriptPoints = 792;

        [Fact]
        public async Task Pdf_document_content()
        {
            const string expectedDocumentContent = "Hello World!";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            var result = await Pdf.From(html).EncodedWith("utf-8").Content();
            string text = PdfDocumentReader.ToText(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
        }

        [Fact]
        public async Task Text_encoding()
        {
            const string expectedDocumentContent = "ƒцех";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            byte[] result = await Pdf.From(html).EncodedWith("utf-8").Content();
            string text = PdfDocumentReader.ToText(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
        }

        [Fact]
        public async Task Document_title()
        {
            const string expectedTitle = "Hello World!";
            const string expectedDocumentContent = "Hello User!";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            byte[] result = await Pdf.From(html)
                .WithTitle(expectedTitle)
                .Content();

            string title = PdfDocumentReader.Title(result);
            string text = PdfDocumentReader.ToText(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
            title.Should().BeEquivalentTo(expectedTitle);
        }

        [Fact]
        public async Task Page_size()
        {
            const string expectedDocumentContent = "Hello World!";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            byte[] result = await Pdf.From(html)
                .OfSize(PaperSize.Letter)
                .Content();

            string text = PdfDocumentReader.ToText(result);
            double width = PdfDocumentReader.WidthOfFirstPage(result);
            double height = PdfDocumentReader.HeightOfFirstPage(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
            width.Should().BeApproximately(_letterShortLengthInPostScriptPoints, 0.1);
            height.Should().BeApproximately(_letterLongLengthInPostScriptPoints, 0.1);
        }

        [Fact]
        public async Task Portrait()
        {
            const string expectedDocumentContent = "Hello World!";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            byte[] result = await Pdf.From(html)
                .OfSize(PaperSize.Letter)
                .Portrait()
                .Content();

            string text = PdfDocumentReader.ToText(result);
            double width = PdfDocumentReader.WidthOfFirstPage(result);
            double height = PdfDocumentReader.HeightOfFirstPage(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
            width.Should().BeApproximately(_letterShortLengthInPostScriptPoints, 0.1);
            height.Should().BeApproximately(_letterLongLengthInPostScriptPoints, 0.1);
        }

        [Fact]
        public async Task Landscape()
        {
            const string expectedDocumentContent = "Hello World!";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            byte[] result = await Pdf.From(html)
                .OfSize(PaperSize.Letter)
                .Landscape()
                .Content();

            string text = PdfDocumentReader.ToText(result);
            double width = PdfDocumentReader.WidthOfFirstPage(result);
            double height = PdfDocumentReader.HeightOfFirstPage(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
            height.Should().BeApproximately(_letterShortLengthInPostScriptPoints, 0.1);
            width.Should().BeApproximately(_letterLongLengthInPostScriptPoints, 0.1);
        }

        [Fact]
        public async Task Margins()
        {
            const string expectedDocumentContent = "Hello World!";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            byte[] result = await Pdf.From(html).WithMargins(1.25.Centimeters()).Content();
            string text = PdfDocumentReader.ToText(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
        }

        [Fact]
        public async Task With_outline()
        {
            const string expectedDocumentContent = "Hello World!";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            byte[] result = await Pdf.From(html).WithOutline().Content();
            string text = PdfDocumentReader.ToText(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
        }

        [Fact]
        public async Task Without_outline()
        {
            const string expectedDocumentContent = "Hello World!";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            byte[] result = await Pdf.From(html).WithoutOutline().Content();
            string text = PdfDocumentReader.ToText(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
        }

        [Fact]
        public async Task Compressed()
        {
            const string expectedDocumentContent = "Hello World!";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            byte[] result = await Pdf.From(html).Comressed().Content();
            string text = PdfDocumentReader.ToText(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
        }

        [Fact]
        public async Task Is_directory_agnostic()
        {
            const string expectedDocumentContent = "Hello World!";
            string html = string.Format(_htmlDocumentFormat, expectedDocumentContent);

            Directory.SetCurrentDirectory(@"c:\");
            byte[] result = await Pdf.From(html).Content();
            string text = PdfDocumentReader.ToText(result);

            text.Should().BeEquivalentTo(expectedDocumentContent);
        }
    }
}
