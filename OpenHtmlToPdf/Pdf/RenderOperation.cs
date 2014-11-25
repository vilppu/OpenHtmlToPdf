using System;
using System.Collections.Generic;
using OpenHtmlToPdf.WkHtmlToX;

namespace OpenHtmlToPdf.Pdf
{
    public sealed class RenderOperation : MarshalByRefObject
    {
        public byte[] Render(
            string html,
            IDictionary<string, string> globalSettings, 
            IDictionary<string, string> objectSettings)
        {
            using (var wkhtmlToPdfContext = WkHtmlToPdfContext.Create())
            {
                foreach (var globalSetting in globalSettings)
                    WkHtmlToPdf.wkhtmltopdf_set_global_setting(wkhtmlToPdfContext.GlobalSettingsPointer,
                        globalSetting.Key,
                        globalSetting.Value);

                foreach (var objectSetting in objectSettings)
                    WkHtmlToPdf.wkhtmltopdf_set_object_setting(wkhtmlToPdfContext.ObjectSettingsPointer,
                        objectSetting.Key,
                        objectSetting.Value);

                return wkhtmlToPdfContext.Render(html);
            }
        }
    }
}