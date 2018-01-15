using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FlightControlSystem
{
    public class SystemObject
    {
        #region Properties

        private static SystemObject _sys; // singleton object is stored here
        private static Random _rnd;
        public List<Flight> Flights { get; set; }
        public ObservableCollection<Flight> AllFlightsList { get; set; }
        public List<Airport> Airports { get; set; }
        public int Max;
        public Canvas Canv;
        const double IntervalSecond = 1000; // milliseconds to one second
        public ObservableCollection<FlightWaiting> FlightsWaitingList;

        Timer checkForTime = new Timer(IntervalSecond);
       


        #endregion

        #region Constructors

        // private constructor prevents from creating instance
        // of this class outside
        private SystemObject(Canvas canv)
        {
            checkForTime.Elapsed += new ElapsedEventHandler(checkForTime_Elapsed);
            checkForTime.Enabled = true;
            Max = 16;
            _rnd = new Random();
            Canv = canv;
            FlightsWaitingList = new ObservableCollection<FlightWaiting>();
            Flights = new List<Flight>();
            AllFlightsList = new ObservableCollection<Flight>();
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
                a.RenderMapObject(canv);
                
            }
            foreach (Airport a in Airports)
            {
                for (int i = 0; i < 1; i++) //jeden random z każdego lotniska
                {
                    int r = _rnd.Next(Airports.Count);
                    while (a.Name == Airports[r].Name)
                    {
                        r = _rnd.Next(Airports.Count);
                    }
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


        private void checkForTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {

                foreach (FlightWaiting f in FlightsWaitingList)
                {
                    if (DateTime.Now.ToString("h:mm:ss") == f.DateTime)
                    {
                        MessageBox.Show("Startuje zaplanowany " + f.Flight.AircraftFlying.Type + " start: "+f.Flight.StartAirport.Name+" cel: "+f.Flight.DestinationAirport.Name);
                        //AllFlightsList.Add(f.Flight);
                        try
                        {
                            CreateFlight(f.Flight.StartAirport, f.Flight.DestinationAirport, f.Flight.AircraftFlying.Type);
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message);
                        }
                        FlightsWaitingList.Remove(f);
                        //FlightsWaitingList.CollectionChanged +=  delegate(object o, NotifyCollectionChangedEventArgs args) {   };

                    }
                }

            });
           
        }

        // pseudo constructor - publicly visuble static method 
        // used to create instace of this class unless any instance
        // was previously created
        public static SystemObject CreateSystem(Canvas c)
        {
            // if there isn't any existing instance of this class, create it
            return _sys ?? (_sys = new SystemObject(c));
            // and return
        }

        #endregion

        #region Methods

        

        private int NextAvailiableId()
        {
            if (_sys == null)
            {
                // MessageBox.Show("Sys == null więc - Return" + (Flights.Count + 1).ToString());
                return Flights.Count + 1;
            }
            
            return ++Max;
        }

        public void CreateFlight(Airport origin, Airport destination, AircraftType type)
        {
            int id = NextAvailiableId();
            
            string name = type.ToString() + id;
            Aircraft a = new Aircraft(name, origin.Coordinates, type,id);
            Flight f = new Flight(a, origin, destination, Canv, DateTime.UtcNow);
            Flights.Add(f);
            AllFlightsList.Add(f);

        }

        public void GenerateRandomWaitingFlight(AircraftType t, string d)
        {
            FlightWaiting f;
            f = CreateWaitingFlight(Airports[_rnd.Next(Airports.Count)], Airports[_rnd.Next(Airports.Count)], t, d);
            FlightsWaitingList.Add(f);
        }

        public FlightWaiting CreateWaitingFlight(Airport origin, Airport destination, AircraftType type, string time)
        {
            int id = NextAvailiableId();

            string name = type.ToString() + id;
            Aircraft a = new Aircraft(name, origin.Coordinates, type, id);
            WaitingFlight f = new WaitingFlight(a, origin, destination, Canv, DateTime.UtcNow);
            return new FlightWaiting(){DateTime = time, Flight = f};

        }

        public void GenerateRandomFlight(AircraftType t)
        {
            Airport a = Airports[_rnd.Next(Airports.Count)];
            Airport b = Airports[_rnd.Next(Airports.Count)];
            while (a.Name == b.Name)
            {
                b = Airports[_rnd.Next(Airports.Count)];
            }
            CreateFlight(a, b, t);
        }
        #endregion
    }
}
