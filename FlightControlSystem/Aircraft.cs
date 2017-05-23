using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace FlightControlSystem
{
    public class Aircraft : MapObject
    {
        #region Properties

        public AircraftType Type { get; set; }
        public double Altitude { get; set; }
        public double Speed { get; set; }
        public int IdNumber { get; private set; }
        private string Origin { get; set; }
        private string Destination { get; set;}
        private string _name;

        #endregion

        #region Constructors

        public Aircraft(string name, Point coordinates, AircraftType t, int id)
            : base(name, coordinates)
        {
            _name = name;
            IdNumber = id;
            Type = t;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            switch (t)
            {
                case AircraftType.Plane:
                    bi.UriSource = new Uri(@"/FlightControlSystem;component/Images/plane.png", UriKind.Relative);
                    break;
                case AircraftType.Balloon:
                    bi.UriSource = new Uri(@"/FlightControlSystem;component/Images/baloon.png", UriKind.Relative);
                    break;
                case AircraftType.Glider:
                    bi.UriSource = new Uri(@"/FlightControlSystem;component/Images/glider.png", UriKind.Relative);
                    break;
                case AircraftType.Helicopter:
                    bi.UriSource = new Uri(@"/FlightControlSystem;component/Images/helicopter.png", UriKind.Relative);
                    break;
            }
            
            bi.EndInit();
            ImageMapObject.Width = 30;
            ImageMapObject.Height = 30;
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
            
            MessageBox.Show(Name);
        }

        public override void MapObject_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Name != null)
            {
                NameLabel.Content = IdNumber;
            }
            NameLabel.Visibility = Visibility.Visible;
            //ChangeDestinationDialog window = new ChangeDestinationDialog
            //{
            //    LbName = {Content = MapObjectName},
            //    LbOrigin = {Content = Origin},
            //    CbDestination = {Text = Destination},
            //    LbSpeed = {Content = Speed},
            //    LbAltitude = {Content = Altitude}
            //};
            //window.Show();

        }

        public override void ImageMapObject_OnMouseLeave(object sender, MouseEventArgs e)
        {
            NameLabel.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}
