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
    public partial class MeetingPage : Page
    {
        private List<MeetingEntry> meetingsForUser;

        public MeetingPage()
        {
            meetingsForUser = new List<MeetingEntry>();
            InitializeComponent();
            titleText.Text = Data.User.Name + "'s Meetings";

            PopulateMeetings();
        }

        private void PopulateMeetings()
        {
            using (var connection = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                string email = "'" + Data.User.Email + "'";
                selectCmd.CommandText = ("SELECT * FROM Meetings WHERE email IS " + email);
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string title = (string)reader["title"];
                        List<User> attendees = new List<User>();
                        string[] attendeesString = reader["attendees"].ToString().Split(',');

                        for (int i = 0; i < attendeesString.Length; i++)
                        {
                            Student attendee = new Student();
                            PopulateAttendeeInfo(attendee, connection);
                            attendees.Add(new Student());
                        }

                        MeetingEntry currentEntry = new MeetingEntry(mainGrid);


                        meetingsForUser.Add();
                    }
                }
            }
        }

        private void PopulateAttendeeInfo(Student attendee, SqliteConnection connection)
        {
            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = ("SELECT name,email FROM Users WHERE email IS " + email);
        }
    }
}
