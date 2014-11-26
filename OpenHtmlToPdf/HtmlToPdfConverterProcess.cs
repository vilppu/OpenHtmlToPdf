using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;
using OpenHtmlToPdf.WkHtmlToPdf;

namespace OpenHtmlToPdf
{
    static class HtmlToPdfConverterProcess
    {
        public static byte[] ConvertToPdf(
            string html,
            IDictionary<string, string> globalSettings,
            IDictionary<string, string> objectSettings)
        {
            return Convert(ToConversionSource(html, globalSettings, objectSettings));
        }

        private static byte[] Convert(ConversionSource conversionSource)
        {
            var processStartInfo = GetProcessStartInfo();
            var process = Process.Start(processStartInfo);

            return process.Convert(conversionSource);
        }

        private static byte[] Convert(this Process process, ConversionSource conversionSource)
        {
            process.WriteToStandardInput(conversionSource);

            var base64EncodedPdf = process.ReadBase64EncodedPdfFromStandardOutput();
            var pdf = System.Convert.FromBase64String(base64EncodedPdf);

            RaiseExceptionIfErrorOccured(process);
            RaiseExceptionIfEmpty(base64EncodedPdf);

            return pdf;
        }

        // ReSharper disable once UnusedParameter.Local
        private static void RaiseExceptionIfEmpty(string base64EncodedPdf)
        {
            if (string.IsNullOrWhiteSpace(base64EncodedPdf))
                throw new PdfDocumentCreationFailedException("Empty document generated");
        }

        private static void RaiseExceptionIfErrorOccured(Process process)
        {
            if (process.ExitCode != 0)
                throw new PdfDocumentCreationFailedException(process.StandardError.ReadToEnd());
        }

        private static string ReadBase64EncodedPdfFromStandardOutput(this Process process)
        {
            var base64EncodedPdf = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            return base64EncodedPdf;
        }

        private static void WriteToStandardInput(this Process process, ConversionSource conversionSource)
        {
            process.StandardInput.Write(SerializeToBase64EncodedString(conversionSource));
            process.StandardInput.Close();
        }

        private static string SerializeToBase64EncodedString(ConversionSource conversionSource)
        {
            return System.Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(conversionSource)));
        }

        private static ProcessStartInfo GetProcessStartInfo()
        {
            return new ProcessStartInfo
            {
                FileName = ConverterExeutable.PathToConverterExecutable(),
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
        }

        private static ConversionSource ToConversionSource(string html, IDictionary<string, string> globalSettings, IDictionary<string, string> objectSettings)
        {
            var conversionSource = new ConversionSource
            {
                Html = html,
                GlobalSettings = globalSettings,
                ObjectSettings = objectSettings
            };
            return conversionSource;
        }
    }
}