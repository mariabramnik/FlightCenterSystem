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
            if (con.State != System.Data.ConnectionState.Open)
                con.Open();
        }
        public void SQLConnectionClose()
        {
            if (con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }
        public int Add(Flight ob)
        {
            int res = 0;
            Flight fl = GetFlightByAllParametrs(ob);               
            if (fl is null)
            {
                SQLConnectionOpen();
                string str = $"INSERT INTO Flights VALUES({ob.airLineCompanyId},{ob.originCountryCode}," +
                    $"{ob.destinationCountryCode},'{ob.departureTime}','{ob.landingTime}',{ob.remainingTickets},{ob.flightStatusId});SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                   res =  Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            else
            {
                throw new FlightAlreadyExistException("This Flight already exist");
            }
            SQLConnectionClose();
            return res;
        }

        public Flight GetFlightByAllParametrs(Flight flight)
        {
            SQLConnectionOpen();
            Flight fl = null;
            string str = $"SELECT * FROM Flights WHERE AIRLINECOMPANY_ID = {flight.airLineCompanyId} AND " +
                $"ORIGIN_COUNTRY_CODE = {flight.originCountryCode} AND DESTINATION_COUNTRY_CODE = {flight.destinationCountryCode} AND " +
                $"DEPARTURE_TIME = '{flight.departureTime}' AND LANDING_TIME = '{flight.landingTime}'";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
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
            SQLConnectionClose();
            return fl;
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            SQLConnectionOpen();
            Dictionary<Flight,int> vacFlights = new Dictionary<Flight, int>();
            string str = $"SELECT Flights.ID,Flights.AIRLINECOMPANY_ID,Flights.ORIGIN_COUNTRY_CODE, "+
                   " Flights.DESTINATION_COUNTRY_CODE,Flights.DEPARTURE_TIME,Flights.LANDING_TIME," +
                   "Flights.REMAINING_TICKETS,Flights.FLIGHT_STATUS_ID FROM Flights JOIN FlightStatus " +
                   "ON Flights.FLIGHT_STATUS_ID = FlightStatus.ID  WHERE REMAINING_TICKETS > 0 " +
                   "AND FlightStatus.STATUS_NAME = 'panding'; ";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
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
            }
            SQLConnectionClose();
            return vacFlights;
        }

        public Flight GetFlightById(int id)
        {
            SQLConnectionOpen();
            Flight fl = null;
            string str = $"SELECT * FROM Flights WHERE ID = {id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();                       
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
            SQLConnectionClose();
            return fl;
        }

        public List<Flight> GetFlightsByCustomer(Customer customer)
        {
            SQLConnectionOpen();
            List<Flight> flByCustomer = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT Flights.ID,Flights.AIRLINECOMPANY_ID, Flights.ORIGIN_COUNTRY_CODE," +
                $"Flights.DESTINATION_COUNTRY_CODE,Flights.DEPARTURE_TIME,Flights.LANDING_TIME,Flights.REMAINING_TICKETS," +
                $"Flights.FLIGHT_STATUS_ID FROM  Tickets JOIN Flights ON Tickets.FLIGHT_ID = Flights.ID WHERE Tickets.CUSTOMER_ID = {customer.id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                { 
                     while( reader.Read())
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
            SQLConnectionClose();
            return flByCustomer;
        }

        public List<Flight> GetFlightsByDepartureTime(DateTime datetime)
        {
            SQLConnectionOpen();
            //SELECT* FROM Flights WHERE DEPARTURE_TIME = '2020-08-23 10:20.000';
            List<Flight> flByDepartureTime = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT* FROM Flights WHERE DEPARTURE_TIME = '{datetime}'";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {   
                    while(reader.Read())
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
            SQLConnectionClose();
            return flByDepartureTime;
        }

        public List<Flight> GetFlightsByDestinationCountry(Country country)
        {
            SQLConnectionOpen();
            List<Flight> flByDestCountry = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT* FROM Flights WHERE DESTINATION_COUNTRY_CODE = {country.id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                     while(reader.Read())
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
            SQLConnectionClose();
            return flByDestCountry;
        }

        public List<Flight> GetFlightsByLandingTime(DateTime datetime)
        {
            SQLConnectionOpen();
            List<Flight> flByLandingTime = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT* FROM Flights WHERE LANDING_TIME = '{datetime}'";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                     while(reader.Read())
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
            SQLConnectionClose();
            return flByLandingTime;
        }

        public List<Flight> GetFlightsByOriginCountry(Country country)
        {
            SQLConnectionOpen();
            List<Flight> flByOriginCountry = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT* FROM Flights WHERE ORIGIN_COUNTRY_CODE = {country.id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                     while(reader.Read())
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
            SQLConnectionClose();
            return flByOriginCountry;
        }

        public List<Flight> GetFlightsByAirLineCompany(AirLineCompany company)
        {
            SQLConnectionOpen();
            List<Flight> flByOriginCountry = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT* FROM Flights WHERE AIRLINECOMPANY_ID = {company.id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
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
            SQLConnectionClose();
            return flByOriginCountry;
        }

        public Flight Get(int id)
        {
            Flight fl = GetFlightById(id);
            return fl;
        }

        public IList<Flight> GetAll()
        {
            SQLConnectionOpen();
            List<Flight> allFlights = new List<Flight>();
            string str = $"SELECT * FROM Flights ";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
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
            }
            SQLConnectionClose();
            return allFlights;
        }

        public void Remove(Flight ob)
        {
            Flight fl = GetFlightById(ob.id);
            if(fl is null)
            {
                throw new FlightNotExistException("this flight not exist in system");
            }
            List<Ticket> ticketList = GetTicketsByFlight(ob.id);
            foreach(Ticket ticket in ticketList)
            {
                 RemoveTicket(ticket);
            }
            SQLConnectionOpen();
            string str = $"DELETE FROM Flights WHERE ID = {ob.id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }
        public List<Ticket> GetTicketsByFlight(int id_flight)
        {
            SQLConnectionOpen();
            List<Ticket> ticketsListByFlight = new List<Ticket>();
            string str = $"SELECT * FROM Tickets WHERE FLIGHT_ID = {id_flight}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ticket ticket = new Ticket
                        {
                            id = (int)reader["ID"],
                            customerId = (int)reader["CUSTOMER_ID"],
                            flightId = (int)reader["FLIGHT_ID"]
                        };
                        ticketsListByFlight.Add(ticket);
                    }
                }
            }
            SQLConnectionClose();
            return ticketsListByFlight;
        }

        public void Update(Flight ob)
        {
            Flight fl = GetFlightById(ob.id);
            if (fl is null)
            {
                throw new FlightNotExistException("this flight not exist in system");
            }
            SQLConnectionOpen();
            string str = string.Format($"UPDATE Flights SET AIRLINECOMPANY_ID = {ob.airLineCompanyId},ORIGIN_COUNTRY_CODE = {ob.originCountryCode}, " +
                $"DESTINATION_COUNTRY_CODE = {ob.destinationCountryCode}, DEPARTURE_TIME = '{ob.departureTime}', LANDING_TIME = '{ob.landingTime}', " +
                $"REMAINING_TICKETS = {ob.remainingTickets}, FLIGHT_STATUS_ID = {ob.flightStatusId} WHERE ID = {ob.id}");
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }

        public void RemoveAllFromFlights()
        {
            SQLConnectionOpen();
            string str = "delete from Flights";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }
        public void RemoveAllFromFlights_History()
        {
            SQLConnectionOpen();
            string str = "delete from Flights_History";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }

        public bool IfTableFlightsIsEmpty()
        {
            SQLConnectionOpen();
            bool res = false;
            string str = $"SELECT COUNT(*) FROM Flights";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                int num = (int)cmd.ExecuteScalar();
                if (num == 0)
                {
                    res = true;
                }
            }
            SQLConnectionClose();
            return res;
        }
        public bool IfTableFlights_HistoryIsEmpty()
        {
            SQLConnectionOpen();
            bool res = false;
            string str = $"SELECT COUNT(*) FROM Flights_History";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                int num = (int)cmd.ExecuteScalar();
                if (num == 0)
                {
                    res = true;
                }
            }
            SQLConnectionClose();
            return res;
        }

        public List<Flight> SelectElapsedFlightsToHistory()
        {
            SQLConnectionOpen();
            //DateTime dtCurr = DateTime.Now;
            // DateTime dt = dtCurr.AddHours(3.0);
            List<Flight> elapsedFlights = new List<Flight>();
            string str = $"SELECT * FROM Flights WHERE LANDING_TIME BETWEEN (SELECT DATEADD(DAY,-1, GETDATE())) AND (SELECT DATEADD(HOUR,-3, GETDATE()));";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
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
                        elapsedFlights.Add(fl);
                    }
                }
            }
            SQLConnectionClose();
            return elapsedFlights;
        }
        public void InsertElapsedFlightsToHistory(List<Flight> flightList)
        {
            foreach(Flight fl in flightList)
            {
                AddFlightToHistoryTable(fl);
            }
        }

        public void AddFlightToHistoryTable(Flight ob)
        {
            SQLConnectionOpen();
            string str = $"INSERT INTO Flights_History VALUES({ob.id},{ob.airLineCompanyId},{ob.originCountryCode}," +
                    $"{ob.destinationCountryCode},'{ob.departureTime}','{ob.landingTime}')";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
               cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }

        public void DeleteElapsedFlightsFromFlights(List<Flight> flightList)
        {
            foreach (Flight fl in flightList)
            {
                Remove(fl);
            }
        }

        public void RemoveTicket(Ticket ob)
        {
            SQLConnectionOpen();
            string str = $"DELETE FROM Tickets WHERE ID = {ob.id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }

        public List<Flight> SelectAllFromFlights_History()
        {
            SQLConnectionOpen();
            List<Flight> listFlights = new List<Flight>();
            Flight fl = null;
            string str = $"SELECT* FROM Flights_History";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fl = new Flight
                        {
                            id = (int)reader["ID"],
                            airLineCompanyId = (int)reader["AIRLINECOMPANY_ID"],
                            originCountryCode = (int)reader["ORIGIN_COUNTRY_CODE"],
                            destinationCountryCode = (int)reader["DESTINATION_COUNTRY_CODE"],
                            departureTime = (DateTime)reader["DEPARTURE_TIME"],
                            landingTime = (DateTime)reader["LANDING_TIME"],
                            remainingTickets = 0,
                            flightStatusId = 0,
                        };                       
                        listFlights.Add(fl);

                    }
                }
            }
            SQLConnectionClose();
            FlightStatus flStatus = GetFlightStatusByFlightStatusName("landing");
            foreach (Flight flight in listFlights)
            {
                flight.flightStatusId = flStatus.id;
            }           
            return listFlights;
        }

        public FlightStatus GetFlightStatusByFlightStatusName(string statusName)
        {
            SQLConnectionOpen();
            FlightStatus flStatus = null;
            string str = $"SELECT * FROM FlightStatus WHERE STATUS_NAME = '{statusName}'";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    flStatus = new FlightStatus
                    {
                        id = (int)reader["ID"],
                        statusName = (string)reader["STATUS_NAME"]
                    };
                }
            }
            SQLConnectionClose();
            return flStatus;
        }

    }
}
