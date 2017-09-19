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
using iRacingSimulator;

namespace CrewChief_Sandbox
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Sim.Instance.SessionInfoUpdated += OnSessionInfoUpdated;
            Sim.Instance.TelemetryUpdated += OnTelemetryUpdated;
            Sim.Instance.Connected += wrapper_Connected;
            Sim.Instance.Disconnected += wrapper_Disconnected;
            Sim.Instance.Start();

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

        }

        private void OnTelemetryUpdated(object sender, SdkWrapper.TelemetryUpdatedEventArgs e)
        {

        }

        private void ParseWeather(TelemetryInfo TelemetryInfo)
        {

        }
    }
}
