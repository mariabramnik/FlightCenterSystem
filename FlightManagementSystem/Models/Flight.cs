using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Models
{
    public class Flight : IPoco
    {
        public int id;
        public int airLineCompanyId;
        public int originCountryCode;
        public int destinationCountryCode;
        public DateTime departureTime;
        public DateTime landingTime;
        public int remainingTickets;
        public int flightStatusId;

        public Flight(int id, int airLineCompanyId, int originCountryCode, int destinationCountryCode, 
            DateTime departureTime, DateTime landingTime, int remainingTickets)
        {
            this.id = id;
            this.airLineCompanyId = airLineCompanyId;
            this.originCountryCode = originCountryCode;
            this.destinationCountryCode = destinationCountryCode;
            this.departureTime = departureTime;
            this.landingTime = landingTime;
            this.remainingTickets = remainingTickets;
            this.flightStatusId = 1;

        }
        public Flight() { }

        public override bool Equals(object obj)
        {
            var flight = obj as Flight;
            return !(flight is null) &&
                   id == flight.id ;
                   
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public override string ToString()
        {
            return ($"{id} ,{airLineCompanyId} ,{originCountryCode} ," +
                $"{destinationCountryCode} ,{departureTime} ,{landingTime} ,{remainingTickets}");
        }
        public static bool operator ==(Flight f1, Flight f2)
        {
            bool res = false;
            if (f1.id == f2.id )             
            {
                res = true;
            }
            return res;
        }
        public static bool operator !=(Flight f1, Flight f2)
        {
            return !(f1 == f2);
        }
    }
}
