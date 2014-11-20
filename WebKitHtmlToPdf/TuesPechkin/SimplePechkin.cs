using System;
using WebKitHtmlToPdf.EventHandlers;
using WebKitHtmlToPdf.Settings;
using WebKitHtmlToPdf.TuesPechkin.EventHandlers;
using WebKitHtmlToPdf.Util;

namespace WebKitHtmlToPdf.TuesPechkin
{
    /// <summary>
    ///     Covers the necessary converter functionality, for internal
    ///     use behind a remote proxy implementing the same interface.
    /// </summary>
    [Serializable]
    class SimplePechkin : MarshalByRefObject, IPechkin
    {
        private readonly StringCallback _onErrorDelegate;
        private readonly IntCallback _onFinishedDelegate;
        private readonly VoidCallback _onPhaseChangedDelegate;
        private readonly IntCallback _onProgressChangedDelegate;
        private readonly StringCallback _onWarningDelegate;
        private IntPtr _converter;

        /// <summary>
        ///     Constructs HTML to PDF converter instance from <code>GlobalSettings</code>.
        /// </summary>
        /// <param name="config">global configuration object</param>
        public SimplePechkin()
        {
            _onErrorDelegate = OnError;
            _onFinishedDelegate = OnFinished;
            _onPhaseChangedDelegate = OnPhaseChanged;
            _onProgressChangedDelegate = OnProgressChanged;
            _onWarningDelegate = OnWarning;
        }

        /// <summary>
        ///     This event happens every time the conversion starts
        /// </summary>
        public event BeginEventHandler Begin;

        /// <summary>
        ///     Event handler is called whenever error happens during conversion process.
        ///     Error typically means that conversion will be terminated.
        /// </summary>
        public event ErrorEventHandler Error;

        /// <summary>
        ///     This event handler is fired when conversion is finished.
        /// </summary>
        public event FinishEventHandler Finished;

        /// <summary>
        ///     This event handler signals phase change of the conversion process.
        /// </summary>
        public event PhaseChangedEventHandler PhaseChanged;

        /// <summary>
        ///     This event handler signals progress change of the conversion process.
        ///     Number of percents is included in text description.
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged;

        /// <summary>
        ///     This event handler is called whenever warning happens during conversion process.
        ///     You can also see javascript errors and warnings if you enable <code>SetJavascriptDebugMode</code> in
        ///     <code>ObjectSettings</code>
        /// </summary>
        public event WarningEventHandler Warning;

        /// <summary>
        ///     Current phase number for the converter.
        ///     We recommend to use this property only inside the event handlers.
        /// </summary>
        public int CurrentPhase
        {
            get { return PechkinStatic.GetPhaseNumber(_converter); }
        }

        /// <summary>
        ///     Error code returned by server when converter tried to request the page or the resource. Should be available after
        ///     failed conversion attempt.
        /// </summary>
        public int HttpErrorCode
        {
            get { return PechkinStatic.GetHttpErrorCode(_converter); }
        }

        /// <summary>
        ///     Phase count for the current conversion process.
        ///     We recommend to use this property only inside the event handlers.
        /// </summary>
        public int PhaseCount
        {
            get { return PechkinStatic.GetPhaseCount(_converter); }
        }

        /// <summary>
        ///     Current phase string description for the converter.
        ///     We recommend to use this property only inside the event handlers.
        /// </summary>
        public string PhaseDescription
        {
            get { return PechkinStatic.GetPhaseDescription(_converter, CurrentPhase); }
        }

        /// <summary>
        ///     Current progress string description. It includes percent count, btw.
        ///     We recommend to use this property only inside the event handlers.
        /// </summary>
        public string ProgressString
        {
            get { return PechkinStatic.GetProgressDescription(_converter); }
        }

