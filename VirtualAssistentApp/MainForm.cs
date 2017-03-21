using Emgu.CV;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace VirtualAssistentApp
{
    public partial class MainForm : Form
    {
        //OBJECTS
        private SpeechRecognitionEngine recEngine;
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        private Capture capture;
        private HandleProcess handler = new HandleProcess();
        private Bitmap image;
        private Grammar grammar;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        //PRIMITIVE TYPES
        private string UserName { get; set; }
        private string City { get; set; }
        private string Country { get; set; }
        private string BotName { get; set; }

        private string Temperature { get; set; }
        private string Condition { get; set; }

        private string Query { get; set; }

        private bool minimized { get; set; }
        private bool formMinized { get; set; }
        public bool UseAwake { get; set; }
        private bool selfie = false;


        public MainForm()
        {
            InitializeComponent();

            //READ SETTINGS
            readSettings();

            //INITIALIZING VARIABLES
            minimized = false;
            formMinized = false;
            explainLabel.Text = "Start By Saying '" + BotName + "'";

            //SET TOOLTIPS
            weatherBox.MouseHover += WeatherBox_MouseHover;
            internetBox.MouseHover += InternetBox_MouseHover;
            selfieBox.MouseHover += SelfieBox_MouseHover;
            mediaBox.MouseHover += MediaBox_MouseHover;
            appBox.MouseHover += AppBox_MouseHover;
            officeBox.MouseHover += OfficeBox_MouseHover;
            systemBox.MouseHover += SystemBox_MouseHover;
            btnSettings.MouseHover += BtnSettings_MouseHover;
            btnAddCommand.MouseHover += BtnAddCommand_MouseHover;
            closeButton.MouseHover += CloseButton_MouseHover;
            minimizeButton.MouseHover += MinimizeButton_MouseHover;
            maximizeButton.MouseHover += MaximizeButton_MouseHover;
            lockButton.MouseHover += LockButton_MouseHover;

            //LISTENERS
            this.MouseDown += MainForm_MouseDown;
            this.Resize += MainForm_Resize;
            notifyIcon1.MouseDoubleClick += NotifyIcon1_MouseDoubleClick;

            //RUN METHODS
            setupSpeechRecognition();

            //START RECOGNIZING
            recEngine.RecognizeAsync(RecognizeMode.Multiple);

            //SAY WELCOME TEXT
            if (UserName != null)
            {
                string[] greetings = new string[4];
                greetings[0] = "Hello " + UserName.Split(' ')[0] + ", how may I help you?";
                greetings[1] = "Yo, whats up," + UserName.Split(' ')[0];
                greetings[2] = "Hey, how are you today?";
                greetings[3] = "Ey, what up homeboy?";

                Random random = new Random();
                int rndm = random.Next(0, 3);

                synthesizer.SpeakAsync(greetings[rndm]);
            }
            else
            {
                synthesizer.SpeakAsync("Hello, how may I help you?");
            }
        }

        //MOVE FORM WITHOUT TITLEBAR
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // METHODS-------------------------------------------------------------------------------------------------

        //READ SETINGS
        private void readSettings()
        {
            var filename = @"C:\Github\VirtualAssistant\VirtualAssistentApp\settings.txt";
            var allLines = File.ReadAllLines(filename);

            if (allLines.Length > 0)
            {
                ArrayList settingsList = new ArrayList();

                foreach (var line in allLines)
                {
                    var currentLine = line.Split(':')[1];
                    settingsList.Add(currentLine);
                }

                UserName = settingsList[0].ToString();
                City = settingsList[1].ToString();
                Country = settingsList[2].ToString();
                string gender = settingsList[3].ToString();

                if (gender == "Male")
                {
                    synthesizer.SelectVoiceByHints(VoiceGender.Male);
                }
                else if (gender == "Female")
                {
                    synthesizer.SelectVoiceByHints(VoiceGender.Female);
                }
                else
                {
                    synthesizer.SelectVoiceByHints(VoiceGender.Female);
                }

                BotName = settingsList[4].ToString();

                string useawake = settingsList[5].ToString();
                if (useawake == "false" || useawake == "False")
                {
                    UseAwake = false;
                }
                else if (useawake == "true" || useawake == "True")
                {
                    UseAwake = true;
                }
                else
                {
                    UseAwake = true;
                }

            }
        }

        //---------------------------------------------------------------------------------------------------------
        private void setupSpeechRecognition()
        {
            RecognizerInfo recognizerInfo = null;
            foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if ((ri.Culture.TwoLetterISOLanguageName.Equals("en")) && (recognizerInfo == null))
                {
                    recognizerInfo = ri;
                    break;
                }
            }

            recEngine = new SpeechRecognitionEngine(recognizerInfo.Culture);

            recEngine.BabbleTimeout = TimeSpan.FromSeconds(4.0);
            recEngine.SetInputToDefaultAudioDevice();

            Choices commands = new Choices();
            commands.Add(File.ReadAllLines(@"C:\Github\VirtualAssistant\VirtualAssistentApp\commands.txt"));

            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Culture = recognizerInfo.Culture;
            gBuilder.Append(commands);
            grammar = new Grammar(new GrammarBuilder(gBuilder, 0, 5));

            try
            {
                recEngine.RequestRecognizerUpdate();
                recEngine.LoadGrammarAsync(grammar);
                recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        //EXIT PROGRAM
        private void Exit()
        {
            string[] goodbyes = new string[3];
            goodbyes[0] = "Have a nice day";
            goodbyes[1] = "See you later, alligator";
            goodbyes[2] = "Yo, check you later";

            Random random = new Random();
            int rndm = random.Next(0, 2);

            synthesizer.Speak(goodbyes[rndm]);
            Environment.Exit(0);
        }

        //---------------------------------------------------------------------------------------------------------
        //OPEN DEFAULT BROWSER
        private void openBrowser(string url)
        {
            synthesizer.Speak("Starting browser ");
            Process.Start(url);
        }

        //---------------------------------------------------------------------------------------------------------
        //MINIMIZE OR MAXIMIZE BROWSER
        private void minimizeBrowser()
        {
            if (minimized)
            {
                minimized = false;
                handler.CloseWindow("chrome");
                handler.CloseWindow("firefox");
                handler.CloseWindow("ie");
            }
            else
            {
                minimized = true;
                handler.CloseWindow("chrome");
                handler.CloseWindow("firefox");
                handler.CloseWindow("ie");
            }

        }

        //---------------------------------------------------------------------------------------------------------
        //GET WEATHER FROM YAHOO
        private String GetWeather(String input)
        {
            XmlDocument wData = new XmlDocument();
            String query = String.Format("https://query.yahooapis.com/v1/public/yql?q="
                + "select * from weather.forecast where u='c' and woeid in (select woeid from geo.places(1) where "
                + "text='" + City + ", " + Country + "')&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");

            try
            {
                wData.Load(query);
            }
            catch
            {
                MessageBox.Show("No Internet connection!");
                return "No internet";
            }

            XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);
            manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

            XmlNode channel = wData.SelectSingleNode("query").SelectSingleNode("results").SelectSingleNode("channel");
            XmlNodeList nodes = wData.SelectNodes("query/results/channel");

            try
            {
                Temperature = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;
                Condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;

                if (input == "temp")
                {
                    return Temperature;
                }

                if (input == "cond")
                {
                    return Condition;
                }
            }
            catch
            {
                return "Error Receiving data";
            }

            return "error";
        }

        //---------------------------------------------------------------------------------------------------------
        //START PROCESS 
        private void startProgram(string app)
        {
            Process process = null;

            try
            {
                process = Process.Start(app);
                synthesizer.Speak("Starting Application");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        //KILL PROCESS
        private void killProgram(string app)
        {
            Process[] process = null;

            try
            {
                process = Process.GetProcesses();

                foreach (var p in process)
                {
                    if (p.ProcessName == app)
                    {
                        p.Kill();
                        p.WaitForExit();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        //READ SELECTED TEXT
        private void readSelected()
        {
            string clipboardText = "";

            SendKeys.Send("^(c)");

            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                clipboardText = Clipboard.GetText(TextDataFormat.Text);
            }

            synthesizer.SpeakAsync(clipboardText);
        }

        //---------------------------------------------------------------------------------------------------------
        //MINIMIZE FORM
        private void minimizeForm()
        {
            this.Height = 36;
            this.Width = 320;
            formMinized = true;
        }

        //---------------------------------------------------------------------------------------------------------
        //MAXIMIZE FORM
        private void maximizeForm()
        {
            this.Height = 455;
            this.Width = 685;
            formMinized = false;
        }

        //---------------------------------------------------------------------------------------------------------
        //SEARCH
        private void startSearch(bool image)
        {
            try
            {
                recEngine.UnloadAllGrammars();

                Choices search = new Choices();
                search.Add(File.ReadAllLines(@"C:\Github\VirtualAssistant\VirtualAssistentApp\words.txt"));

                GrammarBuilder gBuilder = new GrammarBuilder();
                gBuilder.Append(search);
                grammar = new Grammar(new GrammarBuilder(gBuilder, 0, 5));

                recEngine.LoadGrammar(grammar);


                if (image == true)
                {
                    synthesizer.Speak("For what would you like to search images of?");
                }
                else
                {
                    synthesizer.Speak("For what would you like to search for?");
                }

                RecognitionResult r = recEngine.Recognize();
                Query = r.Text.ToString();

                //START SEARCH BASED ON IMAGE OR NOT
                if (image == true && Query != "")
                {
                    openBrowser("http://www.google.nl/images?q=" + Uri.EscapeDataString(Query));
                }
                else
                {
                    openBrowser("http://www.google.nl/search?q=" + Uri.EscapeDataString(Query));
                }


                recEngine.UnloadAllGrammars();

                Choices commands = new Choices();
                commands.Add(File.ReadAllLines(@"C:\Github\VirtualAssistant\VirtualAssistentApp\commands.txt"));

                gBuilder = new GrammarBuilder();
                gBuilder.Append(commands);
                grammar = new Grammar(new GrammarBuilder(gBuilder, 0, 5));

                recEngine.LoadGrammar(grammar);
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        //------------------------------------------------------------------------------------------------------------
        // LISTENERS----------------------------------------------------------------------------------------------
        
        //SPEECH RECOGNIZER LISTENER
        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Boolean isAwake = false;
            try
            {
                string speech = e.Result.Text;
                Debug.WriteLine("User said: " + speech);

                //CHECK IF AWAKE IS NEEDED
                if (UseAwake == false)
                {
                    isAwake = true;
                }

                //AWAKE PROGRAM
                if (speech == "Hey " + BotName || speech == BotName)
                {
                    isAwake = true;
                    synthesizer.Speak("Whats up");
                }

                //WHEN READING TEXT, SAY STOP TO CANCEL SPEAK
                if (speech == "Stop")
                {
                    var current = synthesizer.GetCurrentlySpokenPrompt();

                    if (current != null)
                    {
                        synthesizer.SpeakAsyncCancel(current);
                    }
                }

                if (isAwake == true)
                {

                    if ((speech.StartsWith("Search For") || speech.StartsWith("search for")) && !speech.Contains("images"))
                    {
                        recEngine.RecognizeAsyncStop();
                        startSearch(false);
                    }

                    else if (speech.StartsWith("Search For Images") || speech.StartsWith("search for images"))
                    {
                        recEngine.RecognizeAsyncStop();
                        startSearch(true);
                    }


                    switch (speech)
                    {
                        //--------------------------------------------------------------------------------------//
                        //----BEGIN OF APPLICATIONS---------------------------------------------------------------//

                        case "Open Word":
                            startProgram(@"C:\Program Files\Microsoft Office\root\Office16\WINWORD.exe");
                            break;
                        case "Close Word":
                            killProgram("WINWORD");
                            break;

                        case "Open Excel":
                            startProgram(@"C:\Program Files\Microsoft Office\root\Office16\EXCEL.exe");
                            break;
                        case "Close Excel":
                            killProgram("EXCEL");
                            break;

                        case "Open Powerpoint":
                            startProgram(@"C:\Program Files\Microsoft Office\root\Office16\POWERPNT.exe");
                            break;
                        case "Close Powerpoint":
                            killProgram("POWERPNT");
                            break;

                        case "Open Note Pad":
                            startProgram(@"C:\WINDOWS\system32\notepad.exe");
                            break;
                        case "Close Note Pad":
                            killProgram("notepad");
                            break;

                        case "Open Visual Studio":
                            startProgram(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe");
                            break;
                        case "Close Visual Studio":
                            killProgram("devenv");
                            break;

                        case "Save":
                            SendKeys.Send("^(s)");
                            break;

                        //----END OF APPLICATIONS---------------------------------------------------------------//
                        //--------------------------------------------------------------------------------------//
                        //----BEGIN OF INFORMATION---------------------------------------------------------------//

                        case "Whats The Date":
                            synthesizer.Speak("Today is " + DateTime.Now.ToString("dd MMMM"));
                            break;

                        case "Whats The Time":
                            synthesizer.Speak("The time is " + DateTime.Now.ToString("HH mm"));
                            break;

                        case "Whats The Weather Like":
                            synthesizer.Speak("The sky is " + GetWeather("cond").ToLower());
                            break;

                        case "Whats The Temperature":
                            string temperature = GetWeather("temp");
                            Debug.WriteLine("Temperature: " + temperature);
                            double temp = (double.Parse(temperature));

                            synthesizer.Speak("The temperature is " + Math.Round(temp, 1) + " degrees Celsius");
                            break;

                        //----END OF INFORMATION---------------------------------------------------------------//
                        //--------------------------------------------------------------------------------------//
                        //----BEGIN OF INTERNET---------------------------------------------------------------//

                        case "Go To Internet":
                        case "Go To Google":
                            openBrowser("http://www.google.nl");
                            break;

                        case "Go To G Mail":
                            openBrowser("https://gmail.com");
                            break;

                        case "Go To Facebook":
                            openBrowser("https://facebook.com");
                            break;

                        case "Login":
                            SendKeys.Send("{ENTER}");
                            break;

                        case "Open Internet":
                        case "Open Browser":
                            openBrowser("http://www.google.nl");
                            break;

                        case "Close Internet":
                        case "Close Browser":
                            minimizeBrowser();
                            break;

                        case "Show Me The Latest Headlines":
                            openBrowser("https://news.google.com/");
                            break;

                        //----END OF INTERNET---------------------------------------------------------------//
                        //--------------------------------------------------------------------------------------//
                        //----BEGIN OF ENTERTAINMENT---------------------------------------------------------------//

                        case "Take A Selfie":
                        case "Selfie":
                            capture = new Capture();
                            selfie = true;
                            Application.Idle += Application_Idle;
                            break;

                        case "Open Media Player":
                            Process.Start(@"C:\Program Files (x86)\Windows Media Player\wmplayer.exe");
                            break;
                        case "Close Media Player":
                            killProgram("wmplayer");
                            break;

                        case "Open Fun X":
                            openBrowser("http://radioplayer.npo.nl/funx/?channel=null");
                            break;

                        case "Open YouTube":
                        case "Open You Tjub":
                            openBrowser("https://youtube.com");
                            break;

                        case "Open Radio":
                            openBrowser("http://nederland.fm/");
                            break;

                        case "Play":
                            SendKeys.Send("^( )");
                            break;

                        case "Pause":
                            SendKeys.Send("^( )");
                            break;

                        //----END OF ENTERTAINMENT---------------------------------------------------------------//
                        //--------------------------------------------------------------------------------------//
                        //----BEGIN OF SYSTEM---------------------------------------------------------------//

                        case "Whats My Name":
                        case "Who Am I":
                            synthesizer.Speak("You Are " + UserName);
                            break;

                        case "Copy":
                            SendKeys.Send("^(c)");
                            break;

                        case "Cut":
                            SendKeys.Send("^(x)");
                            break;

                        case "Select All":
                            SendKeys.Send("^(a)");
                            break;

                        case "Paste":
                            SendKeys.Send("^(v)");
                            break;

                        case "Read Selected Text":
                            readSelected();
                            break;

                        case "Close":
                        case "Exit":
                        case "Goodbye":
                            Exit();
                            break;

                        //----END OF SYSTEM---------------------------------------------------------------//
                        //--------------------------------------------------------------------------------------//

                        default:
                            break;

                        //--------------------------------------------------------------------------------------//
                    }

                    isAwake = false;

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        //LISTENER TO TAKE SELFIE FROM WEBCAM
        private void Application_Idle(object sender, EventArgs e)
        {
            if (selfie)
            {
                synthesizer.Speak("Say cheese in 3");
                Thread.Sleep(1);
                synthesizer.Speak("2");
                Thread.Sleep(1);
                synthesizer.Speak("1");

                image = capture.QueryFrame().Bitmap;
                image.Save(@"C:/VirtualAssistant/Selfie-" + DateTime.Now.ToString("ddhhmm") + ".png", ImageFormat.Png);

                Thread.Sleep(500);

                synthesizer.Speak("Your picture is taken");

                selfie = false;
            }
        }

        //---------------------------------------------------------------------------------------------------------
        //SHOW TOOLTIPS WHEN HOVERING OVER ICONS
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
            tt.SetToolTip(this.appBox, "Possible commands are: \n \n - Open Visual Studio"
                + " - Save(when in VS)");
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

        private void BtnAddCommand_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.btnAddCommand, "Add New Command");
        }

        private void LockButton_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.lockButton, "Minimize To System Tray");
        }

        private void MaximizeButton_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.maximizeButton, "Maximize");
        }

        private void MinimizeButton_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.minimizeButton, "Minimize As Toolbar");
        }

        private void CloseButton_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.closeButton, "Close Program");
        }

        //------------------------------------------------------------------------------------------------------
        //SETTINGS BUTTON
        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            this.Hide();
            sf.Show();
            sf.Activate();
        }

        //ADD COMMAND BUTTON
        private void btnAddCommand_Click(object sender, EventArgs e)
        {
            AddCommandForm af = new AddCommandForm();
            this.Hide();
            af.Show();
            af.Activate();
        }

        //CLOSE BUTTON
        private void closeButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        //MINIMIZE BUTTON
        private void minimizeButton_Click(object sender, EventArgs e)
        {
            if (formMinized == false)
            {
                minimizeForm();
            }
        }

        //MAXIMIZE BUTTON
        private void maximizeButton_Click(object sender, EventArgs e)
        {
            if (formMinized == true)
            {
                maximizeForm();
            }
        }

        //SEND TO SYSTEM TRAY
        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        //REMOVE FROM SYSTEM TRAY
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(1000);
            }
        }

        private void lockButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        //----------------------------------------------------------------------------------------//


    }
}
