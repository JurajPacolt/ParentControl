/* Created on 19.10.2016 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl
{
    /// <summary>
    /// Data model helper class.
    /// </summary>
    public class DataModel
    {
        /// <summary>
        /// Tables names.
        /// </summary>
        public static class Tables
        {
            public const string BASE_SETTINGS = "BASE_SETTINGS";
            public const string OBSERVED_VALUES = "OBSERVED_VALUES";
            public const string RULE = "RULE";
        }

        /// <summary>
        /// All table's columns.
        /// </summary>
        public static class Columns
        {
            public static class BASE_SETTINGS
            {
                public const string SHUTDOWN_COMMAND = "SHUTDOWN_COMMAND";
                public const string CHECK_INTERVAL = "CHECK_INTERVAL";
                public const string DELAY_START = "DELAY_START";
                public const string SERVICE_URL = "SERVICE_URL";
            }

            public static class OBSERVED_VALUES
            {
                public const string ACTUAL_DATE = "ACTUAL_DATE";
                public const string DURATION = "DURATION";
            }

            public static class RULE
            {
                public const string ID = "ID";
                public const string NAME = "NAME";
                public const string DAY_OF_WEEK = "DAY_OF_WEEK";
                public const string DURATION = "DURATION";
                public const string FROM_DATE_TIME = "FROM_DATE_TIME";
                public const string TO_DATE_TIME = "TO_DATE_TIME";
                public const string ENABLED = "ENABLED";
            }
        }
    }
}
