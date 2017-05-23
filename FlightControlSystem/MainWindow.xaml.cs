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

        private void MenuItem_Start_OnClick(object sender, RoutedEventArgs e)
        {
            sys = SystemObject.CreateSystem(this.CanvasMap);
            MenuItemAddRandom.IsEnabled = true;
            MenuItemStart.IsEnabled = false;
        }

        private void MenuItem_GeneratePlane_OnClick(object sender, RoutedEventArgs e)
        {
            sys.GenerateRandomFlight(AircraftType.Plane);
        }

        private void MenuItem_GenerateBaloon_OnClick(object sender, RoutedEventArgs e)
        {
            sys.GenerateRandomFlight(AircraftType.Balloon);
        }

        private void MenuItem_GenerateHelicopter_OnClick(object sender, RoutedEventArgs e)
        {
            sys.GenerateRandomFlight(AircraftType.Helicopter);
        }

        private void MenuItem_GenerateGlider_OnClick(object sender, RoutedEventArgs e)
        {
            sys.GenerateRandomFlight(AircraftType.Glider);
        }

        private void MenuItem_Stop_OnClick(object sender, RoutedEventArgs e)
        {
            DeleteAllAircrafts(CanvasMap.Children);
        }

        private void DeleteAllAircrafts(UIElementCollection c)
        {
            foreach (UIElement element in c)
            {
                if (element is Aircraft)
                {
                    c.Remove(element);
                    DeleteAllAircrafts(c);
                    break;
                }
            }
            return;
        }
    }
}
