using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenHtmlToPdf.Tests.Helpers;

namespace OpenHtmlToPdf.Tests
{
    [TestClass]
    public class HtmlToPdfConversion
    {
        private const string HtmlDocumentFormat = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title></title></head><body>{0}</body></html>";

        [TestMethod]
        public void HTML_document_is_converted_to_PDF_document()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).Content();

            TextAssert.Contains(PdfDocument.ToText(result), expectedDocumentContent);
        }

        [TestMethod]
        public void Convert_multiple_documents_simultaneously()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);
            const int numbeOfDocuments = 2;
            var results = Enumerable
                .Range(0, numbeOfDocuments)
                .Select(i => Task.Factory.StartNew(() => Pdf.From(html).Content()))
                .ToArray();

            // ReSharper disable once CoVariantArrayConversion
            Task.WaitAll(results);

            foreach (var result in results)
                TextAssert.Contains(PdfDocument.ToText(result.Result), expectedDocumentContent);
        }

        [TestMethod]
        public void Document_title()
        {
            const string expectedTitle = "Expected title";
            var html = string.Format(HtmlDocumentFormat, "");

            var result = Pdf.From(html).WithTitle(expectedTitle).Content();

            Assert.AreEqual(expectedTitle, PdfDocument.Title(result));
        }

        [TestMethod]
        public void Page_size()
        {
            var html = string.Format(HtmlDocumentFormat, "");

            var result = Pdf.From(html).WithPaperSize(PaperSize.A4).Content();

            Assert.AreEqual(595, PdfDocument.WidthOfFirstPage(result)); // 210mm is 595 PostScript points where 1 pt = 25.4/72 mm
            Assert.AreEqual(842, PdfDocument.HeightOfFirstPage(result)); // 297mm is 842 PostScript points where 1 pt = 25.4/72 mm
        }

        [TestMethod]
        public void Margins()
        {
            var html = string.Format(HtmlDocumentFormat, "");

            Pdf.From(html).WithMargins(new PaperMargins(5, "mm")).Content();
        }

        [TestMethod]
        public void Outline()
        {
            var html = string.Format(HtmlDocumentFormat, "");

            Pdf.From(html).WithOutline().Content();
        }

        [TestMethod]
        public void Orientation()
        {
            var html = string.Format(HtmlDocumentFormat, "");

            Pdf.From(html).WithPaperOrientation(PaperOrientation.Landscape).Content();
        }
    }
}
