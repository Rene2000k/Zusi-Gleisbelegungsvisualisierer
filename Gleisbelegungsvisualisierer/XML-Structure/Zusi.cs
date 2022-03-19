using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer.XML_Structure
{
    public class Zusi
    {
        [XmlElement(ElementName = "Zug")]
        public XMLTrain Train;

        public override string ToString()
        {
            return Train.ToString();
        }
    }
}