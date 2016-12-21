using System;

namespace ParentControl.ObjectModel
{
    /// <summary>
    /// Structure of the base settings.
    /// </summary>
    [Serializable]
    public class BaseSettings
    {
        /// <summary>
        /// Shutdown command is executed if least one of the rules is true.
        /// </summary>
        private String shutdownCommand;
        /// <summary>
        /// Check interval at seconds.
        /// </summary>
        private int? checkInterval;
        /// <summary>
        /// Url for REST service for remote controling.
        /// </summary>
        private String urlService;
        /// <summary>
        /// Delay start of the job.
        /// </summary>
        private int? delayStart;

        public string ShutdownCommand
        {
            get
            {
                return shutdownCommand;
            }
            set
            {
                shutdownCommand = value;
            }
        }

        public int? CheckInterval
        {
            get
            {
                return checkInterval;
            }
            set
            {
                checkInterval = value;
            }
        }

        public String UrlService
        {
            get
            {
                return urlService;
            }
            set
            {
                this.urlService = value;
            }
        }

        public int? DelayStart
        {
            get
            {
                return delayStart;
            }
            set
            {
                this.delayStart = value;
            }
        }
    }

}
