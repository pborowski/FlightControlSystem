using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKontroliLotu
{
    class Flight
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
        public Flight(Aircraft aircraftFlying, Airport startAirport, Airport destinationAirport)
        {
            AircraftFlying = aircraftFlying;
            StartAirport = startAirport;
            DestinationAirport = destinationAirport;
        }
        #endregion
    }
}
