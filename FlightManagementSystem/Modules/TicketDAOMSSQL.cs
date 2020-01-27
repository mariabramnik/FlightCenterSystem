using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagementSystem.Models;

namespace FlightManagementSystem.Modules
{
    class TicketDAOMSSQL : ITicketDAO
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
        public void Add(Ticket ob)
        {
            int id = ob.id;
            Ticket ticket = Get(id);
            if (ticket is null)
            {
                string str = string.Format($"INSERT INTO Tickets VALUES({id},{ob.flightId},{ob.customerId})");
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                throw new TicketAlreadyExistException("this ticket already exist");
            }

        }
        public Ticket Get(int id)
        {
            Ticket ticket = null;
            string str = $"SELECT * FROM Tickets WHERE ID = {id}";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    {
                        ticket = new Ticket
                        {
                            id = (int)reader["ID"],
                            customerId = (int)reader["CUSTOMER_ID"],
                            flightId = (int)reader["FLIGHT_ID"]
     
                        };
                    }
                }
            }
            if (ticket is null)
            {
                throw new TicketNotExistException("This Ticket not found");
            }
            return ticket;
        }

        public List<Ticket> GetAll()
        {
            List<Ticket> ticketsList = new List<Ticket>();
            string str = $"SELECT * FROM Tickets";
            SqlCommand cmd = new SqlCommand(str, con);
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
                    ticketsList.Add(ticket);
                }

            }
            return ticketsList;
        }

        public void Remove(Ticket ob)
        {
            int id = ob.id;
            Ticket ticket = Get(id);
            if (ticket is null)
            {
                throw new TicketAlreadyExistException("This ticketis is already exist");
            }
            string str = string.Format($"DELETE FROM Tickets WHERE ID = {id})");
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
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
        

        public void Update(Ticket ob)
        {
            int id = ob.id;
            Ticket ticket = Get(id);
            if (ticket is null)
            {
                throw new TicketAlreadyExistException("This ticketis is already exist");
            }
            string str = string.Format($"UPDATE Countries SET FLIGHT_ID = {ob.flightId},CUSTOMER_ID = {ob.customerId} WHERE ID = {id}");
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public List<Ticket>GetTicketsByFlight(int id_flight)
        {
            List<Ticket> ticketsListByFlight = new List<Ticket>();
            string str = $"SELECT * FROM Tickets WHERE FLIGHT_ID = {id_flight}";
            SqlCommand cmd = new SqlCommand(str, con);
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
            return ticketsListByFlight;
        }
        public List<Flight>GetFlightsByCustomer(int custId)
        {
            List<Flight> listFlightByCustomer = new List<Flight>();
            string str = "SELECT Flights.ID,Flights.AIRLINECOMPANY_ID," +
                "Flights.ORIGIN_COUNTRY_CODE,Flights.DESTINATION_COUNTRY_CODE,Flights.DEPARTURE_TIME," +
                "Flights.LANDING_TIME,Flights.REMAINING_TICKETS,Flights.FLIGHT_STATUS_ID FROM Tickets JOIN " +
                "Flights ON Tickets.FLIGHT_ID = Flights.Id WHERE CUSTOMER_ID = 2;";
            SqlCommand cmd = new SqlCommand(str, con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Flight flight = new Flight
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
                    listFlightByCustomer.Add(flight);
                }

            }
            return listFlightByCustomer;
        }
        public void RemoveAllFromTickets()
        {
            string str = "delete from Tickets";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public bool IfTableTicketsIsEmpty()
        {
            bool res = false;
            string str = $"SELECT COUNT(*) FROM Tickets";
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
