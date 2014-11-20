using System.Drawing.Printing;
using OpenHtmlToPdf.TuesPechkin;

namespace OpenHtmlToPdf
{
    public static class HtmlToPdfConverter
    {
        public static byte[] ConvertToPdf(string html)
        {
            return PechkinHtmlToPdfConverter.Convert(html);
        }
    }
}
