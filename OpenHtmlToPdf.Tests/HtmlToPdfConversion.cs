using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenHtmlToPdf;
using OpenHtmlToPdf.Tests.Helpers;
using Document = OpenHtmlToPdf.Document;

namespace OpenHtmlToPdf.Tests
{
    [TestClass]
    public class HtmlToPdfConversion
    {
        private const string HtmlDocumentFormat = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title>Title</title></head><body>{0}</body></html>";

        [TestMethod]
        public void HTML_document_is_converted_to_PDF_document()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Document.From(html).Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(result));
        }

        [TestMethod]
        public void Text_encoding()
        {
            const string expectedDocumentContent = "Äöåõ";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Document.From(html).EncodedWith("utf-8").Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(result));
        }

        [TestMethod]
        public void Convert_multiple_documents_sequently()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var first = Document.From(html).Content();
            var second = Document.From(html).Content();
            var third = Document.From(html).Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(first));
            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(second));
            TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(third));
        }

        [TestMethod]
        public void Convert_multiple_documents_concurrently()
        {
            const string expectedDocumentContent = "Expected document content";
            const int documentCount = 10;
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);
            var tasks = new List<Task<byte[]>>();

            for (var i = 0; i < documentCount; i++)
            tasks.Add(Task.Run(() => Document.From(html).Content()));
            
            Task.WaitAll(tasks.OfType<Task>().ToArray());

            foreach (var task in tasks)
                TextAssert.AreEqual(expectedDocumentContent, PdfDocument.ToText(task.Result));
        }

        [TestMethod]
        public void Document_title()
        {
            const string expectedTitle = "Expected title";
            var html = string.Format(HtmlDocumentFormat, "");

            var result = Document.From(html).WithTitle(expectedTitle).Content();

            Assert.AreEqual(expectedTitle, PdfDocument.Title(result));
        }

        [TestMethod]
        public void Page_size()
        {
            var html = string.Format(HtmlDocumentFormat, "");

            var result = Document.From(html).OfSize(PaperSize.A4).Content();

            Assert.AreEqual(595, PdfDocument.WidthOfFirstPage(result)); // 210mm is 595 PostScript points where 1 pt = 25.4/72 mm
            Assert.AreEqual(842, PdfDocument.HeightOfFirstPage(result)); // 297mm is 842 PostScript points where 1 pt = 25.4/72 mm
        }

        [TestMethod]
        public void Margins()
        {
            var html = string.Format(HtmlDocumentFormat, "");

            Document.From(html).With(new PaperMargins(5, "mm")).Content();
        }

        [TestMethod]
        public void Outline()
        {
            var html = string.Format(HtmlDocumentFormat, "");

            Document.From(html).WithOutline().Content();
        }

        [TestMethod]
        public void Orientation()
        {
            var html = string.Format(HtmlDocumentFormat, "");

            Document.From(html).Oriented(PaperOrientation.Landscape).Content();
        }
    }
}
