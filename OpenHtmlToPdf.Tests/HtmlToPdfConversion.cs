using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenHtmlToPdf.Tests.Helpers;

namespace OpenHtmlToPdf.Tests
{
    [TestClass]
    public class HtmlToPdfConversion
    {
        private const string HtmlDocumentFormat = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title>Title</title></head><body>{0}</body></html>";
        private const int _210mmInPostScriptPoints = 595;
        private const int _297mmInPostScriptPoints = 842;

        [TestMethod]
        public void Pdf_document_content()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
        }

        [TestMethod]
        public void Text_encoding()
        {
            const string expectedDocumentContent = "Äöåõ";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).EncodedWith("utf-8").Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
        }

        [TestMethod]
        public void Document_title()
        {
            const string expectedTitle = "Expected title";
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).WithTitle(expectedTitle).Content();

            Assert.AreEqual(expectedTitle, PdfDocumentReader.Title(result));
            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
        }

        [TestMethod]
        public void Page_size()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).OfSize(PaperSize.A4).Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
            Assert.AreEqual(_210mmInPostScriptPoints, PdfDocumentReader.WidthOfFirstPage(result));
            Assert.AreEqual(_297mmInPostScriptPoints, PdfDocumentReader.HeightOfFirstPage(result));
        }

        [TestMethod]
        public void Portrait()
        {
            const string expectedDocumentContent = "Expected document content";

            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).Portrait().Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
            Assert.AreEqual(_210mmInPostScriptPoints, PdfDocumentReader.WidthOfFirstPage(result));
            Assert.AreEqual(_297mmInPostScriptPoints, PdfDocumentReader.HeightOfFirstPage(result));
        }

        [TestMethod]
        public void Landscape()
        {
            const string expectedDocumentContent = "Expected document content";

            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).Landscape().Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
            Assert.AreEqual(_297mmInPostScriptPoints, PdfDocumentReader.WidthOfFirstPage(result));
            Assert.AreEqual(_210mmInPostScriptPoints, PdfDocumentReader.HeightOfFirstPage(result));
        }

        [TestMethod]
        public void Margins()
        {
            const string expectedDocumentContent = "Expected document content";

            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).WithMargins(1.25.Centimeters()).Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
        }

        [TestMethod]
        public void With_outline()
        {
            const string expectedDocumentContent = "Expected document content";

            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).WithOutline().Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
        }

        [TestMethod]
        public void Without_outline()
        {
            const string expectedDocumentContent = "Expected document content";

            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).WithoutOutline().Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
        }

        [TestMethod]
        public void Compressed()
        {
            const string expectedDocumentContent = "Expected document content";

            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var result = Pdf.From(html).Comressed().Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
        }

        [TestMethod]
        public void Is_directory_agnostic()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            Directory.SetCurrentDirectory(@"c:\");
            var result = Pdf.From(html).Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(result));
        }

        [TestMethod]
        public void Convert_multiple_documents_concurrently()
        {
            const string expectedDocumentContent = "Expected document content";
            const int documentCount = 10;
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);
            var tasks = new List<Task<byte[]>>();

            for (var i = 0; i < documentCount; i++)
                tasks.Add(Task.Run(() => Pdf.From(html).Content()));

            Task.WaitAll(tasks.OfType<Task>().ToArray());

            foreach (var task in tasks)
                TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(task.Result));
        }

        [TestMethod]
        public void Convert_multiple_documents_sequently()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);

            var first = Pdf.From(html).Content();
            var second = Pdf.From(html).Content();
            var third = Pdf.From(html).Content();

            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(first));
            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(second));
            TextAssert.AreEqual(expectedDocumentContent, PdfDocumentReader.ToText(third));
        }
    }
}