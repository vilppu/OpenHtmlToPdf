using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenHtmlToPdf.Pdf;
using OpenHtmlToPdf.Tests.Helpers;
using PdfDocument = OpenHtmlToPdf.Tests.Helpers.PdfDocument;

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

            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(result));
        }

        [TestMethod]
        public void Text_encoding()
        {
            const string expectedDocumentContent = "Äöåõ";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(result));
        }

        [TestMethod]
        public void Convert_multiple_documents_sequently()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var first = Pdf.From(html).Content();
            var second = Pdf.From(html).Content();
            var third = Pdf.From(html).Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(first));
            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(second));
            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(third));
        }

        [TestMethod]
        public void Convert_multiple_documents_concurrently()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var first = Task.Run(() =>  Pdf.From(html).Content());
            var second = Task.Run(() => Pdf.From(html).Content());
            var third = Task.Run(() => Pdf.From(html).Content());

            Task.WaitAll(first, second, third);

            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(first.Result));
            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(second.Result));
            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(third.Result));
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

            var result = Pdf.From(html).OfSize(PaperSize.A4).Content();

            Assert.AreEqual(595, PdfDocument.WidthOfFirstPage(result)); // 210mm is 595 PostScript points where 1 pt = 25.4/72 mm
            Assert.AreEqual(842, PdfDocument.HeightOfFirstPage(result)); // 297mm is 842 PostScript points where 1 pt = 25.4/72 mm
        }

        [TestMethod]
        public void Margins()
        {
            var html = string.Format(HtmlDocumentFormat, "");

            Pdf.From(html).With(new PaperMargins(5, "mm")).Content();
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

            Pdf.From(html).Oriented(PaperOrientation.Landscape).Content();
        }
    }
}
