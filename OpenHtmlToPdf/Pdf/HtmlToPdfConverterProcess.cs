using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using OpenHtmlToPdf.WkHtmlToPdf;

namespace OpenHtmlToPdf.Pdf
{
    static class HtmlToPdfConverterProcess
    {
        public static byte[] ConvertToPdf(
            string html,
            IDictionary<string, string> globalSettings,
            IDictionary<string, string> objectSettings)
        {
            var conversionSource = new ConversionSource
            {
                Html = html,
                GlobalSettings = globalSettings,
                ObjectSettings = objectSettings
            };

            var processStartInfo = new ProcessStartInfo
            {
                FileName = typeof(ConversionSource).Assembly.GetName().Name + ".exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(processStartInfo);

            var serializeObject = JsonConvert.SerializeObject(conversionSource);
            process.StandardInput.Write(serializeObject);
            process.StandardInput.Close();
            var base64EncodedPdf = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            var error = process.StandardError.ReadToEnd();
            if (!string.IsNullOrWhiteSpace(error))
            {
                throw JsonConvert.DeserializeObject<Exception>(error);
            }

            return Convert.FromBase64String(base64EncodedPdf);
        }
    }
}