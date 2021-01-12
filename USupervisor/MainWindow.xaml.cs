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
using USupervisor.Pages;
using Microsoft.Data.Sqlite;

namespace USupervisor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            SqliteConnectionStringBuilder s = Database.Instance.GetConnectionString;
            InitializeComponent();
            navigationFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            Data.Frame = navigationFrame;
            LoginPage page = new LoginPage();
            if (System.IO.File.Exists("user"))
            { RememberedUser(ref page); }
            else { navigationFrame.Navigate(page); }
           
            Closed += MainWindow_Closed;
        }

        private void RememberedUser(ref LoginPage page)
        {
            using (System.IO.StreamReader s = new System.IO.StreamReader("user"))
            {
                string[] line = s.ReadLine().Split(',');
                page = new LoginPage(line[0], line[1]);
            }
            System.IO.File.Delete("user");
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            if (Data.User != null)
            {
                using (System.IO.StreamWriter s = new System.IO.StreamWriter("user"))
                {
                    using (var connection = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
                    {
                        connection.Open();
                        var cmd = connection.CreateCommand();
                        cmd.CommandText = "SELECT password FROM Users WHERE email IS " + ("'" + Data.User.Email + "'");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                s.WriteLine(Data.User.Email + "," + reader["password"]);
                            }
                        }
                    }
                }
            }
        }
    }
}
