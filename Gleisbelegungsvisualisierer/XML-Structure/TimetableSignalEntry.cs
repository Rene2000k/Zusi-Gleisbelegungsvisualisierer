using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer.XML_Structure
{
    public class TimetableSignalEntry
    {
        [XmlAttribute(AttributeName = "FahrplanSignal")]
        public string TimetableSignal;

        public override string ToString()
        {
            return string.Format("FahrplanSignalEintrag: [FahrplanSignal: {0}]", TimetableSignal);
        }
    }
}
