using System;

namespace ParentControl.ObjectModel
{
    /// <summary>
    /// Rule model object for record of the rule.
    /// </summary>
    [Serializable]
    public class Rule
    {
        /// <summary>
        /// Unit identity (key).
        /// </summary>
        private int? id;
        /// <summary>
        /// Name of the rule.
        /// </summary>
        private String name;
        /// <summary>
        /// Starting date and time of the validity. It's optional. 
        /// If not specified is validity for actual day.
        /// </summary>
        private DateTime? fromDateTime = null;
        /// <summary>
        /// End of validity. It's optional.
        /// If not specified is validity for actual day.
        /// </summary>
        private DateTime? toDateTime = null;
        /// <summary>
        /// Optional duration in minutes.
        /// </summary>
        private int? durationInMinutes = null;
        /// <summary>
        /// Day of week.
        /// </summary>
        private int? dayOfWeek = null;
        /// <summary>
        /// Enabling this rule.
        /// </summary>
        private bool enabled = true;

        public int? Id {
            get {
                return id;
            }
            set {
                this.id = value;
            }
        }

        public string Name {
            get {
                return name;
            }
            set {
                name = value;
            }
        }

        public DateTime? FromDateTime {
            get {
                return fromDateTime;
            }
            set {
                fromDateTime = value;
            }
        }

        public DateTime? ToDateTime {
            get {
                return toDateTime;
            }
            set {
                toDateTime = value;
            }
        }

        public int? DurationInMinutes {
            get {
                return durationInMinutes;
            }
            set {
                durationInMinutes = value;
            }
        }

        public int? DayOfWeek {
            get {
                return dayOfWeek;
            }
            set {
                dayOfWeek = value;
            }
        }

        public bool Enabled {
            get {
                return enabled;
            }
            set {
                enabled = value;
            }
        }
    }

}
