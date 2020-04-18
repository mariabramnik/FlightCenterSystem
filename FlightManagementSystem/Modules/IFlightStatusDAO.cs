using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public interface IFlightStatusDAO : IBasic<FlightStatus>
    {
        void RemoveAllFromFlightStatus();
        bool IfTableFlightStatusIsEmpty();
        FlightStatus Get(int id);
        FlightStatus GetFlightStatusByName(string statusName);

    }
}
