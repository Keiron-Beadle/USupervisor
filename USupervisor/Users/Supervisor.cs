using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USupervisor.Users
{
    class Supervisor : User
    {
        private List<Student> assignedStudents;

        protected override void DeleteUser()
        {
            throw new NotImplementedException();
        }
    }
}
