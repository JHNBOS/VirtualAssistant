﻿using Emgu.CV;
using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class Form1 : Form
    {
        //OBJECTS
        private SpeechRecognitionEngine recEngine;
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        private Capture capture;
        private HandleProcess handler = new HandleProcess();
        private Bitmap image;

        //PRIMITIVE TYPES
        private string UserName { get; set; }
        private string City { get; set; }
        public string Country { get; set; }

        private string Temperature { get; set; }
        private string Condition { get; set; }

        private string Query { get; set; }

        private bool minimized { get; set; }
        private bool selfie = false;
        private bool awake = false;


        public Form1()
        {
            InitializeComponent();

            //READ SETTINGS
            readSettings();

            //INITIALIZING VARIABLES
            minimized = false;

            //SET TOOLTIPS
            weatherBox.MouseHover += WeatherBox_MouseHover;
            internetBox.MouseHover += InternetBox_MouseHover;
            selfieBox.MouseHover += SelfieBox_MouseHover;

            //RUN METHODS
            setupSpeechRecognition();

            //START RECOGNIZING
            recEngine.RecognizeAsync(RecognizeMode.Multiple);

            //SAY WELCOME TEXT
            if (UserName != null)
            {
                synthesizer.SpeakAsync("Hello " + UserName.Split(' ')[0] + ", how may I help you?");
            }
            else
            {
                synthesizer.SpeakAsync("Hello, how may I help you?");
            }
        }

        //--------------------------------------------------------------------------------------------//
        // METHODS

        private void readSettings()
        {
            var filename = @"C:\Github\VirtualAssistant\VirtualAssistentApp\settings.txt";
            var allLines = File.ReadAllLines(filename);

            if (allLines.Length  > 0)
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
            }
        }

        private void setupSpeechRecognition()
        {
            //RECOGNIZER
            RecognizerInfo recognizerInfo = null;
            foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
            {
                if ((ri.Culture.TwoLetterISOLanguageName.Equals("en")) && (recognizerInfo == null))
                {
                    recognizerInfo = ri;
                    break;
                }
            }

            //SET RECOGNIZER ENGINE
            recEngine = new SpeechRecognitionEngine(recognizerInfo.Culture);
            recEngine.BabbleTimeout = TimeSpan.FromSeconds(3.2);

            //SELECT MICROPHONE
            recEngine.SetInputToDefaultAudioDevice();

            //SETUP POSSIBLE COMMANDS
            Choices commands = new Choices();
            commands.Add(File.ReadAllLines(@"C:\Github\VirtualAssistant\VirtualAssistentApp\commands.txt"));

            //GRAMMARBUILDER
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Culture = recognizerInfo.Culture;
            gBuilder.Append(commands);

            //GRAMMAR
            Grammar grammar = new Grammar(new GrammarBuilder(gBuilder, 0, 5));

            //RECOGNITION ENGINE
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

        //EXIT PROGRAM
        private void Exit()
        {
            synthesizer.Speak("Have a nice day");
            Environment.Exit(0);
        }

        //OPEN DEFAULT BROWSER
        private void openBrowser(string url)
        {
            synthesizer.Speak("Starting browser ");
            Process.Start(url);
        }

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

        //GET WEATHER FROM GOOGLE
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

        //Kill process
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

        //--------------------------------------------------------------------------------------------//
        // LISTENERS

        //SPEECH RECOGNIZER LISTENER
        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                string speech = e.Result.Text;
                Debug.WriteLine("User said: " + speech);

                if (speech == "Hey Mary" || speech == "Mary")
                {
                    awake = true;
                    synthesizer.Speak("Whats up");
                }

                if (awake == true)
                {

                    switch (speech)
                    {
                        case "Open Word":
                            Process.Start(@"C:\Program Files\Microsoft Office\root\Office16\WINWORD.EXE");
                            break;

                        case "Close Word":
                            killProgram("WINWORD");
                            break;

                        case "Whats The Date":
                            synthesizer.Speak("Today is " + DateTime.Now.ToString("dd MMMM"));
                            break;

                        case "Whats The Time":
                            synthesizer.Speak("The time is " + DateTime.Now.ToString("HH mm"));
                            break;

                        case "Whats The Weather Like":
                            synthesizer.Speak("The sky is " + GetWeather("cond").ToLower());
                            break;

                        case "Whats Todays Temperature":
                            string temperature = GetWeather("temp");
                            Debug.WriteLine("Temperature: " + temperature);
                            double temp = (double.Parse(temperature));

                            synthesizer.Speak("The temperature is " + Math.Round(temp, 1) + " degrees Celsius");
                            break;

                        case "Go To Internet":
                        case "Go To Google":
                            openBrowser("http://www.google.nl");
                            break;

                        case "Open Internet":
                        case "Open Browser":
                            openBrowser("http://www.google.nl");
                            break;

                        case "Close Internet":
                        case "Close Browser":
                            minimizeBrowser();
                            break;

                        case "Take A Selfie":
                        case "Selfie":
                            capture = new Capture();
                            selfie = true;
                            Application.Idle += Application_Idle;
                            break;

                        case "Show Me The Latest Headlines":
                            openBrowser("https://news.google.com/");
                            break;

                        case "Close":
                        case "Exit":
                        case "Goodbye":
                            Exit();
                            break;

                        default:
                            break;
                    }
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

        //SHOW TOOLTIPS WHEN HOVERING OVER ICONS
        private void SelfieBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.selfieBox, "Possible commands are: \n \n - Take A Selfie \n - Selfie");
        }

        private void InternetBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.internetBox, "Possible commands are: \n \n - Open Browser/Internet \n - Go to Internet/Google \n - Close Browser/Internet");
        }

        private void WeatherBox_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.weatherBox, "Possible commands are: \n \n - What's the weather like? \n - What's todays temperature?");
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            sf.Show();
            sf.Activate();
        }

        //----------------------------------------------------------------------------------------//


    }
}
