/* Created on 27.10.2016 */
using System;

namespace ParentControlManager.Model
{
    /// <summary>
    /// Object model class of the connection.
    /// </summary>
    [Serializable]
    public class Connection
    {
        private int? id;
        private String name;
        private String hostname;
        private int? port;

        public int? Id
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }

        public String Hostname
        {
            get
            {
                return hostname;
            }
            set
            {
                this.hostname = value;
            }
        }

        public int? Port
        {
            get
            {
                return port;
            }
            set
            {
                this.port = value;
            }
        }
    }
}
