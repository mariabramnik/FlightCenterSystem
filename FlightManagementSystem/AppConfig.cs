using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem
{
    class AppConfig
    {
        public static int timeOut = 900000;
        public static string flightsystemdb = "Server=tcp:mashadb.database.windows.net,1433;Initial Catalog = flightSystem; Persist Security Info=False;User ID = mashadb; Password=288401Riga; MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";
    }
}
