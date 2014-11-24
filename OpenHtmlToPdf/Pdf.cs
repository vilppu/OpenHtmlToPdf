namespace OpenHtmlToPdf
{
    public sealed class Pdf
    {
<<<<<<< HEAD
        private static readonly object SyncRoot = new object();

        public static byte[] From(string html)
=======
        public static IPdfDocument From(string html)
>>>>>>> origin/master
        {
            return new PdfDocument(html);
        }
    }
}