        /// <summary>
        ///     Runs conversion process.
        ///     Allows to convert both external HTML resource and HTML string.
        ///     Takes html source as a byte array for when you don't know the encoding.
        /// </summary>
        /// <param name="doc">document parameters</param>
        /// <param name="html">document body, ignored if <code>ObjectSettings.SetPageUri</code> is set</param>
        /// <returns>PDF document body</returns>
        public byte[] Convert(HtmlToPdfDocument document)
        {
            document.ApplyToConverter(out _converter);

            PechkinStatic.SetErrorCallback(_converter, _onErrorDelegate);
            PechkinStatic.SetWarningCallback(_converter, _onWarningDelegate);
            PechkinStatic.SetPhaseChangedCallback(_converter, _onPhaseChangedDelegate);
            PechkinStatic.SetProgressChangedCallback(_converter, _onProgressChangedDelegate);
            PechkinStatic.SetFinishedCallback(_converter, _onFinishedDelegate);


            // run OnBegin
            OnBegin(_converter);

            // run conversion process
            if (!PechkinStatic.PerformConversion(_converter))
            {
                return null;
            }

            // get output
            byte[] result = PechkinStatic.GetConverterResult(_converter);

            PechkinStatic.DestroyConverter(_converter);

            return result;
        }

        /// <summary>
        ///     Converts external HTML resource into PDF.
        /// </summary>
        /// <param name="doc">document parameters, <code>ObjectSettings.SetPageUri</code> should be set</param>
        /// <returns>PDF document body</returns>
        public byte[] Convert(ObjectSettings settings)
        {
            var doc = new HtmlToPdfDocument();
            doc.Objects.Add(settings);
            return Convert(doc);
        }

        /// <summary>
        ///     Converts HTML string to PDF with default settings.
        /// </summary>
        /// <param name="html">HTML string</param>
        /// <returns>PDF document body</returns>
        public byte[] Convert(string html)
        {
            return Convert(new ObjectSettings {HtmlText = html});
        }

        /// <summary>
        ///     Converts HTML string to PDF with default settings.
        ///     Takes html source as a byte array for when you don't know the encoding.
        /// </summary>
        /// <param name="html">HTML string</param>
        /// <returns>PDF document body</returns>
        public byte[] Convert(byte[] html)
        {
            return Convert(new ObjectSettings {RawData = html});
        }

        /// <summary>
        ///     Converts HTML page at specified URL to PDF with default settings.
        /// </summary>
        /// <param name="url">url of page, can be either http/https or file link</param>
        /// <returns>PDF document body</returns>
        public byte[] Convert(Uri url)
        {
            return Convert(new ObjectSettings {PageUrl = url.AbsoluteUri});
        }

        protected virtual void OnBegin(IntPtr converter)
        {
            int expectedPhaseCount = PechkinStatic.GetPhaseCount(converter);

            BeginEventHandler handler = Begin;
            try
            {
                if (handler != null)
                {
                    handler(this, expectedPhaseCount);
                }
            }
            catch (Exception e)
            {
            }
        }

        protected virtual void OnError(IntPtr converter, string errorText)
        {
            ErrorEventHandler handler = Error;
            try
            {
                if (handler != null)
                {
                    handler(this, errorText);
                }
            }
            catch (Exception e)
            {
            }
        }

        protected virtual void OnFinished(IntPtr converter, int success)
        {
            FinishEventHandler handler = Finished;
            try
            {
                if (handler != null)
                {
                    handler(this, success != 0);
                }
            }
            catch (Exception e)
            {
            }
        }

        protected virtual void OnPhaseChanged(IntPtr converter)
        {
            int phaseNumber = PechkinStatic.GetPhaseNumber(converter);
            string phaseDescription = PechkinStatic.GetPhaseDescription(converter, phaseNumber);

            PhaseChangedEventHandler handler = PhaseChanged;
            try
            {
                if (handler != null)
                {
                    handler(this, phaseNumber, phaseDescription);
                }
            }
            catch (Exception e)
            {
            }
        }

        protected virtual void OnProgressChanged(IntPtr converter, int progress)
        {
            string progressDescription = PechkinStatic.GetProgressDescription(converter);


            ProgressChangedEventHandler handler = ProgressChanged;
            try
            {
                if (handler != null)
                {
                    handler(this, progress, progressDescription);
                }
            }
            catch (Exception e)
            {
            }
        }

        protected virtual void OnWarning(IntPtr converter, string warningText)
        {
            WarningEventHandler handler = Warning;
            try
            {
                if (handler != null)
                {
                    handler(this, warningText);
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}