using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Diagnostics;

namespace USupervisor.Pages
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = emailBox.Text;
            string password = passwordBox.Password;

            if (string.IsNullOrEmpty(email))
            {
                Debug.WriteLine("Null or empty email");
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                Debug.WriteLine("Null or empty password");
                return;
            }
            Tuple<int, string> databaseReturn = InDatabase(email, password);
            switch (databaseReturn.Item1)
            {
                case 0:
                    User currentUser = null;
                    if (databaseReturn.Item2 == "Supervisor")
                    {
                        currentUser = new Users.Supervisor();
                        currentUser.Group = "Supervisor";
                    }
                    else if (databaseReturn.Item2 == "Student")
                    {
                        currentUser = new Users.Student();
                        currentUser.Group = "Student";

                    }
                    FillUserData(currentUser, email);
                    Data.User = currentUser;
                    HomePage page = new HomePage();
                    NavBar.Frame.Navigate(page);
                    break;
                case 1:
                    Debug.WriteLine("Not in database");
                    break;
                case 2:
                    Debug.WriteLine("Wrong password");
                    break;
            }
        }

        private void FillUserData(User user, string email)
        {
            using (var cnn = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
            {
                cnn.Open();

                var selectCmd = cnn.CreateCommand();
                email = "'" + email + "'";
                selectCmd.CommandText = ("SELECT name,email FROM Users WHERE email IS " + email);
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.SetName((string)reader["name"]);
                        user.SetEmail((string)reader["email"]);
                    }
                }
            }
        }

        private Tuple<int,string> InDatabase(string email, string password)
        {
            using (var cnn = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
            {
                cnn.Open();

                var selectCmd = cnn.CreateCommand();
                email = "'" + email + "'";
                selectCmd.CommandText = ("SELECT * FROM Users WHERE email IS " + email);
                using (var reader = selectCmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return new Tuple<int, string>(1, "");
                    while (reader.Read())
                    {
                        if ((string)reader["password"] == password)
                        {
                            return new Tuple<int, string>(0, (string)reader["group"]);
                        }
                        //System.Diagnostics.Debug.WriteLine(reader["email"] + " " + reader["password"]);
                    }
                }
            }

            return new Tuple<int, string>(2,"");
        }
    }
}
