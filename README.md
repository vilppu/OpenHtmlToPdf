#OpenHtmlToPdf

.NET library for rendering HTML documents to PDF format. 

OpenHtmlToPdf uses [WkHtmlToPdf](http://github.com/antialize/wkhtmltopdf) native Windows library for HTML to PDF rendering.

##Download

OpenHtmlToPdf can be download as a [NuGet package] (https://www.nuget.org/packages/OpenHtmlToPdf/)

##Usage
	const string html = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title>Title</title></head><body>Body text...</body></html>";

	var pdf = Document
		.From(html)
		.WithTitle("Title")
		.OfSize(PaperSize.A4)
		.Comressed()
		.Oriented(PaperOrientation.Landscape)
		.WithOutline()
		.Content();

License
-------

This work, "OpenHtmlToPdf", is a derivative of "TuesPechkin" by tuespetre (Derek Gray) used under the Creative Commons Attribution 3.0 license.

This work is made available under the terms of the Creative Commons Attribution 3.0 license (viewable at http://creativecommons.org/licenses/by/3.0/) by Timo Vilppu
