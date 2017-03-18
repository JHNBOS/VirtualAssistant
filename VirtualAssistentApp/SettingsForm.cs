using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualAssistentApp
{
    public partial class SettingsForm : Form
    {
        string filename = @"C:\Github\VirtualAssistant\VirtualAssistentApp\settings.txt";

        public SettingsForm()
        {
            InitializeComponent();

            if (File.ReadAllLines(filename).Length > 0)
            {
                readSettings();
            }
        }

        private void readSettings()
        {
            var allLines = File.ReadAllLines(filename);

            if (allLines.Length > 0)
            {
                ArrayList settingsList = new ArrayList();

                foreach (var line in allLines)
                {
                    var currentLine = line.Split(':')[1];
                    settingsList.Add(currentLine);
                }

                nameBox.Text = settingsList[0].ToString();
                cityBox.Text = settingsList[1].ToString();
                countryBox.Text = settingsList[2].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = nameBox.Text.ToString();
            string city = cityBox.Text.ToString();
            string country = countryBox.Text.ToString();

            File.WriteAllText(filename, "");

            string[] settings = new string[3];
            settings[0] = "Name:" + name;
            settings[1] = "City:" + city;
            settings[2] = "Country:" + country;

            File.WriteAllLines(filename, settings);

            this.Dispose();
        }

    }
}
