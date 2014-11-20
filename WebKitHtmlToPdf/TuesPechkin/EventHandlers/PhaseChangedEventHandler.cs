using WebKitHtmlToPdf.TuesPechkin;

namespace WebKitHtmlToPdf.EventHandlers
{
    delegate void PhaseChangedEventHandler(IPechkin converter, int phaseNumber, string phaseDescription);
}