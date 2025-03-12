using Gleisbelegungsvisualisierer.XML_Structure;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using Gleisbelegungsvisualisierer.Model;

namespace Gleisbelegungsvisualisierer
{
    public class SettingController
    {
        private const string SETTINGS_FILE_NAME = "userPreferences.xml";

        private readonly XmlSerializer settingsSerializer;

        public SettingController() {
            settingsSerializer = new XmlSerializer(typeof(UserSettings));
        }

        public void SerializeSettingsToFile(UserSettings settings)
        {
            using (StreamWriter sw = new StreamWriter(SETTINGS_FILE_NAME))
            {
                settingsSerializer.Serialize(sw, settings);
            }
        }

        public UserSettings DeserializeSettingsFromFile()
        {
            try
            {
                using (Stream reader = new FileStream(SETTINGS_FILE_NAME, FileMode.Open))
                {
                    return (UserSettings)settingsSerializer.Deserialize(reader);
                }
            }
            catch (FileNotFoundException)
            {
                return new UserSettings(new List<OperatingSite>(), new List<string>());
            }
        }
    }
}
