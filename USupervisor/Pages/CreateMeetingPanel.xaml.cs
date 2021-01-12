using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace USupervisor.Pages
{
    public class DataGridEntry /*: INotifyPropertyChanged*/
    {
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Group { get; set; }
        public bool Invited { get; set; }

        public DataGridEntry(string forename, string surname, string email, string group) 
        {
            Forename = forename;
            Surname = surname;
            Email = email;
            Group = group;
            Invited = false;
           /* OnPropertyChanged("forename");            
            OnPropertyChanged("surname");            
            OnPropertyChanged("email");            
            OnPropertyChanged("role");            
            OnPropertyChanged("selected");*/
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //private void OnPropertyChanged(string propertyname)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        //}
    }

    public partial class CreateMeetingPanel : Window
    {
        public List<DataGridEntry> entries = new List<DataGridEntry>();

        public CreateMeetingPanel()
        {
            InitializeComponent();

            using (var connection = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
            {
                connection.Open();
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT * FROM Users";
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] name = reader["name"].ToString().Split(' ');
                        entries.Add(new DataGridEntry(name[0], name[1], reader["email"].ToString(), reader["group"].ToString()));
                    }
                }
            }
            dataGrid.ItemsSource = entries;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string selectedEmails = "";
            var rows = GetDataGridRows(dataGrid);

            foreach (DataGridRow row in rows)
            {
                DataGridEntry entry = (DataGridEntry)row.Item;
                if (entry.Invited)
                {
                    string email = entry.Email;
                    selectedEmails += email + ',';
                }
            }


            using (var connection = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
            {
                connection.Open();
                var insertCmd = connection.CreateCommand();
                var transaction = connection.BeginTransaction();
                string title = "'" + meetingTitle.Text + "'";
                DateTime dateTime = datePicker.Value.Value.Date;
                TimeSpan time = timePicker.Value.Value.TimeOfDay;
                dateTime = dateTime.Add(time);
                selectedEmails = "'" + selectedEmails + "'";
                string dateTimeStr = dateTime.ToString("yyyy'-'MM'-'dd HH':'mm':'ss");
                dateTimeStr = "'" + dateTimeStr + "'";
                insertCmd.CommandText = "INSERT INTO Meetings (title, attendees, datetime) VALUES ( " + title + ", " + selectedEmails + "," + dateTimeStr + " )";
                insertCmd.Transaction = transaction;
                insertCmd.ExecuteNonQuery();
                transaction.Commit();
                
            }

            this.Close();
        }

        private IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }
    }
}
