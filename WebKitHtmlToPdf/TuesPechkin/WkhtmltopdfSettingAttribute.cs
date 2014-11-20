using System;

namespace WebKitHtmlToPdf.TuesPechkin
{
    [AttributeUsage(AttributeTargets.Property)]
    class WkhtmltopdfSettingAttribute : Attribute
    {
        public WkhtmltopdfSettingAttribute(string settingName)
        {
            this.SettingName = settingName;
        }

        public string SettingName { get; set; }
    }
}