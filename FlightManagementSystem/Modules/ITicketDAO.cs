using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public interface ITicketDAO : IBasic<Ticket>
    {
        void Add(Ticket ob);
        Ticket Get(int id);
        List<Ticket> GetAll();
        void Remove(Ticket ob);
        void Update(Ticket ob);
        List<Ticket> GetTicketsByFlight(int id_flight);
        List<Flight> GetFlightsByCustomer(int custId);
        void RemoveAllFromTickets();
        bool IfTableTicketsIsEmpty();
        Flight GetFlightById(int id);

    }
}
