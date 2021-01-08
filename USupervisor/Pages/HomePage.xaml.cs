using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using USupervisor.Users;

namespace USupervisor.Pages
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            User user = Data.User;
            InitializeComponent();

            NavBar navBar = new NavBar("Home");

            navBar.SetGrid(mainGrid);

            if (user is Supervisor)
                BuildForSupervisor();
            else if (user is Student)
                BuildForStudent();
        }

        private void BuildForStudent()
        {
            GetStudentData();
        }

        private void GetStudentData()
        {
            using (var cnn = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
            {
                cnn.Open();

                var selectCmd = cnn.CreateCommand();
                string email = "'" + Data.User.Email + "'";
                selectCmd.CommandText = ("SELECT * FROM Users WHERE email IS " + email);
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nameText.Text = (string)reader["name"];
                        groupText.Text = (string)reader["group"];
                        IDText.Text = reader["studentID"].ToString();
                    }
                }
            }
        }

        private void BuildForSupervisor()
        {
            GetSupervisorData();
        }

        private void GetSupervisorData()
        {
            using (var cnn = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
            {
                cnn.Open();

                var selectCmd = cnn.CreateCommand();
                string email = "'" + Data.User.Email + "'";
                selectCmd.CommandText = ("SELECT * FROM Users WHERE email IS " + email);
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nameText.Text = (string)reader["name"];
                        groupText.Text = (string)reader["group"];
                    }
                }
            }
        }
    }
}
