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
            NavBar navBar = new NavBar("Meetings");

            navBar.SetGrid(mainGrid);
            titleText.Text = Data.User.Name + "'s Meetings";

            PopulateMeetings();

            if(Data.User.Group == "Supervisor")
            {
                BuildCreateMeetingButton();
            }
        }

        private void BuildCreateMeetingButton()
        {
            //<Button Content="Create Meeting" Grid.Column="1" HorizontalAlignment="Left" Margin="710,25,0,0" Grid.Row="1" VerticalAlignment="Top" Width="104" Height="26"/>
            Button createMeeting = new Button();
            createMeeting.Content = "Create Meeting";
            createMeeting.Width = 100;
            createMeeting.Height = 26;
            Grid.SetRow(createMeeting, 1);
            Grid.SetColumn(createMeeting, 1);
            createMeeting.HorizontalAlignment = HorizontalAlignment.Left;
            createMeeting.VerticalAlignment = VerticalAlignment.Top;
            createMeeting.Click += CreateMeeting_Click;

            mainGrid.Children.Add(createMeeting);
        }

        private void CreateMeeting_Click(object sender, RoutedEventArgs e)
        {
            CreateMeetingPanel cmp = new CreateMeetingPanel();
            cmp.Show();
        }

        private void PopulateMeetings()
        {
            using (var connection = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
            {
                connection.Open();
                int counter = 0;
                var selectCmd = connection.CreateCommand();
                string email = "'%" + Data.User.Email + "%'";
                selectCmd.CommandText = ("SELECT * FROM Meetings WHERE attendees LIKE " + email);
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        counter++;
                        string title = (string)reader["title"];
                        List<User> attendees = new List<User>();
                        string[] attendeesString = reader["attendees"].ToString().Split(',');

                        for (int i = 0; i < attendeesString.Length; i++)
                        {
                            User attendee = new User("","", "");
                            attendee.SetEmail(attendeesString[i]);
                            PopulateAttendeeInfo(ref attendee, connection);
                            attendees.Add(attendee);
                        }

                        DateTime datetime = DateTime.Parse(reader["datetime"].ToString());

                        MeetingEntry currentEntry = new MeetingEntry(title, attendees, datetime, mainGrid, counter);


                        meetingsForUser.Add(currentEntry);
                    }
                }
            }
        }

        private void PopulateAttendeeInfo(ref User attendee, SqliteConnection connection)
        {
            var selectCmd = connection.CreateCommand();
            string email = "'" + attendee.Email + "'";
            selectCmd.CommandText = ("SELECT * FROM Users WHERE email IS " + email);
            using (var reader = selectCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    switch (reader["group"])
                    {
                        case "Supervisor":
                            attendee = new Supervisor(attendee.Name, attendee.Email, GetAssignedStudents(email, connection));
                            break;
                        case "Student":
                            attendee = new Student(attendee.Name, attendee.Email, GetStudentID(email, connection), GetStudentSupervisor(email,connection));
                            break;
                    }
                }
            }
        }

        private string GetStudentSupervisor(string email, SqliteConnection connection)
        {
            var sCmd = connection.CreateCommand();
            sCmd.CommandText = "SELECT assignedSupervisor FROM Users WHERE email IS " + email;
            using (var reader = sCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    return (string)reader["assignedSupervisor"];
                }
            }
            return "";
        }

        private int GetStudentID(string email, SqliteConnection connection)
        {
            var sCmd = connection.CreateCommand();
            sCmd.CommandText = "SELECT studentID FROM Users WHERE email IS " + email;
            using (var reader = sCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string r = reader["studentID"].ToString();
                    return int.Parse(r);
                }
            }
            return -1;
        }

        private List<string> GetAssignedStudents(string email, SqliteConnection connection)
        {
            var sCmd = connection.CreateCommand();
            sCmd.CommandText = "SELECT assignedStudents FROM Users WHERE email IS " + email;
            using (var reader = sCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string[] students = reader["assignedStudents"].ToString().Split(',');
                    return students.ToList();
                }
            }
            return new List<string>();
        }
    }
}
