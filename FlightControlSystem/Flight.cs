using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FlightControlSystem
{
    public class Flight
    {
        #region Properties

        public Aircraft AircraftFlying { get; private set; }
        public Airport StartAirport { get; private set; }
        public Airport DestinationAirport { get; set; }

        #endregion

        #region Constructors

        private Flight()
        {
            
        }

        public Flight(Aircraft a, Airport start, Airport dest, Canvas c)
        {
            AircraftFlying = a;
            StartAirport = start;
            DestinationAirport = dest;
            AircraftFlying.RenderMapObject(c);

            double rotationAngle = 0; //Math.Atan((dest.Coordinates.Y - start.Coordinates.Y) / (dest.Coordinates.X - start.Coordinates.X)) * 100 + 90;

            RotateTransform rt = new RotateTransform() { Angle = rotationAngle };
            AircraftFlying.RenderTransformOrigin = new Point(.5, .5);
            AircraftFlying.RenderTransform = rt;
            DoubleAnimation animX = new DoubleAnimation(dest.Coordinates.X, TimeSpan.FromSeconds(20));
            DoubleAnimation animY = new DoubleAnimation(dest.Coordinates.Y, TimeSpan.FromSeconds(20));
            
            animY.Completed += (s, e) =>
            {
                foreach (Flight f in MainWindow.sys.Flights)
                {
                    if (f.AircraftFlying.Name == this.AircraftFlying.Name)
                    {
                        c.Children.Remove(this.AircraftFlying);
                        MainWindow.sys.Flights.Remove(this);
                        break;
                    }
                }
            };

            AircraftFlying.BeginAnimation(Canvas.LeftProperty, animX);
            AircraftFlying.BeginAnimation(Canvas.TopProperty, animY);
        }

        #endregion
    }
}
