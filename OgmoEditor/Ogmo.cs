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

        [STAThread]
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
        static public void LoadProject()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Ogmo Editor Project Files|*.oep";

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            BinaryFormatter bf = new BinaryFormatter();
            Stream s = dialog.OpenFile();
            Project project = (Project)bf.Deserialize(s);
            s.Close();

            AddProject(project);
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
                //Do not change if its already selected
                if (currentProject == value)
                    return;

                //Change the project
                currentProject = value;

                //Call the change event
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
