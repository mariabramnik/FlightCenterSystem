using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementSystem.Modules
{
    public interface IBasic<T>
    {
        int Add(T ob);
        T Get(int id);
        IList<T>GetAll();
        void Remove(T ob);
        void Update(T ob);
    }
}
