using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace OgmoEditor
{
    static public class Ogmo
    {
        public const float VERSION = .5f;

        static public readonly MainWindow MainWindow = new MainWindow();
        static public List<Project> Projects { get; private set; }
        static private Project currentProject;

        public delegate void ProjectCallback(Project project, int projectID);
        static public event ProjectCallback OnProjectChange;
        static public event ProjectCallback OnProjectAdd;
        static public event ProjectCallback OnProjectRemove;

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            InitializeDirectories();

            Projects = new List<Project>();

            Application.Run(MainWindow);
        }

        /*
         *  Create the directory system
         */
        static public void InitializeDirectories()
        {
            if (!Directory.Exists("Projects"))
                Directory.CreateDirectory("Projects");
        }

        /*
         *  Project stuff
         */
        static public Project LoadProject(string filename)
        {
            if (!File.Exists(filename))
                throw new Exception("Loading a project file that does not exist!");

            BinaryFormatter bf = new BinaryFormatter();
            return (Project)bf.Deserialize(new FileStream(filename, FileMode.Open));
        }

        static public void AddProject(Project project)
        {
            //Add it to the list
            Projects.Add(project);

            //Call the added event
            if (OnProjectAdd != null)
                OnProjectAdd(project, Projects.Count - 1);

            CurrentProject = project;
        }

        static public void RemoveProject(Project project)
        {
            //Remove it from a list
            int id = Projects.FindIndex(e => e == project);
            Projects.Remove(project);

            //Call removed event
            if (OnProjectRemove != null)
                OnProjectRemove(project, id);

            //If it's the current project, switch to another one
            if (CurrentProject == project)
            {
                if (Projects.Count > 0)
                    CurrentProject = Projects[0];
                else
                    CurrentProject = null;
            }
        }

        static public Project CurrentProject
        {
            get { return currentProject; }
            set
            {
                if (currentProject == value)
                    return;

                Console.WriteLine("Set Current Project to: " + value.Name);

                currentProject = value;
                if (OnProjectChange != null)
                    OnProjectChange(currentProject, CurrentProjectID);
            }
        }

        static public int CurrentProjectID
        {
            get { return Projects.FindIndex(e => e == currentProject); }
            set { CurrentProject = Projects[value]; }
        }
    }
}
