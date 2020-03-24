using System;
using System.Globalization;

namespace Core.OpenHtmlToPdf
{
    public static class FluentSettings
    {
        public static IPdfDocument Comressed(this IPdfDocument pdfDocument) => pdfDocument.WithGlobalSetting("useCompression", "true");

        public static IPdfDocument WithTitle(this IPdfDocument pdfDocument, string title) => pdfDocument.WithGlobalSetting("documentTitle", title);

        public static IPdfDocument WithOutline(this IPdfDocument pdfDocument) => pdfDocument.WithGlobalSetting("outline", "true");

        public static IPdfDocument WithoutOutline(this IPdfDocument pdfDocument) => pdfDocument.WithGlobalSetting("outline", "false");

        public static IPdfDocument EncodedWith(this IPdfDocument pdfDocument, string encoding) => pdfDocument.WithObjectSetting("web.defaultEncoding", encoding);

        public static IPdfDocument Landscape(this IPdfDocument pdfDocument) => pdfDocument
                .WithGlobalSetting("orientation", "Landscape");

        public static IPdfDocument Portrait(this IPdfDocument pdfDocument) => pdfDocument
                .WithGlobalSetting("orientation", "Portrait");

        public static IPdfDocument OfSize(this IPdfDocument pdfDocument, PaperSize paperSize) => pdfDocument
                .WithGlobalSetting("size.width", paperSize.Width)
                .WithGlobalSetting("size.height", paperSize.Height);

        public static IPdfDocument WithMargins(this IPdfDocument pdfDocument, Func<PaperMargins, PaperMargins> paperMargins) => pdfDocument.WithMargins(paperMargins(PaperMargins.None()));

        public static IPdfDocument WithMargins(this IPdfDocument pdfDocument, PaperMargins margins) => pdfDocument
                .WithGlobalSetting("margin.bottom", margins.BottomSetting)
                .WithGlobalSetting("margin.left", margins.LeftSetting)
                .WithGlobalSetting("margin.right", margins.RightSetting)
                .WithGlobalSetting("margin.top", margins.TopSetting);

        public static IPdfDocument WithResolution(this IPdfDocument pdfDocument, int dpi) => pdfDocument
                .WithGlobalSetting("dpi", dpi.ToString(CultureInfo.InvariantCulture));
    }
}