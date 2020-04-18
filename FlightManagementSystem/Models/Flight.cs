using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Models
{
    public class Flight : IPoco , INotifyPropertyChanged
    {
        public int id { get; set; }
        public int airLineCompanyId { get; set; }
        public int originCountryCode { get; set; }
        public int destinationCountryCode { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime landingTime { get; set; }
        public int remainingTickets { get; set; }
        public int flightStatusId { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            var flight = obj as Flight;
            return !(flight is null) &&
                   id == flight.id ;
                   
        }

        public override int GetHashCode()
        {
            return id;
        }

        public override string ToString()
        {
            return ($"{id} ,{airLineCompanyId} ,{originCountryCode} ," +
                $"{destinationCountryCode} ,{departureTime} ,{landingTime} ,{remainingTickets},{flightStatusId}");
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
