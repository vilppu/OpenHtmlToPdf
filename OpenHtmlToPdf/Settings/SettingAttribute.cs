using System;

namespace OpenHtmlToPdf.Settings
{
    [AttributeUsage(AttributeTargets.Property)]
    class SettingAttribute : Attribute
    {
        public SettingAttribute(string settingName)
        {
            SettingName = settingName;
        }

        public string SettingName { get; set; }
    }
}