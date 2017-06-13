using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FlightControlSystem
{
    public class Flight
    {
        #region Properties

        public Aircraft AircraftFlying { get; }
        public Airport StartAirport { get; private set; }
        public Airport DestinationAirport { get; set; }
        public Storyboard FlightStory { get; set; }
        public Canvas C { get; set; }

        #endregion

        #region Constructors

        private Flight()
        {
            
        }

        public Flight(Aircraft a, Airport start, Airport dest, Canvas c)
        {
            AircraftFlying = a;
            a.Destination = dest.Name;
            a.Origin = start.Name;
            StartAirport = start;
            DestinationAirport = dest;
            C = c;
            AircraftFlying.RenderMapObject(c);

            

            FlightStory = new Storyboard {Duration = TimeSpan.FromSeconds(40)};


            DoubleAnimation animX = new DoubleAnimation(dest.Coordinates.X, TimeSpan.FromSeconds(40));
            Storyboard.SetTarget(animX, AircraftFlying);
            Storyboard.SetTargetProperty(animX, new PropertyPath("(Canvas.Left)"));
            FlightStory.Children.Add(animX);

            DoubleAnimation animY = new DoubleAnimation(dest.Coordinates.Y, TimeSpan.FromSeconds(40));
            Storyboard.SetTarget(animY, AircraftFlying);
            Storyboard.SetTargetProperty(animY, new PropertyPath("(Canvas.Top)"));
            FlightStory.Children.Add(animY);
            FlightStory.CurrentTimeInvalidated += FlightStory_CurrentTimeInvalidated;
            animY.Completed += (s, e) =>
            {
                foreach (Flight f in MainWindow.Sys.Flights)
                {
                    if (f.AircraftFlying.Name == AircraftFlying.Name)
                    {
                        c.Children.Remove(AircraftFlying);
                        MainWindow.Sys.Flights.Remove(this);
                        break;
                    }
                }
            };
            
            FlightStory.Begin();

        }
        #endregion

        #region EventHandlers
        private void FlightStory_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            Clock clock = (Clock)sender;
            var distAirportX = Math.Abs(Canvas.GetLeft(AircraftFlying) - Canvas.GetLeft(DestinationAirport));
            var distAirportY = Math.Abs(Canvas.GetTop(AircraftFlying) - Canvas.GetTop(DestinationAirport));
            if (distAirportX > 10 && distAirportY > 10)
            {
                if (clock.CurrentTime != null && clock.CurrentTime.Value.Seconds != 0)
                {
                    foreach (Flight f in MainWindow.Sys.Flights)
                    {
                        if (!AircraftFlying.Equals(f.AircraftFlying))
                        {
                            var distanceX = Math.Abs(Canvas.GetLeft(AircraftFlying) - Canvas.GetLeft(f.AircraftFlying));
                            var distanceY = Math.Abs(Canvas.GetTop(AircraftFlying) - Canvas.GetTop(f.AircraftFlying));
                            if (distanceX < 6 && distanceY < 6)
                            {
                                f.FlightStory.Stop();
                                foreach (Flight ff in MainWindow.Sys.Flights)

                                {
                                    if (ff.AircraftFlying.IdNumber == f.AircraftFlying.IdNumber)
                                    {
                                        C.Children.Remove(f.AircraftFlying);
                                        MainWindow.Sys.Flights.Remove(f);
                                        string collisionMessage = "Kolizja pojazdu: "+ f.AircraftFlying.IdNumber +" i "+ AircraftFlying.IdNumber;
                                        MessageBox.Show(collisionMessage);
                                        break;
                                    }
                                }
                                FlightStory.Stop();
                                foreach (Flight ff in MainWindow.Sys.Flights)
                                {
                                    if (ff.AircraftFlying.IdNumber == AircraftFlying.IdNumber)
                                    {
                                        C.Children.Remove(AircraftFlying);
                                        MainWindow.Sys.Flights.Remove(this);
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