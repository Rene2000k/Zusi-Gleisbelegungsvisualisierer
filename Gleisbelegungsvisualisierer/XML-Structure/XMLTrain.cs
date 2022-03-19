using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer.XML_Structure
{
    public class XMLTrain
    {
        [XmlAttribute(AttributeName = "Gattung")]
        public string TrainType;

        [XmlAttribute(AttributeName = "Nummer")]
        public string TrainNumber;

        [XmlAttribute(AttributeName = "Zuglauf")]
        public string TrainRun;

        [XmlElement(ElementName = "FahrplanEintrag")]
        public List<TimetableEntry> TimetableEntries;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Zug: [Gattung: '{0}', Nummer: '{1}', Zuglauf: '{2}', [", TrainType, TrainNumber, TrainRun);
            foreach (TimetableEntry timetableEntry in TimetableEntries)
            {
                sb.Append(timetableEntry.ToString());
            }
            sb.Append("]]");
            return sb.ToString();
        }
    }
}
