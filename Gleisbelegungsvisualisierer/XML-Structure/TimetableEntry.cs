using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer.XML_Structure
{
    public class TimetableEntry
    {
        [XmlAttribute(AttributeName = "Ank")]
        public string ArrivalTime;

        [XmlAttribute(AttributeName = "Abf")]
        public string DepartureTime;

        [XmlAttribute(AttributeName = "Betrst")]
        public string OperatingSite;

        [XmlElement(ElementName = "FahrplanSignalEintrag")]
        public List<TimetableSignalEntry> TimetableSignalEntries;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("FahrplanEintrag: [Ank: '{0}', Abf: '{1}', Betrst: '{2}', [", ArrivalTime, DepartureTime, OperatingSite);
            foreach (TimetableSignalEntry timetableSignalEntry in TimetableSignalEntries)
            {
                sb.Append(timetableSignalEntry.ToString());
            }
            sb.Append("]]");
            return sb.ToString();
        }
    }
}
