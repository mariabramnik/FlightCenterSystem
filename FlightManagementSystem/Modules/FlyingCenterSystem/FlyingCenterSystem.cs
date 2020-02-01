using System.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FlightManagementSystem.Models;
using FlightManagementSystem.Modules.Login;

namespace FlightManagementSystem.Modules.FlyingCenterSystem
{
    public class FlyingCenterSystem
    {
        AnonymousUserFacade _anonymousUserFacade = null;
        LoggedInAdministratorFacade _loggedInAdministratorFacade = null;
        LoggedInAirlineFacade _loggedInAirlineFacade = null;
        LoggedInCustomerFacade _loggedInCustomerFacade = null;
        public static string strTimeout = "";
        private static readonly FlyingCenterSystem instance = new FlyingCenterSystem();

        private Thread _timeoutThread;
        private bool _disposed = false;
        private int _timeout = 1000;
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static FlyingCenterSystem()
        {
        }

        private FlyingCenterSystem()
        {

            _timeoutThread = new Thread(new ThreadStart(this.execute));
            _timeoutThread.Start();
        }

        ~FlyingCenterSystem()
        {
            Dispose();
        }

        public static FlyingCenterSystem Instance
        {
            get
            {
                return instance;
            }
        }
// getFacade is gineric function is returning all facade's interfaces
        public T GetFacade<T>()
        {
            T obj = default(T);
            Type facadeType = typeof(T);
            if (facadeType == typeof(ILoggedInCustomerFacade))
            {
                if (_loggedInCustomerFacade is null)
                {
                    _loggedInCustomerFacade = new LoggedInCustomerFacade();
                }
                obj = (T)(object)_loggedInCustomerFacade;
            }
            else if (facadeType == typeof(ILoggedInAirLineFacade))
            {
                if (_loggedInAirlineFacade is null)
                {
                    _loggedInAirlineFacade = new LoggedInAirlineFacade();
                }
                obj = (T)(object)_loggedInAirlineFacade;
            }
            else if (facadeType == typeof(ILoggedInAdministratorFacade))
            {
                if (_loggedInAdministratorFacade is null)
                {
                    _loggedInAdministratorFacade = new LoggedInAdministratorFacade();
                }
                obj = (T)(object)_loggedInAdministratorFacade;
            }
            else if (facadeType == typeof(IAnonymousUserFacade))
            {
                if (_anonymousUserFacade is null)
                {
                    _anonymousUserFacade = new AnonymousUserFacade();
                }
                obj = (T)(object)_anonymousUserFacade;
            }

            return obj;
        }

        private void execute()
        {
            // get transfer's timeout
            _timeout = AppConfig.timeOut;
            LoginToken<Administrator> ltAdmin = null;
            LoginService ls = new LoginService();
            bool resLogin = ls.TryAdminLogin("9999", "admin", out ltAdmin);
            if (resLogin == true)
            {
                while (!_disposed)
                {
                    try
                    {
                        Thread.Sleep(_timeout);
                    }
                    catch (ThreadInterruptedException e)
                    {
                        break;
                    }
                    ILoggedInAdministratorFacade iLoggedAdminFacade = GetFacade<ILoggedInAdministratorFacade>();
                    //flights than landed more than 3 hours ago deleted from bd 
                    //table Flights and recorded in the table Flights_History.And the same thing with tickets;
                    iLoggedAdminFacade.TransferElapsedFlightsToHistory(ltAdmin);
                }
            }
        }
        public void Dispose()
        {
            _disposed = true;
            _timeoutThread.Interrupt();
            _timeoutThread.Join();
        }
    }
}
