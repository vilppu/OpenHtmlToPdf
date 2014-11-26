using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenHtmlToPdf.WkHtmlToPdf;

namespace OpenHtmlToPdf
{
    static class ConverterExeutable
    {
        public static string PathToConverterExecutable()
        {
            var executable = typeof(ConversionSource).Assembly.GetName().Name + ".exe";
            var pathToExecutable = PathCandidates(executable).FirstOrDefault(File.Exists);
            var throwFileNotFoundException =(Func<string>) (() => { throw new FileNotFoundException(executable); });

            return pathToExecutable ?? throwFileNotFoundException();
        }

        private static IEnumerable<string> PathCandidates(string executable)
        {
            return DirectoryCandidates().Select(d => Path.Combine(d, executable));
        }

        private static IEnumerable<string> DirectoryCandidates()
        {
            return new[]
            {
                GetDirectoryName(typeof (ConversionSource).Assembly.CodeBase),
                typeof (ConversionSource).Assembly.Location
            };
        }

        private static string GetDirectoryName(string codeBase)
        {
            return Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(codeBase).Path));
        }
    }
}