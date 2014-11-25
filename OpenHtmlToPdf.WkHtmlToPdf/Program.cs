using System;
using System.IO;
using Newtonsoft.Json;
using OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf.WkHtmlToPdf
{
    class Program
    {
        static void Main()
        {
            try
            {
                using (var standardOutput = Console.OpenStandardOutput())
                {
                    using (var writer = new StreamWriter(standardOutput))
                    {
                        writer.Write(ConvertStandardInputToBase64EncodedPdf());
                    }
                }
            }
            catch (Exception ex)
            {
                using (var standardErrort = Console.OpenStandardError())
                {
                    using (var writer = new StreamWriter(standardErrort))
                    {
                        writer.Write(JsonConvert.SerializeObject(ex));
                    }
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
                    return JsonConvert.DeserializeObject<ConversionSource>(streamReader.ReadToEnd());
                }
            }
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
    }
}
