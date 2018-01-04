using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FlightControlSystem
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SystemObject Sys;
        public static AirportSelectionDialog Dlg;
        public static ObservableCollection<Flight> FlightsObservable;

        public MainWindow()
        {
            //FlightsObservable = new ObservableCollection<Flight>();
            InitializeComponent();
           
        }

        #region MenuEvenHandlers
        private void MenuItem_Start_OnClick(object sender, RoutedEventArgs e)
        {
            Sys = SystemObject.CreateSystem(CanvasMap);
            //DataGrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding("Flights")
            //{
            //    Source = Sys.AllFlightsList,
            //    Mode = BindingMode.TwoWay
            //});
            DataGrid.ItemsSource = Sys.AllFlightsList;
            MenuItemAddRandom.IsEnabled = true;
            MenuItemStart.IsEnabled = false;
            MenuItemStop.IsEnabled = true;
            MenuItemPause.IsEnabled = true;
        }

        private void MenuItem_GeneratePlane_OnClick(object sender, RoutedEventArgs e)
        {
            Sys.GenerateRandomFlight(AircraftType.Plane);
            MenuItemStop.IsEnabled = true;
            MenuItemPause.IsEnabled = true;
        }

        private void MenuItem_GenerateBaloon_OnClick(object sender, RoutedEventArgs e)
        {
            Sys.GenerateRandomFlight(AircraftType.Balloon);
            MenuItemStop.IsEnabled = true;
            MenuItemPause.IsEnabled = true;
        }

        private void MenuItem_GenerateHelicopter_OnClick(object sender, RoutedEventArgs e)
        {
            Sys.GenerateRandomFlight(AircraftType.Helicopter);
            MenuItemStop.IsEnabled = true;
            MenuItemPause.IsEnabled = true;
        }

        private void MenuItem_GenerateGlider_OnClick(object sender, RoutedEventArgs e)
        {
            Sys.GenerateRandomFlight(AircraftType.Glider);
            MenuItemStop.IsEnabled = true;
            MenuItemPause.IsEnabled = true;
        }

        private void MenuItem_Stop_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteAllAircrafts(CanvasMap.Children);
            foreach (Flight f in Sys.Flights)
            {
                MessageBox.Show(f.AircraftFlying.IdNumber.ToString());
            }
            MenuItemPause.IsEnabled = false;
            MenuItemStop.IsEnabled = false;
            ManuItemContinue.IsEnabled = false;

        }

        private void MenuItem_Pause_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Flight f in Sys.Flights)
            {
                f.FlightStory.Pause();
            }
            //foreach (Flight f in sys.Flights)
            //{
            //    MessageBox.Show(f.AircraftFlying.Coordinates.ToString());
            //}
            ManuItemContinue.IsEnabled = true;
        }

        private void MenuItem_Continue_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Flight f in Sys.Flights)
            {
                f.FlightStory.Resume();
            }
        }

        private void DeleteAllAircrafts(UIElementCollection c)
        {
            foreach (UIElement element in c)
            {
                if (element is Aircraft)
                {
                    c.Remove(element);
                    foreach (Flight f in Sys.Flights)
                    {
                        if (Equals(f.AircraftFlying, element))
                        {
                            Sys.Flights.Remove(f);
                            break;
                        }   
                    }
                    DeleteAllAircrafts(c);
                    break;
                }
            }
        }
        #endregion

        private void MenuItem_GeneralInformation_OnClick(object sender, RoutedEventArgs e)
        {
            GeneralInformationDialog window = new GeneralInformationDialog();
            window.Show();
        }

        private void MenuItem_About_Onclick(object sender, RoutedEventArgs e)
        {
            AboutDialog window = new AboutDialog();
            window.Show();
        }
    }
}
