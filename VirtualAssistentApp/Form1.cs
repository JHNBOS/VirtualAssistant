using System;
using System.Diagnostics;
using System.Drawing;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;
using Emgu.CV;
using System.Drawing.Imaging;
using System.Xml;
using System.IO;
using System.Threading;

namespace VirtualAssistentApp
{
    public partial class Form1 : Form
    {
        //OBJECTS
        private SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        private Capture capture;
        private HandleProcess handler = new HandleProcess();
        private Bitmap image;

        //PRIMITIVE TYPES
        private string userName { get; set; }
        private string temperature { get; set; }
        private string condition { get; set; }
        private string City { get; set; }
        private string Query { get; set; }
        private bool minimized { get; set; }
        private bool awake = false;
        private bool selfie = false;


        public Form1()
        {
            InitializeComponent();

            //INITIALIZING VARIABLES
            City = "Rotterdam";
            userName = "Johan Bos";
            string fname = userName.Split(' ')[0];
            minimized = false;

            //RUN METHODS
            setupSpeechRecognition();

            //START RECOGNIZING
            recEngine.RecognizeAsync(RecognizeMode.Multiple);

            //SAY WELCOME TEXT
            synthesizer.SpeakAsync("Hello " + fname + ", how may I help you?");

        }

        //--------------------------------------------------------------------------------------------//
        // METHODS

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

            //SET VOICE
            synthesizer.SelectVoiceByHints(VoiceGender.Female);

            //SELECT MICROPHONE
            recEngine.SetInputToDefaultAudioDevice();

            //SETUP POSSIBLE COMMANDS
            Choices commands = new Choices();
            commands.Add(File.ReadAllLines(@"D:\Johan Bos\Documents\Visual Studio 2015\Projects\PersonalProjects\VirtualAssistentApp\commands.txt"));

            //GRAMMARBUILDER
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Culture = recognizerInfo.Culture;
            gBuilder.Append(commands);

            //GRAMMAR
            Grammar grammar = new Grammar(gBuilder);

            //RECOGNITION ENGINE
            recEngine.RequestRecognizerUpdate();
            recEngine.LoadGrammarAsync(grammar);
            recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
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

        //GET WEATHER FROM YAHOO
        private String GetWeather(String input)
        {
            String query = String.Format("https://query.yahooapis.com/v1/public/yql?q=select * from weather.forecast where woeid in (select woeid from geo.places(1) where text='" + City + "')&format=xml&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
            XmlDocument wData = new XmlDocument();
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
                temperature = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;
                condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;

                if (input == "temp")
                {
                    return temperature;
                }

                if (input == "cond")
                {
                    return condition;
                }
            }
            catch
            {
                return "Error Receiving data";
            }
            return "error";
        }

        //--------------------------------------------------------------------------------------------//
        // LISTENERS

        //SPEECH RECOGNIZER LISTENER
        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            if (e.Result.Text == "hey V A")
            {
                awake = true;
            }

            else if (awake == true)
            {
                switch (e.Result.Text)
                {
                    case "what date is it":
                        synthesizer.Speak("Today is " + DateTime.Now.ToString("dd MMMM"));
                        break;

                    case "what time is it":
                        synthesizer.Speak("The time is " + DateTime.Now.ToString("hh mm"));
                        break;

                    case "whats the weather like":
                        synthesizer.Speak("The sky is " + GetWeather("cond").ToLower());
                        break;

                    case "whats the temperature":
                        string temperature = GetWeather("temp");
                        double temp = 5.0 / 9.0 * (double.Parse(temperature) - 32);

                        synthesizer.Speak("The temperature is " + Math.Round(temp, 1) + " degrees Celsius");
                        break;

                    case "go to internet":
                    case "go to google":
                        openBrowser("http://www.google.nl");
                        break;

                    case "open internet":
                    case "open browser":
                        minimizeBrowser();
                        break;

                    case "close internet":
                    case "close browser":
                        minimizeBrowser();
                        break;

                    case "take a selfie":
                    case "selfie":
                        capture = new Capture();
                        selfie = true;
                        Application.Idle += Application_Idle;
                        break;

                    case "show me the latest headlines":
                        openBrowser("https://news.google.com/");
                        break;

                    case "close":
                    case "exit":
                        Exit();
                        break;

                    default:
                        break;
                }

                awake = false;
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

        //----------------------------------------------------------------------------------------//


    }
}
