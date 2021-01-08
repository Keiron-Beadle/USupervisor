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
            int databaseReturn = InDatabase(email, password);
            switch (databaseReturn)
            {
                case 0:
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

        private int InDatabase(string email, string password)
        {
            using (var cnn = new SqliteConnection(DatabaseContext.Instance.GetConnectionString.ConnectionString))
            {
                cnn.Open();

                var selectCmd = cnn.CreateCommand();
                email = "'" + email + "'";
                selectCmd.CommandText = ("SELECT * FROM Users WHERE email IS " + email);
                using (var reader = selectCmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                        return 1;
                    while (reader.Read())
                    {
                        if ((string)reader["password"] == password)
                        {
                            return 0;
                        }
                        //System.Diagnostics.Debug.WriteLine(reader["email"] + " " + reader["password"]);
                    }
                }
            }

            return 2;
        }
    }
}
