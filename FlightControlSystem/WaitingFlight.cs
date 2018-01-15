using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using FlightControlSystem.Annotations;

namespace FlightControlSystem
{
    public class WaitingFlight 

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
                }
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

        private WaitingFlight()
        {

        }

        public WaitingFlight(Aircraft a, Airport start, Airport dest, Canvas c, DateTime d)
        {
            AircraftFlying = a;
            a.Destination = dest.Name;
            a.Origin = start.Name;
            StartAirport = start;
            DestinationAirport = dest;
            FlightDateTime = d;

        }
        #endregion
    }
}