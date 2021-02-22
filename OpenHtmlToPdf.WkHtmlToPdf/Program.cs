using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using OpenHtmlToPdf.WkHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf.WkHtmlToPdf
{
    static class Program
    {
        internal static string TempDir { private set; get; }

        static int Main(string[] args)
        {
            try
            {
                if (args.Length > 0)
                    ParseArguments(args);

                ConvertStandardInputToPdf();

                return 0;
            }
            catch (Exception ex)
            {
                WriteExceptionMessageToStandardError(ex);

                return -1;
            }
        }

        /// <summary>
        /// Parses the programs arguments.
        /// </summary>
        private static void ParseArguments(string[] args)
        {
            Regex argument = new Regex(@"^-([^=]+)=(.+)$");

            for (int i = 0; i < args.Length; i++)
            {
                Match arg = argument.Match(args[i]);
                if (arg.Success)
                {
                    string
                        name = arg.Groups[1].Value,
                        value = arg.Groups[2].Value;

                    switch (name.ToUpper())
                    {
                        case "TEMPDIR":
                            TempDir = value;
                            break;
                    }
                }
                else
                {
                    throw new Exception($"Failed to parse argument \"{args[i]}\"");
                }
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

        private static void ConvertStandardInputToPdf()
        {
            ConvertToPdf(ConversionSource());
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
            using (var stream = new MemoryStream(Convert.FromBase64String(base64EncodedObject)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(stream);
            }
        }

        private static void ConvertToPdf(ConversionSource conversionSource)
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

                wkhtmlToPdfContext.Convert(conversionSource.Html);
            }
        }

        private static void WriteAsBase64EncodedString(this TextWriter writer, string str)
        {
            writer.Write(Convert.ToBase64String(Encoding.UTF8.GetBytes(str)));
        }
    }
}
