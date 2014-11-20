using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebKitHtmlToPdf.Tests.Helpers
{
    public static class TextAssert
    {
        private static readonly Regex WhitespaceOrLineFeedRegex = new Regex(@"[\s\n\r]");

        public static void Contains(string value, string substring)
        {
            ContainsExact(value.ToLowerCaseSingleLine(), substring.ToLowerCaseSingleLine());
        }

        private static void ContainsExact(string value, string substring)
        {
            StringAssert.Contains(value, substring);
        }

        private static string ToLowerCaseSingleLine(this string text)
        {
            return text
                .ToSingleLine()
                .ToLower();
        }

        private static string ToSingleLine(this string text)
        {
            return WhitespaceOrLineFeedRegex.Replace(text, "");
        }
    }
}