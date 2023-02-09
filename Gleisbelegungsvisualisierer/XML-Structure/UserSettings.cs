using Gleisbelegungsvisualisierer.Model;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer.XML_Structure
{
    [XmlRoot("UserSettings")]
    [XmlInclude(typeof(OperatingSite))]
    public class UserSettings
    {
        [XmlElement(ElementName = "OperatingSite")]
        public List<OperatingSite> OperatingSites;

        public List<string> PathsToTimetableFolder;

        public UserSettings(List<OperatingSite> operatingSites, List<string> pathsToTimetableFolder)
        {
            OperatingSites = operatingSites;
            PathsToTimetableFolder = pathsToTimetableFolder;
        }

        //For XML-Serailization
        private UserSettings() { }
    }
}
