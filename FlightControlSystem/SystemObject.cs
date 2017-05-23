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
        private static Random rnd;
        public List<Flight> Flights { get; set; }
        public List<Airport> Airports { get; set; }
        public Canvas C;

        #endregion

        #region Constructors

        // private constructor prevents from creating instance
        // of this class outside
        private SystemObject(Canvas c)
        {
            rnd = new Random();
            this.C = c;
            Flights = new List<Flight>();
            Airports = new List<Airport>();
            Airports.Add(new Airport("Białystok", new Point(450, 120)));
            Airports.Add(new Airport("Lublin", new Point(450, 280)));
            Airports.Add(new Airport("Rzeszów", new Point(410, 390)));
            Airports.Add(new Airport("Warszawa", new Point(350, 180)));
            Airports.Add(new Airport("Gdańsk", new Point(180, 20)));
            Airports.Add(new Airport("Bydgoszcz", new Point(210, 120)));
            Airports.Add(new Airport("Szczecin", new Point(50, 70)));
            Airports.Add(new Airport("Gorzów_Wielkopolski", new Point(35, 200)));
            Airports.Add(new Airport("Wroclaw", new Point(100, 290)));
            Airports.Add(new Airport("Poznań", new Point(135, 190)));
            Airports.Add(new Airport("Łódź", new Point(250, 260)));
            Airports.Add(new Airport("Olsztyn", new Point(330, 60)));
            Airports.Add(new Airport("Opole", new Point(175, 325)));
            Airports.Add(new Airport("Katowice", new Point(230, 370)));
            Airports.Add(new Airport("Krakow", new Point(310, 410)));
            Airports.Add(new Airport("Kielce", new Point(335, 320)));
            foreach (Airport a in Airports)
            {
                a.RenderMapObject(c);
                for (int i = 0; i < 1; i++) //jeden random z każdego lotniska
                {
                    int r = rnd.Next(Airports.Count);
                    CreateFlight(a,Airports[r]);
                }
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

        public void CreateFlight(Airport origin, Airport destination)
        {
            Aircraft a = new Aircraft("Tupolew", origin.Coordinates, AircraftType.Plane);
            Flight f = new Flight(a, origin, destination, C);
            Flights.Add(f);

        }

        public void GenerateRandomFlights()
        {

        }

        public void ChangeFlightDestination()
        {
            /*
             * implement changing destination
             */
        }

        public void CollisionCheck()
        {
            /*
             * implement checking for collisions
             */
        }

        #endregion
    }
}
