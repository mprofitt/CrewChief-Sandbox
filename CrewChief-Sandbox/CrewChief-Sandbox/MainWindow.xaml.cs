using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using iRacingSdkWrapper;
using System.Diagnostics;

namespace CrewChief_Sandbox
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SdkWrapper wrapper;
        private List<Weather> listWeather;
        private bool isUpdatingDrivers;

        public MainWindow()
        {
            InitializeComponent();

            wrapper = new SdkWrapper();
            wrapper.TelemetryUpdated += OnTelemetryUpdated;
            wrapper.SessionInfoUpdated += OnSessionInfoUpdated;
            wrapper.Connected += wrapper_Connected;
            wrapper.Disconnected += wrapper_Disconnected;
            wrapper.Start();
        }
        private void wrapper_Connected(object sender, EventArgs e)
        {
            Debug.WriteLine("*** wrapper connected ***\n");
        }

        private void wrapper_Disconnected(object sender, EventArgs e)
        {
            Debug.WriteLine("*** wrapper disconnected ***\n");
        }

        private void OnSessionInfoUpdated(object sender, SdkWrapper.SessionInfoUpdatedEventArgs e)
        {
            this.isUpdatingDrivers = true;

            ParseWeather(e.SessionInfo);

            this.isUpdatingDrivers = false;
        }

        private void OnTelemetryUpdated(object sender, SdkWrapper.TelemetryUpdatedEventArgs e)
        {

        }

        private void ParseWeather(SessionInfo sessionInfo)
        {

            sessionInfoTextBox.Text = e.SessionInfo;
            sessionInfoLabel.Text = string.Format("Session info: (updated @ session time {0})", e.UpdateTime);

            Weather weather = new Weather();
            // Instantiate an instance of Weather class

            var newWeather = new List<Weather>();
            // Create a local list newWeather

            YamlQuery query = sessionInfo["Weekendinfo"]["WeekendOptions"];
            weather.SessionID = int.Parse(query["SessionID"].GetValue("0"));
            weather.AirPressure = float.Parse(query["AirPressure"].GetValue("0"));
            weather.Skies = query["Skies"].GetValue("0");
            weather.AirTemp = float.Parse(query["AirTemp"].GetValue("0"));
            weather.AirPressure = float.Parse(query["AirPressure"].GetValue("0"));
            weather.WindVel = float.Parse(query["WindVel"].GetValue("0"));
            weather.WindDir = float.Parse(query["WindDir"].GetValue("0"));
            weather.RelativeHumidity = int.Parse(query["RelativeHumidity"].GetValue("0"));
            weather.FogLevel = int.Parse(query["FogLevel"].GetValue("0"));

            newWeather.Add(weather);

            Debug.WriteLine("weather.SessionID: {0}\n", weather.SessionID);
            Debug.WriteLine("weather.Skies: {0}\n", weather.Skies);
            Debug.WriteLine("weather.AirTemp: {0}\n", weather.AirTemp);
            Debug.WriteLine("weather.WindVel: {0}\n", weather.WindVel);


            // Add data to list newWeather

            // listWeather.Clear();
            // Clear listWeather

            // listWeather.AddRange(newWeather);
            // Copy newWeather to listWeather

            //weatherGrid.DataSource = weather;

            // dataGridView1.DataSource = persons;



            // var list = new BindingList<Weather>(weather);
            //myGrid.DataSource = list;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (wrapper.IsRunning)
            {
                wrapper.Stop();
                startButton.Text = "Start";
            }
            else
            {
                wrapper.Start();
                startButton.Text = "Stop";
            }
        }
}
