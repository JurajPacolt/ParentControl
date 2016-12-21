/* Created on 27.9.2016 */
using System;

namespace ParentControl.ObjectModel
{
    public class DayOfWeek
    {
        private int id;
        private String name;

        public DayOfWeek() {
        }

        public DayOfWeek(String name, int id) : this() {
            this.name = name;
            this.id = id;
        }

        public int Id {
            get {
                return id;
            }
            set {
                this.id = value;
            }
        }

        public String Name {
            get {
                return name;
            }
            set {
                this.name = value;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) {
                return false;
            }
            DayOfWeek dow = (DayOfWeek)obj;
            if (dow.Id == Id && dow.Name != null && Name != null && dow.Name == Name) {
                return true;
            }
            if (dow.Name == null && Name == null) {
                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = 13;
                hashCode = (hashCode * 321) ^ Id;
                hashCode = !string.IsNullOrEmpty(Name) ? Name.GetHashCode() : 0;
                return hashCode;
            }
        }
    }

}
