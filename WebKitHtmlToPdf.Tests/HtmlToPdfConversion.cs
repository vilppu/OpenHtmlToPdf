using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebKitHtmlToPdf.Tests.Helpers;

namespace WebKitHtmlToPdf.Tests
{
    [TestClass]
    public class HtmlToPdfConversion
    {
        [TestMethod]
        public void HTML_document_is_converted_to_PDF_document()
        {
            const string expectedDocumentContent = "Expected document content";
            const string htmlDocumentFormat = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title></title></head><body>{0}</body></html>";
            var html = string.Format(htmlDocumentFormat, expectedDocumentContent);

            var result = HtmlToPdfConverter.ConvertToPdf(html);

            TextAssert.Contains(PdfDocument.ReadDocumentContent(result), expectedDocumentContent);
        }

        [TestMethod]
        public void Convert_multiple_documents_simultaneously()
        {
            const string expectedDocumentContent = "Expected document content";
            const string htmlDocumentFormat = "<!DOCTYPE html><html><head><meta charset='UTF-8'><title></title></head><body>{0}</body></html>";
            var html = string.Format(htmlDocumentFormat, expectedDocumentContent);
            const int numbeOfDocuments = 1;
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
