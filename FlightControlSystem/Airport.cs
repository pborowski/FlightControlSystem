using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FlightControlSystem
{
    public class Airport : MapObject
    {
        #region Constructors
        public Airport(string name, Point coordinates) 
            : base(name, coordinates)
        {
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
            Canvas.SetLeft(this, this.Coordinates.X);
            Canvas.SetTop(this, this.Coordinates.Y);
            c.Children.Add(this);
        }

        public override void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.dlg = new AirportSelectionDialog();
            MainWindow.dlg.ShowDialog();
            if(MainWindow.dlg.selection != null)
            {
                MainWindow.sys.createFlight(this, MainWindow.dlg.selection);
            }    
        }

        #endregion
    }
}
