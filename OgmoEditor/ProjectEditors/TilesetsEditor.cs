using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OgmoEditor.Definitions;
using System.IO;
using System.Diagnostics;

namespace OgmoEditor.ProjectEditors
{
    public partial class TilesetsEditor : UserControl, IProjectChanger
    {
        private const string DEFAULT_NAME = "NewTileset";

        private List<Tileset> tilesets;
        private string directory;

        public TilesetsEditor()
        {
            InitializeComponent();
        }

        public string ErrorCheck()
        {
            return "";
        }

        public void LoadFromProject(Project project)
        {
            tilesets = project.Tilesets;
            foreach (var t in tilesets)
                listBox.Items.Add(t.Name);

            directory = project.SavedDirectory;
            Debug.WriteLine(directory);
        }

        public void ApplyToProject(Project project)
        {

        }

        private void setControlsFromTileset(Tileset t)
        {
            previewBox.Enabled = true;
            nameTextBox.Enabled = true;
            imageFileTextBox.Enabled = true;
            imageFileButton.Enabled = true;
            imageFileWarningLabel.Enabled = true;
            tileSizeXTextBox.Enabled = true;
            tileSizeYTextBox.Enabled = true;
            tileSpacingTextBox.Enabled = true;

            nameTextBox.Text = t.Name;
            imageFileTextBox.Text = t.Path;
            tileSizeXTextBox.Text = t.TileSize.Width.ToString();
            tileSizeYTextBox.Text = t.TileSize.Height.ToString();
            tileSpacingTextBox.Text = t.TileSep.ToString();

            imageFileWarningLabel.Visible = !checkImageFile();
        }

        private void disableControls()
        {
            previewBox.Enabled = false;
            nameTextBox.Enabled = false;
            imageFileTextBox.Enabled = false;
            imageFileButton.Enabled = false;
            imageFileWarningLabel.Enabled = false;
            tileSizeXTextBox.Enabled = false;
            tileSizeYTextBox.Enabled = false;
            tileSpacingTextBox.Enabled = false;
        }

        private string getNewName()
        {
            int i = 0;
            string name;

            do
            {
                name = DEFAULT_NAME + i.ToString();
                i++;
            }
            while (tilesets.Find(t => t.Name == name) != null);

            return name;
        }

        private bool checkImageFile()
        {
            return File.Exists(Util.GetPathAbsolute(imageFileTextBox.Text, directory));
        }

        /*
         *  Events
         */
        private void addButton_Click(object sender, EventArgs e)
        {
            Tileset t = new Tileset();
            t.Name = getNewName();
            tilesets.Add(t);
            listBox.SelectedIndex = listBox.Items.Add(t.Name);
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1)
                disableControls();
            else
                setControlsFromTileset(tilesets[listBox.SelectedIndex]);
        }

        private void nameTextBox_Validated(object sender, EventArgs e)
        {
            tilesets[listBox.SelectedIndex].Name = nameTextBox.Text;
        }

        private void tileSizeXTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref tilesets[listBox.SelectedIndex].TileSize, tileSizeXTextBox, tileSizeYTextBox);
        }

        private void tileSizeYTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref tilesets[listBox.SelectedIndex].TileSize, tileSizeXTextBox, tileSizeYTextBox);
        }

        private void tileSpacingTextBox_Validated(object sender, EventArgs e)
        {
            ProjParse.Parse(ref tilesets[listBox.SelectedIndex].TileSep, tileSpacingTextBox);
        }

        private void imageFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = Tileset.FILE_FILTER;
            dialog.CheckFileExists = true;

            if (checkImageFile())
                dialog.InitialDirectory = Path.GetFullPath(Util.GetPathAbsolute(imageFileTextBox.Text, directory));
            else
                dialog.InitialDirectory = directory;

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            imageFileTextBox.Text = Util.GetFilePathRelativeTo(dialog.FileName, directory);
            imageFileWarningLabel.Visible = !checkImageFile();

            tilesets[listBox.SelectedIndex].Path = imageFileTextBox.Text;
        }
    }
}
