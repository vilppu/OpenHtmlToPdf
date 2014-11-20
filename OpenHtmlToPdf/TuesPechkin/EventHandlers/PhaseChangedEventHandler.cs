using OpenHtmlToPdf.TuesPechkin;

namespace OpenHtmlToPdf.EventHandlers
{
    delegate void PhaseChangedEventHandler(IPechkin converter, int phaseNumber, string phaseDescription);
}