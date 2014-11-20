using System;
using System.Globalization;
using System.Reflection;
using WebKitHtmlToPdf.TuesPechkin;

namespace WebKitHtmlToPdf.Settings
{
    internal static class SettingApplicator
    {
        public static void ApplySettings(IntPtr config, object settings, bool global = false)
        {
            if (settings == null)
            {
                return;
            }

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            foreach (var property in settings.GetType().GetProperties(bindingFlags))
            {
                var attributes = property.GetCustomAttributes(true);

                if (attributes.Length == 0 || !(attributes[0] is SettingAttribute))
                {
                    continue;
                }

                var attribute = attributes[0] as SettingAttribute;
                var rawValue = property.GetValue(settings, null);

                if (rawValue == null)
                {
                    continue;
                }

                var value = GetStringValue(property, rawValue);

                if (global)
                {
                    PechkinStatic.SetGlobalSetting(config, attribute.SettingName, value);
                }
                else
                {
                    PechkinStatic.SetObjectSetting(config, attribute.SettingName, value);
                }
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