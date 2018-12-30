﻿using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace VirtualAssistentApp
{
    public partial class SettingsForm : Form
    {
        public string SettingsPath { get; set; }

        public SettingsForm()
        {
            InitializeComponent();

            this.SettingsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\settings.txt");
            if (File.ReadAllLines(SettingsPath).Length > 0)
            {
                ReadSettings();
            }
        }

        private void ReadSettings()
        {
            var settings = File.ReadAllLines(SettingsPath);

            if (settings.Length > 0)
            {
                ArrayList settingsList = new ArrayList();

                foreach (var line in settings)
                {
                    var currentLine = line.Split(':')[1];
                    settingsList.Add(currentLine);
                }

                nameBox.Text = settingsList[0].ToString();
                cityBox.Text = settingsList[1].ToString();
                countryBox.Text = settingsList[2].ToString();

                string gender = settingsList[3].ToString();

                if (gender == "Male")
                {
                    genderBox.SelectedIndex = 0;
                }
                else if (gender == "Female")
                {
                    genderBox.SelectedIndex = 1;
                }
                else
                {
                    genderBox.SelectedIndex = 1;
                }

                assistentBox.Text = settingsList[4].ToString();

                bool awake = bool.Parse(settingsList[5].ToString());

                if (awake == true)
                {
                    awakeCheckBox.Checked = true;
                }
                else
                {
                    awakeCheckBox.Checked = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = nameBox.Text.ToString();
            string city = cityBox.Text.ToString();
            string country = countryBox.Text.ToString();
            string gender = genderBox.SelectedItem.ToString();
            string botName = assistentBox.Text.ToString();
            string useAwake = awakeCheckBox.Checked.ToString();

            File.WriteAllText(this.SettingsPath, "");

            string[] settings = new string[6];
            settings[0] = "Name:" + name;
            settings[1] = "City:" + city;
            settings[2] = "Country:" + country;
            settings[3] = "Gender:" + gender;
            settings[4] = "BotName:" + botName;
            settings[5] = "UseAwake:" + useAwake;

            File.WriteAllLines(this.SettingsPath, settings);

            string[] commands = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\commands.txt"));

            foreach (var line in commands)
            {
                if (line != botName)
                {
                    using (StreamWriter w = File.AppendText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\commands.txt")))
                    {
                        w.WriteLine(botName);
                    }
                }
            }

            MainForm f = new MainForm();
            f.Show();
            this.Dispose();
            f.Activate();
        }

    }
}
