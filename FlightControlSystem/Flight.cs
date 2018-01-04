using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using FlightControlSystem.Annotations;

namespace FlightControlSystem
{
    public class Flight : INotifyPropertyChanged
    {
        #region Properties


        private Airport _destinationAirport;
        private Storyboard _flightStory;
        private DateTime _dateTime;
        private Canvas _c;

        public Aircraft AircraftFlying { get; }
        public Airport StartAirport { get; private set; }
        public Airport DestinationAirport
        {
            get
            {
                return _destinationAirport;
            }
            set
            {
                if (!Equals(value, _destinationAirport))
                {
                    _destinationAirport = value;
                    OnFlightPropertyChanged(nameof(DestinationAirport));
                }
            }
        }
        public Storyboard FlightStory
        {
            get { return _flightStory; }
            set
            {
                _flightStory = value;
                OnFlightPropertyChanged(nameof(_flightStory));
            }
        }
        public Canvas C
        {
            get { return _c; }
            set
            {
                _c = value;
                OnFlightPropertyChanged(nameof(_c));
            }
        }

        public DateTime FlightDateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = DateTime.Now.Date;
            }
        }

        #endregion

        #region Constructors

        private Flight()
        {
            
        }

        public Flight(Aircraft a, Airport start, Airport dest, Canvas c, DateTime d)
        {
            AircraftFlying = a;
            a.Destination = dest.Name;
            a.Origin = start.Name;
            StartAirport = start;
            DestinationAirport = dest;
            C = c;
            FlightDateTime = d;
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
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnFlightPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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