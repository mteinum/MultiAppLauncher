using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace MultiAppLauncher
{
    public class SettingsDocument
    {
        [XmlAttribute("softstart")]
        public int SoftStart;

        public List<FileSettings> FileNames;

        public void Save(string fileName)
        {
            var serializer = new XmlSerializer(typeof (SettingsDocument));
            
            using (var stream = File.Open(fileName, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }

        public static SettingsDocument Load(string fileName)
        {
            var serializer = new XmlSerializer(typeof(SettingsDocument));

            using (var stream = File.OpenRead(fileName))
            {
                return (SettingsDocument) serializer.Deserialize(stream);
            }
        }
    }
}