using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Speech.Recognition;
using System.Windows.Forms;

using VirtualAssistentApp.Helper;

namespace VirtualAssistentApp
{
    public partial class MainForm : Form
    {
        #region Properties

        private Assistant assistent;
        private HandleProcess handler = new HandleProcess();

        #endregion

        public MainForm()
        {
            InitializeComponent();

            // Init properties
            this.assistent = new Assistant();
            explainLabel.Text = "Start By Saying '" + this.assistent.BotName + "'";
            
            // Focus on program
            title.Focus();

            #region Event Handlers Setup

            weatherBox.MouseHover += WeatherBox_MouseHover;
            internetBox.MouseHover += InternetBox_MouseHover;
            selfieBox.MouseHover += SelfieBox_MouseHover;
            mediaBox.MouseHover += MediaBox_MouseHover;
            appBox.MouseHover += AppBox_MouseHover;
            officeBox.MouseHover += OfficeBox_MouseHover;
            systemBox.MouseHover += SystemBox_MouseHover;
            btnSettings.MouseHover += BtnSettings_MouseHover;
            closeButton.MouseHover += CloseButton_MouseHover;
            minimizeButton.MouseHover += MinimizeButton_MouseHover;
            this.MouseDown += MainForm_MouseDown;
            this.Resize += MainForm_Resize;
            notifyIcon1.MouseDoubleClick += NotifyIcon1_MouseDoubleClick;

            #endregion
        }

        #region Form Handler

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void minimizeForm()
        {
            this.WindowState = FormWindowState.Minimized;
            this.assistent.formMinized = true;
        }

        #endregion

        #region Hover Event Handlers

        private void SelfieBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.selfieBox, "Possible commands are: \n \n - Take A Selfie \n - Selfie");
        }

        private void InternetBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.internetBox, "Possible commands are: \n \n - Open Browser/Internet \n"
                + " - Go to Internet/Google/Facebook \n - Close Browser/Internet \n"
                + " - When 'Remember Me?' Enabled, Use 'Login' \n"
                + " - Go To Gmail \n");
        }

        private void WeatherBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.weatherBox, "Possible commands are: \n \n - What's the weather like? \n"
                + " - What's the temperature?");
        }

        private void MediaBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.mediaBox, "Possible commands are: \n \n - Open Radio \n - Open FunX \n"
                + " - Open Media Player \n - Open YouTube \n - Play \n - Pause \n");
        }

        private void AppBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.appBox, "Possible commands are: \n \n - Open Visual Studio \n"
                + " - Save (in apps where save is 'Ctrl + S')");
        }

        private void OfficeBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.officeBox, "Possible commands are: \n \n - Open Word \n - Open Powerpoint \n"
                + " - Open Excel \n - Open Notepad \n");
        }

        private void SystemBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.systemBox, "Possible commands are: \n \n - Copy \n - Cut \n"
                + " - Paste \n - Select All \n"
                + " - Read Selected Text \n - Stop (while reading text)");
        }

        private void BtnSettings_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnSettings, "Open Settings");
        }

        private void MinimizeButton_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.minimizeButton, "Minimize");
        }

        private void CloseButton_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.closeButton, "Exit");
        }

        #endregion

        #region Buttons Event Handlers

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            this.Hide();
            sf.Show();
            sf.Activate();
        }
        
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.assistent.Exit();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            if (this.assistent.formMinized == false)
            {
                minimizeForm();
            }
        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
            title.Focus();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void disableMicButton_Click(object sender, EventArgs e)
        {
            if (this.assistent.disableMic == false)
            {
                disableMicButton.BackgroundImage = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Images\muted.png"));
                this.assistent.disableMic = true;
                this.assistent.recEngine.RecognizeAsyncStop();
            }
            else
            {
                disableMicButton.BackgroundImage = Image.FromFile(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Images\microphone.png"));
                this.assistent.disableMic = false;
                this.assistent.recEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
        }

        #endregion

    }
}
