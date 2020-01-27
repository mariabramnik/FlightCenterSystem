using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Modules
{
    class FlightDAOMSSQL : IFlightDAO
    {
        static SqlConnection con = new SqlConnection(@"Data Source=BRAMNIK-PC;Initial Catalog=FlightManagementSystem;Integrated Security=True");
        public void SQLConnectionOpen()
        {
            con.Open();
        }
        public void SQLConnectionClose()
        {
            con.Close();
        }
        public int Add(Flight ob)
        {
            int res = 0;
            int id = ob.id;
            Flight fl = GetFlightByAllParamets(ob.airLineCompanyId,ob.originCountryCode,
                ob.destinationCountryCode,ob.departureTime,ob.landingTime);
            if (fl is null)
            {
                string str = $"INSERT INTO Flights VALUES({id},{ob.airLineCompanyId},{ob.originCountryCode}," +
                    $"{ob.destinationCountryCode},'{ob.departureTime}','{ob.landingTime}',{ob.remainingTickets},{ob.flightStatusId});SELECT SCOPE_IDENTITY()";
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    res = (int)cmd.ExecuteScalar();
                }
            }
            else
            {
                throw new FlightAlreadyExistException("This Flight already exist");
            }
            return res;
        }

        private Flight GetFlightByAllParamets(int airLineCompanyId, int originCountryCode, int destinationCountryCode, DateTime departureTime, DateTime landingTime)
        {
            throw new NotImplementedException();
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight,int> vacFlights = new Dictionary<Flight, int>();
            string str = $"SELECT * FROM Flights WHERE REMAINING_TICKETS > 0 AND FLIGHT_STATUS_ID = 1";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Flight fl = new Flight
                    {
                        id = (int)reader["ID"],                             
                        airLineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                        originCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                        destinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                        departureTime = (DateTime)reader["DEPARTURE_TIME"],
                        landingTime = (DateTime)reader["LANDING_TIME"],
                        remainingTickets = (int)reader["REMAINING_TICKETS"],
                        flightStatusId = (int)reader["FLIGHT_STATUS_ID"],
                    };
                    vacFlights.Add(fl, fl.id);
                }

            }
            return vacFlights;
        }

        public Flight GetFlightById(int id)
        {
            Flight fl = null;
            string str = $"SELECT * FROM Flights WHERE ID = {id}";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            { 
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        fl = new Flight
                        {
                            id = (int)reader["ID"],
                            airLineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            originCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            destinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            departureTime = (DateTime)reader["DEPARTURE_TIME"],
                            landingTime = (DateTime)reader["LANDING_TIME"],
                            remainingTickets = (int)reader["REMAINING_TICKETS"],
                            flightStatusId = (int)reader["FLIGHT_STATUS_ID"],
                        };
                    }
                }          
            }

            return fl;
        }

        public List<Flight> GetFlightsByCustomer(Customer customer)
        {
            int custId = customer.id;
            List<Flight> flByCustomer = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT Flights.ID,Flights.AIRLINECOMPANY_ID, Flights.ORIGIN_COUNTRY_CODE," +
                $"Flights.DESTINATION_COUNTRY_CODE,Flights.DEPARTURE_TIME,Flights.LANDING_TIME,Flights.REMAINING_TICKETS," +
                $"Flights.FLIGHT_STATUS_ID FROM  Tickets JOIN Flights ON Tickets.FLIGHT_ID = Flights.ID WHERE Tickets.CUSTOMER_ID = {custId}";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        fl = new Flight
                        {
                            id = (int)reader["ID"],
                            airLineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            originCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            destinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            departureTime = (DateTime)reader["DEPARTURE_TIME"],
                            landingTime = (DateTime)reader["LANDING_TIME"],
                            remainingTickets = (int)reader["REMAINING_TICKETS"],
                            flightStatusId = (int)reader["FLIGHT_STATUS_ID"],
                        };
                        flByCustomer.Add(fl);
                    }
                }
            }
            return flByCustomer;
        }

        public List<Flight> GetFlightsByDepartureTime(DateTime datetime)
        {
            //SELECT* FROM Flights WHERE DEPARTURE_TIME = '2020-08-23 10:20.000';
            List<Flight> flByDepartureTime = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT* FROM Flights WHERE DEPARTURE_TIME = '{datetime}' ";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        fl = new Flight
                        {
                            id = (int)reader["ID"],
                            airLineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            originCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            destinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            departureTime = (DateTime)reader["DEPARTURE_TIME"],
                            landingTime = (DateTime)reader["LANDING_TIME"],
                            remainingTickets = (int)reader["REMAINING_TICKETS"],
                            flightStatusId = (int)reader["FLIGHT_STATUS_ID"],
                        };
                        flByDepartureTime.Add(fl);
                    }
                }
            }
            return flByDepartureTime;
        }

        public List<Flight> GetFlightsByDestinationCountry(Country country)
        {
            int countryId = country.id;
            List<Flight> flByDestCountry = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT* FROM Flights WHERE DESTINATION_COUNTRY_CODE = {countryId}";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        fl = new Flight
                        {
                            id = (int)reader["ID"],
                            airLineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            originCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            destinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            departureTime = (DateTime)reader["DEPARTURE_TIME"],
                            landingTime = (DateTime)reader["LANDING_TIME"],
                            remainingTickets = (int)reader["REMAINING_TICKETS"],
                            flightStatusId = (int)reader["FLIGHT_STATUS_ID"],
                        };
                        flByDestCountry.Add(fl);
                    }
                }
            }
            return flByDestCountry;
        }

        public List<Flight> GetFlightsByLandingTime(DateTime datetime)
        {
            List<Flight> flByLandingTime = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT* FROM Flights WHERE LANDING_TIME = '{datetime}' ";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        fl = new Flight
                        {
                            id = (int)reader["ID"],
                            airLineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            originCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            destinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            departureTime = (DateTime)reader["DEPARTURE_TIME"],
                            landingTime = (DateTime)reader["LANDING_TIME"],
                            remainingTickets = (int)reader["REMAINING_TICKETS"],
                            flightStatusId = (int)reader["FLIGHT_STATUS_ID"],
                        };
                        flByLandingTime.Add(fl);
                    }
                }
            }
            return flByLandingTime;
        }

        public List<Flight> GetFlightsByOriginCountryCode(Country country)
        {
            int countryId = country.id;
            List<Flight> flByOriginCountry = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT* FROM Flights WHERE ORIGIN_COUNTRY_CODE = {countryId}";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        fl = new Flight
                        {
                            id = (int)reader["ID"],
                            airLineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            originCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            destinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            departureTime = (DateTime)reader["DEPARTURE_TIME"],
                            landingTime = (DateTime)reader["LANDING_TIME"],
                            remainingTickets = (int)reader["REMAINING_TICKETS"],
                            flightStatusId = (int)reader["FLIGHT_STATUS_ID"],
                        };
                        flByOriginCountry.Add(fl);
                    }
                }
            }
            return flByOriginCountry;
        }

        public Flight Get(int id)
        {
            Flight fl = GetFlightById(id);
            return fl;
        }

        public List<Flight> GetAll()
        {
            List<Flight> allFlights = new List<Flight>();
            string str = $"SELECT * FROM Flights ";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Flight fl = new Flight
                    {
                        id = (int)reader["ID"],
                        airLineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                        originCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                        destinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                        departureTime = (DateTime)reader["DEPARTURE_TIME"],
                        landingTime = (DateTime)reader["LANDING_TIME"],
                        remainingTickets = (int)reader["REMAINING_TICKETS"],
                        flightStatusId = (int)reader["FLIGHT_STATUS_ID"],
                    };
                    allFlights.Add(fl);
                }
            }
            return allFlights;
        }
    

        public void Remove(Flight ob)
        {
         Flight fl = GetFlightById(ob.id);
         if(fl is null)
         {
                throw new FlightNotExistException("this flight not exist in system");
         }
        int id = ob.id;
        string str = string.Format($"DELETE FROM Flights WHERE ID = {id})");
        using (SqlCommand cmd = new SqlCommand(str, con))
        {
            cmd.ExecuteNonQuery();
        }
        
    }

        public void Update(Flight ob)
        {
            Flight fl = GetFlightById(ob.id);
            if (fl is null)
            {
                throw new FlightNotExistException("this flight not exist in system");
            }
            int id = ob.id;
            string str = string.Format($"UPDATE Flights SET AIRLINECOMPANY_ID = {ob.airLineCompanyId},ORIGIN_COUNTRY_CODE = {ob.originCountryCode}, " +
                $"DESTINATION_COUNTRY_CODE = {ob.destinationCountryCode}, DEPARTURE_TIME = '{ob.departureTime}', LANDING_TIME = '{ob.landingTime}', " +
                $"REMAINING_TIME = {ob.remainingTickets}, FLIGHT_STATUS_ID = {ob.flightStatusId} WHERE ID = {id}");
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }

        }
        public void RemoveAllFromFlights()
        {
            string str = "delete from Flights";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public bool IfTableFlightsIsEmpty()
        {
            bool res = false;
            string str = $"SELECT COUNT(*) FROM Flights";
            SqlCommand cmd = new SqlCommand(str, con);
            int num = (int)cmd.ExecuteScalar();
            if (num == 0)
            {
                res = true;
            }
            return res;
        }
    }

}
