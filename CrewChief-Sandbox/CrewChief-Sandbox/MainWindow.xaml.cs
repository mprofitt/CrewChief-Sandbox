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

            

            this.isUpdatingDrivers = false;
        }

        private void OnTelemetryUpdated(object sender, SdkWrapper.TelemetryUpdatedEventArgs e)
        {
            ParseWeather(e.TelemetryInfo);
        }

        private void ParseWeather(TelemetryInfo TelemetryInfo)
        {

            Weather weather = new Weather(wrapper.GetTelemetryValue<int>("Skies").Value,
                wrapper.GetTelemetryValue<float>("AirTemp").Value,
                wrapper.GetTelemetryValue<float>("AirPressure").Value,
                wrapper.GetTelemetryValue<float>("WindVel").Value,
                wrapper.GetTelemetryValue<float>("WindDir").Value,
                wrapper.GetTelemetryValue<float>("RelativeHumidity").Value,
                wrapper.GetTelemetryValue<float>("FogLevel").Value);
            // Instantiate an instance of Weather class

            var newWeather = new List<Weather>();
            // Create a local list newWeather

            newWeather.Add(new Weather(weather.Skies, weather.AirTemp,weather.AirPressure,
                weather.WindVel,weather.WindDir,weather.RelativeHumidity,weather.FogLevel));

            weather.AirPressure = wrapper.GetTelemetryValue<float>("AirPressure").Value;
            weather.Skies = wrapper.GetTelemetryValue<int>("Skies").Value;
            weather.AirTemp = wrapper.GetTelemetryValue<float>("AirTemp").Value;
            weather.WindVel = wrapper.GetTelemetryValue<float>("WindVel").Value;
            weather.WindDir = wrapper.GetTelemetryValue<float>("WindDir").Value;
            weather.RelativeHumidity = wrapper.GetTelemetryValue<float>("RelativeHumidity").Value;
            weather.FogLevel = wrapper.GetTelemetryValue<float>("FogLevel").Value;

            newWeather.Add(weather);
            // Add data to list newWeather

            foreach(Weather _weather in weather)
            {


            }

            //listWeather.Clear();
            // Clear listWeather

            //listWeather.AddRange(newWeather);
            // Copy newWeather to listWeather

            weatherTextBox.Text = string.Format("Current Conditions\nCloud Cover: {0}\n" +
                "Air Temperature: {1}\n" +
                "Wind Direction: {2}\n" +
                "Wind Velocity: {3}\n" +
                "Relative Humidity: {4}\n" +
                "Fog Level: {5}\n", weather.Skies,weather.AirTemp,weather.WindDir,weather.WindVel,weather.RelativeHumidity,weather.FogLevel);

            //weatherGrid.DataSource = weather;

            // dataGridView1.DataSource = persons;



            // var list = new BindingList<Weather>(weather);
            //myGrid.DataSource = list;



        }
    }
}
