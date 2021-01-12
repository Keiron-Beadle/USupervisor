using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USupervisor
{
    public class User 
    {
        protected string name;
        protected string email;
        protected string group;

        public User(string name, string email, string group)
        {
            this.name = name;
            this.email = email;
            this.group = group;
        }

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
        public string Group { get { return group; } set { group = value; } }
    }
}
