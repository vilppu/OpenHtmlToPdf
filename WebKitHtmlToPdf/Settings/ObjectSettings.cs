using System;
using System.Text;
using WebKitHtmlToPdf.TuesPechkin;

namespace WebKitHtmlToPdf.Settings
{
    [Serializable]
    class ObjectSettings
    {
        private byte[] _data;

        private FooterSettings _footer = new FooterSettings();

        private HeaderSettings _header = new HeaderSettings();

        private LoadSettings _load = new LoadSettings();

        private WebSettings _web = new WebSettings();

        public ObjectSettings()
        {
            IncludeInOutline = true;
            CountPages = true;
            ProduceExternalLinks = true;
            ProduceForms = true;
            ProduceLocalLinks = true;
        }

        [Setting("includeInOutline")]
        public bool? IncludeInOutline { get; set; }

        [Setting("pagesCount")]
        public bool? CountPages { get; set; }

        [Setting("page")]
        public string PageUrl { get; set; }

        [Setting("produceForms")]
        public bool? ProduceForms { get; set; }

        [Setting("useExternalLinks")]
        public bool? ProduceExternalLinks { get; set; }

        [Setting("useLocalLinks")]
        public bool? ProduceLocalLinks { get; set; }

        public FooterSettings FooterSettings
        {
            get { return _footer; }
            set
            {
                _footer = value;
            }
        }

        public HeaderSettings HeaderSettings
        {
            get { return _header; }
            set
            {
                _header = value;
            }
        }

        public string HtmlText
        {
            get { return Encoding.UTF8.GetString(_data); }
            set { _data = Encoding.UTF8.GetBytes(value); }
        }

        public LoadSettings LoadSettings
        {
            get { return _load; }
            set
            {
                _load = value;
            }
        }

        public byte[] RawData
        {
            get { return _data; }
            set { _data = value; }
        }

        public WebSettings WebSettings
        {
            get { return _web; }
            set
            {
                _web = value;
            }
        }

        internal void ApplyToConverter(IntPtr converter)
        {
            IntPtr config = PechkinStatic.CreateObjectSettings();

            SettingApplicator.ApplySettings(config, this);
            SettingApplicator.ApplySettings(config, HeaderSettings);
            SettingApplicator.ApplySettings(config, FooterSettings);
            SettingApplicator.ApplySettings(config, WebSettings);
            SettingApplicator.ApplySettings(config, LoadSettings);

            PechkinStatic.AddObject(converter, config, _data);
        }
    }
}