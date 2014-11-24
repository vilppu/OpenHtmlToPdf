#OpenHtmlToPdf

.NET library for rendering HTML documents to PDF format. 

OpenHtmlToPdf uses [WkHtmlToPdf](http://github.com/antialize/wkhtmltopdf) native Windows library for HTML to PDF rendering.

##Download

OpenHtmlToPdf can be download as a [NuGet package] (https://www.nuget.org/packages/OpenHtmlToPdf/)

##Usage
	const string htmlDocument = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title>Title</title></head><body>Body text...</body></html>";

	var pdf = Pdf.From(html).WithTitle("Title").Content();

License
-------

This work, "OpenHtmlToPdf", is a derivative of "TuesPechkin" by tuespetre (Derek Gray) used under the Creative Commons Attribution 3.0 license.

This work is made available under the terms of the Creative Commons Attribution 3.0 license (viewable at http://creativecommons.org/licenses/by/3.0/) by Timo Vilppu
