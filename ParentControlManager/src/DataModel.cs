/* Created on 27.10.2016 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControlManager
{
    /// <summary>
    /// Data model constants for Manager.
    /// </summary>
    public class DataModel
    {
        /// <summary>
        /// Table's constants of the data model.
        /// </summary>
        public static class Tables
        {
            public const string CONNECTION = "CONNECTION";
        }

        /// <summary>
        /// Column's constants of the data model.
        /// </summary>
        public static class Columns
        {
            /// <summary>
            /// Table CONNECTION for connections to remote stations.
            /// </summary>
            public static class CONNECTION
            {
                public const string ID = "ID";
                public const string NAME = "NAME";
                public const string HOSTNAME = "HOSTNAME";
                public const string PORT = "PORT";
            }
        }
    }
}
