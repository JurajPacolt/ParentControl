using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.ObjectModel
{
    /// <summary>
    /// Model object for exception.
    /// </summary>
    [Serializable]
    public class Exception
    {
        public const int ERROR = 1;
        public const int WARNING = 2;
        public const int INFO = 3;

        private int typeException = ERROR;
        private String message;

        public int TypeException {
            get {
                return typeException;
            }
            set {
                this.typeException = value;
            }
        }

        public String Message {
            get {
                return message;
            }
            set {
                this.message = value;
            }
        }

    }

}
