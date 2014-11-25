namespace OpenHtmlToPdf
{
    public interface IPdfDocument
    {
        IPdfDocument WithGlobalSetting(string key, string value);
        IPdfDocument WithObjectSetting(string key, string value);
        byte[] Content();
    }
}