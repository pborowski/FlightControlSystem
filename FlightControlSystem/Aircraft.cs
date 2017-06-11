using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FlightControlSystem
{
    public class Aircraft : MapObject
    {
        #region Properties

        public AircraftType Type { get; set; }
        public double Altitude { get; set; }
        public double Speed { get; set; }
        public int IdNumber { get; }
        public string Origin { get; set; }
        public string Destination { get; set;}
        private readonly string _name;

        #endregion

        #region Constructors

        public Aircraft(string name, Point coordinates, AircraftType t, int id)
            : base(name, coordinates)
        {
            var rnd = new Random();
            _name = name;
            IdNumber = id;
            Type = t;
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            switch (t)
            {
                case AircraftType.Plane:
                    bi.UriSource = new Uri(@"/FlightControlSystem;component/Images/plane.png", UriKind.Relative);
                    Altitude =10000+ (rnd.Next(3) * 1000) + rnd.Next(999);
                    Speed = 500+ (rnd.Next(3) * 100) + rnd.Next(99);
                    break;
                case AircraftType.Balloon:
                    bi.UriSource = new Uri(@"/FlightControlSystem;component/Images/baloon.png", UriKind.Relative);
                    Altitude = 100 + rnd.Next(99);
                    Speed = 10 + (rnd.Next(3) * 10);
                    break;
                case AircraftType.Glider:
                    bi.UriSource = new Uri(@"/FlightControlSystem;component/Images/glider.png", UriKind.Relative);
                    Altitude = 100 + rnd.Next(99);
                    Speed = 50 + (rnd.Next(5) * 10);
                    break;
                case AircraftType.Helicopter:
                    bi.UriSource = new Uri(@"/FlightControlSystem;component/Images/helicopter.png", UriKind.Relative);
                    Altitude = 100 + (rnd.Next(9)*1000)+ rnd.Next(99);
                    Speed = 50 + (rnd.Next(10) * 10);
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
            Canvas.SetLeft(this, Coordinates.X);
            Canvas.SetTop(this, Coordinates.Y);
            c.Children.Add(this);
        }

        public override void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            //MessageBox.Show(Name);
            ChangeDestinationDialog window = new ChangeDestinationDialog();

            foreach (Flight f in MainWindow.Sys.Flights)
            {
                f.FlightStory.Pause();
            }
            foreach (Flight f in MainWindow.Sys.Flights)
            {
                Aircraft aircraft = sender as Aircraft;
                if (aircraft != null && f.AircraftFlying.IdNumber == aircraft.IdNumber)
                {
                    window.LbName.Content = f.AircraftFlying._name;
                    window.LbOrigin.Content = f.AircraftFlying.Origin;
                    window.LbAltitude.Content = f.AircraftFlying.Altitude;
                    window.LbSpeed.Content = f.AircraftFlying.Speed;
                    window.LbDestination.Content = f.AircraftFlying.Destination;
                    foreach (Airport a in MainWindow.Sys.Airports)
                    {
                        window.CbDestination.Items.Add(a.Name);
                    }
                }
            }
            if (window.ShowDialog() == true)
            {
                foreach (Flight f in MainWindow.Sys.Flights)
                {
                    Aircraft aircraft = sender as Aircraft;
                    if (aircraft != null && f.AircraftFlying.IdNumber == aircraft.IdNumber)
                    {
                        f.AircraftFlying.Destination = window.NewDestination.Name;
                        DoubleAnimation animX = new DoubleAnimation(window.NewDestination.Coordinates.X, TimeSpan.FromSeconds(20));
                        Storyboard.SetTarget(animX, f.AircraftFlying);
                        Storyboard.SetTargetProperty(animX, new PropertyPath("(Canvas.Left)"));
                        f.FlightStory.Children.Add(animX);

                        DoubleAnimation animY = new DoubleAnimation(window.NewDestination.Coordinates.Y, TimeSpan.FromSeconds(20));
                        Storyboard.SetTarget(animY, f.AircraftFlying);
                        Storyboard.SetTargetProperty(animY, new PropertyPath("(Canvas.Top)"));
                        f.FlightStory.Children.Add(animY);
                        f.FlightStory.Begin(); 
                    }
                }
            }
            foreach (Flight f in MainWindow.Sys.Flights)
            {
                f.FlightStory.Resume();
            }
        }

        public override void MapObject_OnMouseEnter(object sender, MouseEventArgs e)
        {
            if (Name != null)
            {
                NameLabel.Content = IdNumber;
            }
            NameLabel.Visibility = Visibility.Visible;
            

        }

        public override void ImageMapObject_OnMouseLeave(object sender, MouseEventArgs e)
        {
            NameLabel.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}
