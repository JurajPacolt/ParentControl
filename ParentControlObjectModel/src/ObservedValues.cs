/* Created on 1.10.2016 */
using System;

namespace ParentControl.ObjectModel
{

    /// <summary>
    /// Model class of the observed values.
    /// </summary>
    public class ObservedValues
    {
        private DateTime? actualDate;
        private long? duration;

        public DateTime? ActualDate {
            get {
                return actualDate;
            }
            set {
                this.actualDate = value;
            }
        }

        public long? Duration {
            get {
                return duration;
            }
            set {
                this.duration = value;
            }
        }
    }

}
