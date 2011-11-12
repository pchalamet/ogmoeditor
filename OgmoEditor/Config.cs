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
        private const int RECENT_PROJECT_LIMIT = 10;

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
        public List<RecentProject> RecentProjects;

        private Config()
        {
            RecentProjects = new List<RecentProject>();
        }

        public void ClearRecentProjects()
        {
            RecentProjects.Clear();
        }

        public void CheckRecentProjects()
        {
            for (int i = 0; i < RecentProjects.Count; i++)
            {
                if (!File.Exists(RecentProjects[i].Path))
                {
                    RecentProjects.RemoveAt(i);
                    i--;
                }
            }
        }

        public void UpdateRecentProjects(Project project)
        {
            for (int i = 0; i < RecentProjects.Count; i++)
            {
                if (RecentProjects[i].Path == project.Filename)
                {
                    RecentProjects.RemoveAt(i);
                    break;
                }
            }

            RecentProjects.Insert(0, new RecentProject(project.Name, project.Filename));
            if (RecentProjects.Count > RECENT_PROJECT_LIMIT)
                RecentProjects.RemoveAt(RECENT_PROJECT_LIMIT);
        }

        public struct RecentProject
        {
            public string Name;
            public string Path;

            public RecentProject(string name, string path)
            {
                Name = name;
                Path = path;
            }
        }
    }
}
