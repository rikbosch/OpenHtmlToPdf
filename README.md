# OpenHtmlToPdf

.NET library for rendering HTML documents to PDF format.

OpenHtmlToPdf uses [WkHtmlToPdf](http://github.com/antialize/wkhtmltopdf) native Windows library for HTML to PDF rendering.

## Download

This source code can be compiled.

## Usage

### Generate PDF with default settings

    const string html =
    	"<!DOCTYPE html>" +
    	"<html>" +
    	"<head><meta charset='UTF-8'><title>Title</title></head>" +
    	"<body>Body text...</body>" +
    	"</html>";

    var pdf = Pdf
    	.From(html)
    	.Content();

### Defining fluent settings

    const string html =
    	"<!DOCTYPE html>" +
    	"<html>" +
    	"<head><meta charset='UTF-8'><title>Title</title></head>" +
    	"<body>Body text...</body>" +
    	"</html>";

    var pdf = Pdf
    	.From(html)
    	.OfSize(PaperSize.A4)
    	.WithTitle("Title")
    	.WithoutOutline()
    	.WithMargins(1.25.Centimeters())
    	.Portrait()
    	.Comressed()
    	.Content();

### Defining wkhtmltopdf settings directly

[Settings API documentation](http://wkhtmltopdf.org/libwkhtmltox/pagesettings.html)

    const string html =
    	"<!DOCTYPE html>" +
    	"<html>" +
    	"<head><meta charset='UTF-8'><title>Title</title></head>" +
    	"<body>Body text...</body>" +
    	"</html>";

    var pdf = Pdf
    	.From(html)
    	.WithGlobalSetting("orientation", "Landscape")
    	.WithObjectSetting("web.defaultEncoding", "utf-8")
    	.Content();

#### License

This work, "OpenHtmlToPdf", is a port to .Net Core of the code by [Timo Vilppu](https://github.com/vilppu/OpenHtmlToPdf). His work was a derivative of ["TuesPechkin" by tuespetre (Derek Gray)](https://github.com/tuespetre/TuesPechkin) used under the Creative Commons Attribution 3.0 license.

This work is made available under the terms of the Creative Commons Attribution 3.0 license (viewable at http://creativecommons.org/licenses/by/3.0/)
