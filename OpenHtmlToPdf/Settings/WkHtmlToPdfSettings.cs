using System;
using System.Globalization;
using System.Reflection;
using OpenHtmlToPdf.Native;

namespace OpenHtmlToPdf.Settings
{
    static class WkHtmlToPdfSettings
    {
        private const BindingFlags InstanceMember = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        public static void ApplySettings(IntPtr config, object settings, bool global = false)
        {
            foreach (var property in settings.GetType().GetProperties(InstanceMember))
            {
                ApplySetting(config, settings, global, property);
            }
        }

        private static void ApplySetting(IntPtr config, object settings, bool global,
            PropertyInfo property)
        {
            var attributes = property.GetCustomAttributes(true);

            if (attributes.Length == 0 || !(attributes[0] is SettingAttribute))
            {
                return;
            }

            var attribute = attributes[0] as SettingAttribute;
            var rawValue = property.GetValue(settings, null);

            if (rawValue == null)
            {
                return;
            }

            var value = GetStringValue(property, rawValue);

            if (global)
            {
                 Wkhtmltox.wkhtmltopdf_set_global_setting(config, attribute.SettingName, value);
            }
            else
            {
                Wkhtmltox.wkhtmltopdf_set_object_setting(config, attribute.SettingName, value);
            }
        }

        private static string GetStringValue(PropertyInfo property, object value)
        {
            var type = property.PropertyType;

            if (type == typeof(double?) || type == typeof(double))
            {
                return ((double?)value).Value.ToString("0.##", CultureInfo.InvariantCulture);
            }
            if (type == typeof(bool?) || type == typeof(bool))
            {
                return ((bool?)value).Value ? "true" : "false";
            }
            return value.ToString();
        }
    }
}