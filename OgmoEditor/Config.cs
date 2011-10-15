using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace OgmoEditor
{
    [XmlRoot("Config")]
    public class Config
    {
        private const string CONFIG_NAME = "config.xml";

        static public Config ConfigFile;

        static public void Save()
        {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            Stream stream = new FileStream(Path.Combine(Ogmo.ProgramDirectory, CONFIG_NAME), FileMode.Create);
            xs.Serialize(stream, ConfigFile);
            stream.Close();
        }

        static public void Load()
        {
            if (File.Exists(Path.Combine(Ogmo.ProgramDirectory, CONFIG_NAME)))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Config));
                Stream stream = new FileStream(Path.Combine(Ogmo.ProgramDirectory, CONFIG_NAME), FileMode.Open);
                ConfigFile = (Config)xs.Deserialize(stream);
                stream.Close();
            }
            else
            {
                ConfigFile = new Config();
            }
        }

        /*
         *  The actual config file
         */
        private Config()
        {
            
        }
    }
}
