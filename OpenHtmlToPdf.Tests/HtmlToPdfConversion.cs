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

            var result = HtmlToPdfConverter.ConvertToPdf(html);

            TextAssert.Contains(PdfDocument.ReadDocumentContent(result), expectedDocumentContent);
        }

        [TestMethod]
        public void Convert_multiple_documents_simultaneously()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);
            const int numbeOfDocuments = 10;
            var results = Enumerable
                .Range(0, numbeOfDocuments)
                .Select(i => Task.Factory.StartNew(() => HtmlToPdfConverter.ConvertToPdf(html)))
                .ToArray();

            Task.WaitAll(results);

            foreach (var result in results)
                TextAssert.Contains(PdfDocument.ReadDocumentContent(result.Result), expectedDocumentContent);
        }
    }
}
