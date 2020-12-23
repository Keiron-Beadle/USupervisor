using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USupervisor
{
    abstract class User 
    {
        protected string name;
        protected string email;

        protected abstract void DeleteUser();
    }
}
