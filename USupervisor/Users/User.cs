using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USupervisor
{
    public abstract class User 
    {
        protected string name;
        protected string email;
        public void SetName(string input)
        {
            name = input;
        }
        public void SetEmail(string input)
        {
            email = input;
        }
        public string Name { get { return name; } }
        public string Email { get { return email; } }
        protected abstract void DeleteUser();
    }
}
