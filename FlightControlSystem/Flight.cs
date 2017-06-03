using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading;
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
        public Storyboard FlightStory { get; set; }
        public Canvas c { get; set; }

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
            this.c = c;
            AircraftFlying.RenderMapObject(c);

            /*double rotationAngle = 0; //Math.Atan((dest.Coordinates.Y - start.Coordinates.Y) / (dest.Coordinates.X - start.Coordinates.X)) * 100 + 90;

            RotateTransform rt = new RotateTransform() { Angle = rotationAngle };
            AircraftFlying.RenderTransformOrigin = new Point(.5, .5);
            AircraftFlying.RenderTransform = rt;*/

            FlightStory = new Storyboard();
            FlightStory.Duration = TimeSpan.FromSeconds(20);


            DoubleAnimation animX = new DoubleAnimation(dest.Coordinates.X, TimeSpan.FromSeconds(20));
            Storyboard.SetTarget(animX, AircraftFlying);
            Storyboard.SetTargetProperty(animX, new PropertyPath("(Canvas.Left)"));
            FlightStory.Children.Add(animX);

            DoubleAnimation animY = new DoubleAnimation(dest.Coordinates.Y, TimeSpan.FromSeconds(20));
            Storyboard.SetTarget(animY, AircraftFlying);
            Storyboard.SetTargetProperty(animY, new PropertyPath("(Canvas.Top)"));
            FlightStory.Children.Add(animY);
            FlightStory.CurrentTimeInvalidated += FlightStory_CurrentTimeInvalidated;
            animY.Completed += (s, e) =>
            {
                foreach (Flight f in MainWindow.sys.Flights)
                {
                    if (f.AircraftFlying.Name == AircraftFlying.Name)
                    {
                        c.Children.Remove(AircraftFlying);
                        MainWindow.sys.Flights.Remove(this);
                        break;
                    }
                }
            };
            
            FlightStory.Begin();

        }

        private void FlightStory_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            Clock clock = (Clock)sender;
            var distAirportX = Math.Abs(Canvas.GetLeft(AircraftFlying) - Canvas.GetLeft(DestinationAirport));
            var distAirportY = Math.Abs(Canvas.GetTop(AircraftFlying) - Canvas.GetTop(DestinationAirport));
            if(distAirportX > 10 && distAirportY > 10)
            {
                if (clock.CurrentTime != null && clock.CurrentTime.Value.Seconds != 0)
                {
                    foreach (Flight f in MainWindow.sys.Flights)
                    {
                        if (!AircraftFlying.Equals(f.AircraftFlying))
                        {
                            var distanceX = Math.Abs(Canvas.GetLeft(AircraftFlying) - Canvas.GetLeft(f.AircraftFlying));
                            var distanceY = Math.Abs(Canvas.GetTop(AircraftFlying) - Canvas.GetTop(f.AircraftFlying));
                            if (distanceX < 6 && distanceY < 6)
                            {
                                f.FlightStory.Stop();
                                foreach (Flight ff in MainWindow.sys.Flights)
                                {
                                    if (ff.AircraftFlying.Name == f.AircraftFlying.Name)
                                    {
                                        c.Children.Remove(f.AircraftFlying);
                                        MainWindow.sys.Flights.Remove(f);
                                        break;
                                    }
                                }
                                FlightStory.Stop();
                                foreach (Flight ff in MainWindow.sys.Flights)
                                {
                                    if (ff.AircraftFlying.Name == AircraftFlying.Name)
                                    {
                                        c.Children.Remove(AircraftFlying);
                                        MainWindow.sys.Flights.Remove(this);
                                        break;
                                    }
                                }
                                return;
                            }
                        }
                    }
                }
            }       
        }

        #endregion
    }
}
