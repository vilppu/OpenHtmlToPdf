using System;

namespace OpenHtmlToPdf.Settings
{
    [Serializable]
    class LoadSettings
    {
        public enum ContentErrorHandling
        {
            Abort,
            Skip,
            Ignore
        }

        public LoadSettings()
        {
            ErrorHandling = ContentErrorHandling.Ignore;
            StopSlowScript = true;
        }

        [Setting("load.blockLocalFileAccess")]
        public bool BlockLocalFileAccess { get; set; }

        [Setting("load.debugJavascript")]
        public bool DebugJavascript { get; set; }

        [Setting("load.loadErrorHandling")]
        public ContentErrorHandling ErrorHandling { get; set; }

        [Setting("load.password")]
        public string Password { get; set; }

        [Setting("load.proxy")]
        public string Proxy { get; set; }

        [Setting("load.jsdelay")]
        public int? RenderDelay { get; set; }

        [Setting("load.stopSlowScript")]
        public bool StopSlowScript { get; set; }

        [Setting("load.username")]
        public string Username { get; set; }

        [Setting("load.zoomFactor")]
        public double? ZoomFactor { get; set; }
    }
}