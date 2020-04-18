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
        // static SqlConnection con = new SqlConnection(@"Data Source=BRAMNIK-PC;Initial Catalog=FlightManagementSystem;Integrated Security=True");
        static SqlConnection con = new SqlConnection(@"Server=tcp:mashadb.database.windows.net,1433;Initial Catalog = flightSystem; Persist Security Info=False;User ID = mashadb; Password=288401Riga; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;");
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
        public int Add(Ticket ob)
        {     
            int res = 0;
            Ticket ticket = GetTicketByAllParametrs(ob.flightId,ob.customerId);
            if (ticket is null)
            {
                SQLConnectionOpen();
                string str = string.Format($"INSERT INTO Tickets VALUES({ob.flightId},{ob.customerId});SELECT SCOPE_IDENTITY()");
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    res = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            else
            {
                throw new TicketAlreadyExistException("this ticket already exist");
            }
            SQLConnectionClose();
             return res;
        }

        public Ticket Get(int id)
        {
            SQLConnectionOpen();
            Ticket ticket = null;
            string str = $"SELECT * FROM Tickets WHERE ID = {id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
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
                SQLConnectionClose();
                throw new TicketNotExistException("This Ticket not found");
            }
            SQLConnectionClose();
            return ticket;
        }
        public Ticket GetTicketByAllParametrs(int flightId,int customerId)
        {
            SQLConnectionOpen();
            Ticket tick = null;
            string str = $"SELECT * FROM Tickets WHERE FLIGHT_ID = {flightId} AND CUSTOMER_ID = {customerId}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();                      
                        tick = new Ticket
                        {
                            id = (int)reader["ID"],
                            customerId = (int)reader["CUSTOMER_ID"],
                            flightId = (int)reader["FLIGHT_ID"]

                        };                      
                    }
                }
            }
            SQLConnectionClose();
            return tick;
        }
        public IList<Ticket> GetAll()
        {
            SQLConnectionOpen();
            List<Ticket> ticketsList = new List<Ticket>();
            string str = $"SELECT * FROM Tickets";
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
                        ticketsList.Add(ticket);
                    }
                }
            }
            SQLConnectionClose();
            return ticketsList;
        }

        public void Remove(Ticket ob)
        {
            Ticket ticket = Get(ob.id);
            if (ticket is null)
            {
                throw new TicketNotExistException("This ticketis is not exist");
            }
            SQLConnectionOpen();
            string str = $"DELETE FROM Tickets WHERE ID = {ob.id}";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
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
        

        public void Update(Ticket ob)
        {
            int id = ob.id;
            Ticket ticket = Get(id);
            if (ticket is null)
            {
                throw new TicketAlreadyExistException("This ticketis is already exist");
            }
            SQLConnectionOpen();
            string str = string.Format($"UPDATE Countries SET FLIGHT_ID = {ob.flightId},CUSTOMER_ID = {ob.customerId} WHERE ID = {id}");
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }

        public List<Ticket>GetTicketsByFlight(int id_flight)
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
        public List<Ticket>GetAllMyTickets(int custId)
        {
            SQLConnectionOpen();
            List<Ticket> ticketsList = new List<Ticket>();
            string str = $"SELECT * FROM Tickets WHERE CUSTOMER_ID = {custId}";
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
                        ticketsList.Add(ticket);
                    }
                }
            }
            SQLConnectionClose();
            return ticketsList;
        }

        public List<Flight>GetFlightsByCustomer(int custId)
        {
            SQLConnectionOpen();
            List<Flight> listFlightByCustomer = new List<Flight>();
            string str = "SELECT Flights.ID,Flights.AIRLINECOMPANY_ID," +
                "Flights.ORIGIN_COUNTRY_CODE,Flights.DESTINATION_COUNTRY_CODE,Flights.DEPARTURE_TIME," +
                "Flights.LANDING_TIME,Flights.REMAINING_TICKETS,Flights.FLIGHT_STATUS_ID FROM Tickets JOIN " +
                "Flights ON Tickets.FLIGHT_ID = Flights.Id WHERE CUSTOMER_ID = 2;";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
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
            }
            SQLConnectionClose();
            return listFlightByCustomer;
        }
        public void RemoveAllFromTickets()
        {
            SQLConnectionOpen();
            string str = "delete from Tickets";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }
        public void RemoveAllFromTickets_History()
        {
            SQLConnectionOpen();
            string str = "delete from Tickets_History";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }

        public bool IfTableTicketsIsEmpty()
        {
            SQLConnectionOpen();
            bool res = false;
            string str = $"SELECT COUNT(*) FROM Tickets";
            SqlCommand cmd = new SqlCommand(str, con);
            int num = (int)cmd.ExecuteScalar();
            if (num == 0)
            {
                res = true;
            }
            SQLConnectionClose();
            return res;
        }
        public bool IfTableTickets_HistoryIsEmpty()
        {
            SQLConnectionOpen();
            bool res = false;
            string str = $"SELECT COUNT(*) FROM Tickets_History";
            SqlCommand cmd = new SqlCommand(str, con);
            int num = (int)cmd.ExecuteScalar();
            if (num == 0)
            {
                res = true;
            }
            SQLConnectionClose();
            return res;
        }

        public void InsertTicketToTicketHistory(Ticket ticket)
        {
            SQLConnectionOpen();
            string str = $"INSERT INTO Tickets_History VALUES({ticket.id},{ticket.flightId},{ticket.customerId})";
            using (SqlCommand cmd = new SqlCommand(str, con))
            {
                cmd.ExecuteNonQuery();
            }
            SQLConnectionClose();
        }
        public List<Ticket> GetTicketsByAirLineCompany(AirLineCompany comp)
        {
            SQLConnectionOpen();
            List<Ticket> ticketsListByAirLine = new List<Ticket>();
            string str = $"SELECT Tickets.ID,Tickets.FLIGHT_ID,Tickets.CUSTOMER_ID FROM Tickets JOIN Flights ON Tickets.FLIGHT_ID = Flights.ID WHERE Flights.AIRLINECOMPANY_ID  = {comp.id}";
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
                        ticketsListByAirLine.Add(ticket);
                    }
                }
            }
            SQLConnectionClose();
            return ticketsListByAirLine;
        }
        public List<Ticket>GetAllTicketsFromTickets_HistoryByCustomer(Customer customer)
        {
            SQLConnectionOpen();
            List<Ticket> ticketsList = new List<Ticket>();
            string str = $"SELECT * FROM Tickets_History WHERE CUSTOMER_ID = {customer.id}";
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
                        ticketsList.Add(ticket);
                    }
                }
            }
            SQLConnectionClose();
            return ticketsList;
        }
    }
}
