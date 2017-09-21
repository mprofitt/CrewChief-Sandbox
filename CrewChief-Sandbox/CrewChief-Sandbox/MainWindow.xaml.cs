﻿using System;
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
using iRacingSimulator.Drivers;
using iRacingSimulator.Events;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CrewChief_Sandbox
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<Driver> _drivers;
        private ObservableCollection<TrackConditions> _weather;
        private ICollectionView _view;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the grid
            this.SetupGrid();

            Sim.Instance.SessionInfoUpdated += OnSessionInfoUpdated;
            Sim.Instance.TelemetryUpdated += OnTelemetryUpdated;
            Sim.Instance.Connected += wrapper_Connected;
            Sim.Instance.Disconnected += wrapper_Disconnected;
            Sim.Instance.Start();

        }

        private void SetupGrid()
        {
            // Create a new observable collection of drivers
            _drivers = new ObservableCollection<Driver>();
            _weather = new ObservableCollection<TrackConditions>();

            // Create a new collectionview to bind to the grid
            //_view = CollectionViewSource.GetDefaultView(_drivers);
            _view = CollectionViewSource.GetDefaultView(_weather);

            // Bind it to the grid
            grid.ItemsSource = _view;
        }

        private void RefreshGrid()
        {
            // Might not be necessary at all due to use of ObservableCollection which automatically notifies grid of changes
            _view.Refresh();
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
            // Update the list of drivers: simply clear the old list and re-fill it with the drivers from iRacingSimulator
            _drivers.Clear();

            // ObservableCollection does not support AddRange so we just call Add in a loop
            // Note: this is probably not a good idea as it notifies the grid of updates for every driver, rather than just once
            foreach (var driver in Sim.Instance.Drivers)
            {
                _drivers.Add(driver);
            }

            // Update the grid
            this.RefreshGrid();

        }

        private void OnTelemetryUpdated(object sender, SdkWrapper.TelemetryUpdatedEventArgs e)
        {
            // No need to do anything here in this example, but you can access all telemetry:
            double sessionTime = e.TelemetryInfo.SessionTime.Value;

            this.RefreshGrid();
        }

        private void OnRaceEvent(object sender, Sim.RaceEventArgs e)
        {
            if (e.Event.Type == RaceEvent.EventTypes.DriverSwap)
            {
                var swapEvent = (DriverSwapRaceEvent)e.Event;
                MessageBox.Show(string.Format("Driver {0} replaced driver {1}.", swapEvent.PreviousDriverName, swapEvent.CurrentDriverName));
            }
        }
    }
}
