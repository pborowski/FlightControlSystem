using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FlightControlSystem
{
    public class Airport : MapObject
    {
        #region Constructors
        public Airport(string name, Point coordinates) 
            : base(name, coordinates)
        {
            MapObjectName = name; 
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(@"/FlightControlSystem;component/Images/airport.png", UriKind.Relative);
            bi.EndInit();
            ImageMapObject.Source = bi;
        }
        #endregion

        #region Methods

        public override void RenderMapObject(Canvas c)
        {
            Canvas.SetLeft(this, Coordinates.X);
            Canvas.SetTop(this, Coordinates.Y);
            c.Children.Add(this);
        }

        public override void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Dlg = new AirportSelectionDialog();
            MainWindow.Dlg.ShowDialog();
            Application.Current.Windows.OfType<MainWindow>().First().MenuItemStop.IsEnabled = true;
            Application.Current.Windows.OfType<MainWindow>().First().MenuItemPause.IsEnabled = true;
            if (MainWindow.Dlg.Selection != null)
            {
                MainWindow.Sys.CreateFlight(this, MainWindow.Dlg.Selection,AircraftType.Plane);
            }    
        }

        public override void MapObject_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (MapObjectName != null) NameLabel.Content = MapObjectName;
            NameLabel.Visibility = Visibility.Visible;
        }

        public override void ImageMapObject_OnMouseLeave(object sender, MouseEventArgs e)
        {
            NameLabel.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
