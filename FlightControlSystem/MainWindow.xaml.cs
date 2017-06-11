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

namespace FlightControlSystem
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SystemObject sys;
        public static AirportSelectionDialog dlg;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region MenuEvenHandlers
        private void MenuItem_Start_OnClick(object sender, RoutedEventArgs e)
        {
            sys = SystemObject.CreateSystem(CanvasMap);
            MenuItemAddRandom.IsEnabled = true;
            MenuItemStart.IsEnabled = false;
            MenuItemStop.IsEnabled = true;
            MenuItemPause.IsEnabled = true;
        }

        private void MenuItem_GeneratePlane_OnClick(object sender, RoutedEventArgs e)
        {
            sys.GenerateRandomFlight(AircraftType.Plane);
            MenuItemStop.IsEnabled = true;
            MenuItemPause.IsEnabled = true;
        }

        private void MenuItem_GenerateBaloon_OnClick(object sender, RoutedEventArgs e)
        {
            sys.GenerateRandomFlight(AircraftType.Balloon);
            MenuItemStop.IsEnabled = true;
            MenuItemPause.IsEnabled = true;
        }

        private void MenuItem_GenerateHelicopter_OnClick(object sender, RoutedEventArgs e)
        {
            sys.GenerateRandomFlight(AircraftType.Helicopter);
            MenuItemStop.IsEnabled = true;
            MenuItemPause.IsEnabled = true;
        }

        private void MenuItem_GenerateGlider_OnClick(object sender, RoutedEventArgs e)
        {
            sys.GenerateRandomFlight(AircraftType.Glider);
            MenuItemStop.IsEnabled = true;
            MenuItemPause.IsEnabled = true;
        }

        private void MenuItem_Stop_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteAllAircrafts(sys.C.Children);
            foreach (UIElement element in sys.C.Children)
            {
                if (element is Aircraft)
                {
                    MessageBox.Show((element as Aircraft).IdNumber.ToString());
                }
            }
            MenuItemPause.IsEnabled = false;
            MenuItemStop.IsEnabled = false;
            ManuItemContinue.IsEnabled = false;

        }

        private void MenuItem_Pause_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (Flight f in sys.Flights)
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
            foreach (Flight f in sys.Flights)
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
                    foreach (Flight f in sys.Flights)
                    {
                        if (Equals(f.AircraftFlying, element))
                        {
                            sys.Flights.Remove(f);
                            break;
                        }   
                    }
                    DeleteAllAircrafts(c);
                    break;
                }
            }
        }
        #endregion
    }
}
