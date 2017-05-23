using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FlightControlSystem
{
    public class SystemObject
    {
        #region Properties

        private static SystemObject _sys; // singleton object is stored here
        public List<Flight> Flights { get; set; }
        public List<Airport> Airports { get; set; }
        public Canvas c;

        #endregion

        #region Constructors

        // private constructor prevents from creating instance
        // of this class outside
        private SystemObject(Canvas c)
        {
            this.c = c;
            Flights = new List<Flight>();
            Airports = new List<Airport>();
            Airports.Add(new Airport("Bialystok", new Point(450, 120)));
            Airports.Add(new Airport("Warszawa", new Point(350, 180)));
            foreach (Airport a in Airports)
            {
                a.RenderMapObject(c);
            }
        }

        // pseudo constructor - publicly visuble static method 
        // used to create instace of this class unless any instance
        // was previously created
        public static SystemObject CreateSystem(Canvas c)
        {
            // if there isn't any existing instance of this class, create it
            if (_sys == null)
            {
                _sys = new SystemObject(c);
            }
            // and return
            return _sys;
        }

        #endregion

        #region Methods

        public void createFlight(Airport origin, Airport destination)
        {
            Aircraft a = new Aircraft("Tupolew", origin.Coordinates, AircraftType.Plane);
            Flight f = new Flight(a, origin, destination, c);
            Flights.Add(f);

        }

        public void generateRandomFlights()
        {

        }

        public void changeFlightDestination()
        {
            /*
             * implement changing destination
             */
        }

        public void collisionCheck()
        {
            /*
             * implement checking for collisions
             */
        }

        #endregion
    }
}
