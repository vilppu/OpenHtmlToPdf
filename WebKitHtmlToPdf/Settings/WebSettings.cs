using System;

namespace WebKitHtmlToPdf.Settings
{
    [Serializable]
    class WebSettings
    {
        public WebSettings()
        {
            DefaultEncoding = "utf-8";
            EnableIntelligentShrinking = true;
            EnableJavascript = true;
            LoadImages = true;
            PrintBackground = true;
            PrintMediaType = true;
        }

        /// <summary>
        ///     What encoding should we guess content is using if they do not specify it properly? E.g. "utf-8"
        /// </summary>
        [Setting("web.defaultEncoding")]
        public string DefaultEncoding { get; set; }

        /// <summary>
        ///     Whether or not to enable intelligent compression of content to fit in the page
        /// </summary>
        [Setting("web.enableIntelligentShrinking")]
        public bool EnableIntelligentShrinking { get; set; }

        /// <summary>
        ///     Whether or not to enable JavaScript
        /// </summary>
        [Setting("web.enableJavascript")]
        public bool EnableJavascript { get; set; }

        /// <summary>
        ///     Whether to enable plugins (maybe like Flash? unsure)
        /// </summary>
        [Setting("web.enablePlugins")]
        public bool EnablePlugins { get; set; }

        /// <summary>
        ///     Whether or not to load images
        /// </summary>
        [Setting("web.loadImages")]
        public bool LoadImages { get; set; }

        /// <summary>
        ///     The minimum font size to use
        /// </summary>
        [Setting("web.minimumFontSize")]
        public double? MinimumFontSize { get; set; }

        /// <summary>
        ///     Whether or not to print the background on elements
        /// </summary>
        [Setting("web.background")]
        public bool? PrintBackground { get; set; }

        /// <summary>
        ///     Whether to print the content using the print media type instead of the screen media type
        /// </summary>
        [Setting("web.printMediaType")]
        public bool? PrintMediaType { get; set; }

        /// <summary>
        ///     Path to a user specified style sheet
        /// </summary>
        [Setting("web.userStyleSheet")]
        public string UserStyleSheet { get; set; }
    }
}