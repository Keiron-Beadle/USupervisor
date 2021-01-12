using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USupervisor.Users
{
    class Supervisor : User
    {
        private List<string> assignedStudents;

        public Supervisor() : base("", "", "") { }

        public Supervisor(string name, string email, List<string> assignedStudents) : base(name, email, "Supervisor")
        {
            this.assignedStudents = assignedStudents;
        }
    }
}
