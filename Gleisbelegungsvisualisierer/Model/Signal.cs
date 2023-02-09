using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer.Model
{
    public class Signal
    {
        // For XML-Serialization
        private Signal() { }

        public Signal(string name) { 
            Name = name;
        }

        [XmlAttribute]
        public string Name;

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            Signal signal = (Signal)obj;
            if (obj == null) return false;
            return signal.Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
