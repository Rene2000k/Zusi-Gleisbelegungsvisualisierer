using System.Collections.Generic;
using System.Xml.Serialization;

namespace Gleisbelegungsvisualisierer.XML_Structure
{
    [XmlRoot("UserSettings")]
    [XmlInclude(typeof(OperatingSite))]
    public class UserSettings
    {
        public List<OperatingSite> OperatingSites;

        public string PathToTimetableFolder;

        public UserSettings(List<OperatingSite> operatingSites, string pathToTimetableFolder)
        {
            OperatingSites = operatingSites;
            PathToTimetableFolder = pathToTimetableFolder;
        }

        //For XML-Serailization
        private UserSettings() { }
    }
}
