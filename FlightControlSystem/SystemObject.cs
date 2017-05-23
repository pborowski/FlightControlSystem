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
            Airports = new List<Airport>
            {
                new Airport("Białystok", new Point(450, 120)),
                new Airport("Lublin", new Point(450, 280)),
                new Airport("Rzeszów", new Point(410, 390)),
                new Airport("Warszawa", new Point(350, 180)),
                new Airport("Gdańsk", new Point(180, 20)),
                new Airport("Bydgoszcz", new Point(210, 120)),
                new Airport("Szczecin", new Point(50, 70)),
                new Airport("Gorzów_Wielkopolski", new Point(35, 200)),
                new Airport("Wroclaw", new Point(100, 290)),
                new Airport("Poznań", new Point(135, 190)),
                new Airport("Łódź", new Point(250, 260)),
                new Airport("Olsztyn", new Point(330, 60)),
                new Airport("Opole", new Point(175, 325)),
                new Airport("Katowice", new Point(230, 370)),
                new Airport("Krakow", new Point(310, 410)),
                new Airport("Kielce", new Point(335, 320))
            };
            foreach (Airport a in Airports)
            {
                a.RenderMapObject(c);
                for (int i = 0; i < 1; i++) //jeden random z każdego lotniska
                {
                    int r = rnd.Next(Airports.Count);
                    AircraftType t;
                    switch (r % 4)
                    {
                        case 0:
                            t = AircraftType.Plane;
                            break;
                        case 1:
                            t = AircraftType.Balloon;
                            break;
                        case 2:
                            t = AircraftType.Glider;
                            break;
                        case 3:
                            t = AircraftType.Helicopter;
                            break;
                        default:
                            t = AircraftType.Plane;
                            break;
                    }
                    CreateFlight(a, Airports[r], t);
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

        public void CreateFlight(Airport origin, Airport destination, AircraftType type)
        {
            int id = Flights.Count + 1;
            Aircraft a = new Aircraft("Tupolew", origin.Coordinates, type,id);
            Flight f = new Flight(a, origin, destination, C);
            Flights.Add(f);

        }

        public void GenerateRandomFlight(AircraftType t)
        {
            CreateFlight(Airports[rnd.Next(Airports.Count)], Airports[rnd.Next(Airports.Count)], t);
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
