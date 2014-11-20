using System.Drawing.Printing;
using WebKitHtmlToPdf.TuesPechkin;

namespace WebKitHtmlToPdf
{
    public static class HtmlToPdfConverter
    {
        public static byte[] ConvertToPdf(string html)
        {
            return PechkinHtmlToPdfConverter.Convert(html);
        }
    }
}
