using OpenHtmlToPdf.TuesPechkin;

namespace OpenHtmlToPdf.EventHandlers
{
    delegate void ProgressChangedEventHandler(IPechkin converter, int progress, string progressDescription);
}