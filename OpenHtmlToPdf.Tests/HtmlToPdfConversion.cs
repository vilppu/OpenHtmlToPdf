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

            var result = Pdf.From(html);

            TextAssert.Contains(PdfToTextConverter.ToText(result), expectedDocumentContent);
        }

        [TestMethod]
        public void Convert_multiple_documents_simultaneously()
        {
            const string expectedDocumentContent = "Expected document content";
            var html = string.Format(HtmlDocumentFormat, expectedDocumentContent);
            const int numbeOfDocuments = 2;
            var results = Enumerable
                .Range(0, numbeOfDocuments)
                .Select(i => Task.Factory.StartNew(() => Pdf.From(html)))
                .ToArray();

            // ReSharper disable once CoVariantArrayConversion
            Task.WaitAll(results);

            foreach (var result in results)
                TextAssert.Contains(PdfToTextConverter.ToText(result.Result), expectedDocumentContent);
        }
    }
}
