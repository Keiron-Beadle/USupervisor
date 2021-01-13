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

namespace USupervisor.Pages
{
    /// <summary>
    /// Interaction logic for Inbox.xaml
    /// </summary>
    public partial class Inbox : Page
    {
        public Inbox()
        {
            InitializeComponent();
            List<Mail> mailbox = new List<Mail>();
            using (var cnn = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
            {
                cnn.Open();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "SELECT * FROM Messages WHERE recipient IS " + ("'" + Data.User.Email + "'");
                using (var reader = cmd.ExecuteReader())
                {
                    int counter = 1;
                    while (reader.Read())
                    {
                        DateTime date = DateTime.Parse(reader["sendDate"].ToString());
                        int id = int.Parse(reader["ID"].ToString());
                        mailbox.Add(new Mail((string)reader["recipient"], (string)reader["sender"], (string)reader["message"], date, 
                            id, mainGrid, counter));
                        counter++;
                    }
                }
            }

        }
    }
}
