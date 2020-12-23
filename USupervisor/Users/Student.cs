using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USupervisor.Users
{
    class Student : User
    {
        private string course;
        private int studentID;
        private Supervisor personalSupervisor;

        protected override void DeleteUser()
        {
            throw new NotImplementedException();
        }
    }
}
