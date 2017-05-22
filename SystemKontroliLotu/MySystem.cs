using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SystemKontroliLotu
{
    class MySystem
    {
        #region Properties
        private static MySystem _sys; //tutaj przechowujemy utworzony ewentualnie obiekt
        public List<Flight> Flights { get; set; }
        public List<Airport> Airports { get; set; }
        #endregion

        #region Constructors
        //kontruktor musi byc prywatny lub protected
        //aby uniemożliwić utworzenie obiektu
        //za pomocą operatora new
        private MySystem() { }
        #endregion

        #region Methods
        //publiczna metoda statyczna za pomocą której
        //otzymamy referencję do obiektu
        public static MySystem CreateSystem()
        {
            //sprawdzamy czy już utworzyliśmy instancję klasy
            if (_sys == null)
            {
                //jeśli nie to ją tworzymy
                _sys = new MySystem();
            }
            //zwracamy instancję obiektu zapisanego
            //w stacznym polu naszej klasy
            return _sys;
        }

        public void generateRandomFlights(Canvas c )
        {
            Airport a = new Airport();
            a.RenderMapObject(new Point(110, 140), c);
            Airports.Add(a);

        }
        #endregion
    }
}
