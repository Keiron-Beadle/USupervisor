using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USupervisor.Users
{
    class Student : User
    {
        private int studentID;
        private string personalSupervisor;

        public Student() : base("","") { }

        public Student(string name, string email, int studentID, string pSupervisor) : base(name, email)
        {
            this.studentID = studentID;
            personalSupervisor = pSupervisor;
        }
    }
}
