using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf.WkHtmlToPdf
{
    static class Program
    {
        static int Main()
        {
            try
            {
                WritePdfToStandardOutput(ConvertStandardInputToBase64EncodedPdf());

                return 0;
            }
            catch (Exception ex)
            {
                WriteExceptionMessageToStandardError(ex);

                return -1;
            }
        }

        private static void WriteExceptionMessageToStandardError(Exception ex)
        {
            using (var standardErrort = Console.OpenStandardError())
            {
                using (var writer = new StreamWriter(standardErrort))
                {
                    writer.WriteAsBase64EncodedString(ex.Message);
                }
            }
        }

        private static void WritePdfToStandardOutput(string convertStandardInputToBase64EncodedPdf)
        {
            using (var standardOutput = Console.OpenStandardOutput())
            {
                using (var writer = new StreamWriter(standardOutput))
                {
                    writer.Write(convertStandardInputToBase64EncodedPdf);
                }
            }
        }

        private static string ConvertStandardInputToBase64EncodedPdf()
        {
            return Convert.ToBase64String(ConvertStandardInputToPdf());
        }

        private static byte[] ConvertStandardInputToPdf()
        {
            return ConvertToPdf(ConversionSource());
        }

        private static ConversionSource ConversionSource()
        {
            using (var standardInput = Console.OpenStandardInput())
            {
                using (var streamReader = new StreamReader(standardInput))
                {
                    return DeserializeBase64EncodedSource<ConversionSource>(streamReader.ReadToEnd());
                }
            }
        }

        private static T DeserializeBase64EncodedSource<T>(string base64EncodedObject)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedObject)));
        }

        private static byte[] ConvertToPdf(ConversionSource conversionSource)
        {
            using (var wkhtmlToPdfContext = WkHtmlToPdfContext.Create())
            {
                foreach (var globalSetting in conversionSource.GlobalSettings)
                    WkHtmlToX.WkHtmlToPdf.wkhtmltopdf_set_global_setting(wkhtmlToPdfContext.GlobalSettingsPointer,
                        globalSetting.Key,
                        globalSetting.Value);

                foreach (var objectSetting in conversionSource.ObjectSettings)
                    WkHtmlToX.WkHtmlToPdf.wkhtmltopdf_set_object_setting(wkhtmlToPdfContext.ObjectSettingsPointer,
                        objectSetting.Key,
                        objectSetting.Value);

                return wkhtmlToPdfContext.Render(conversionSource.Html);
            }
        }

        private static void WriteAsBase64EncodedString(this TextWriter writer, string str)
        {
            writer.Write(Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(str))));
        }
    }
}
