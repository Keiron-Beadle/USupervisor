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
using System.Windows.Shapes;

namespace USupervisor.Pages
{
    /// <summary>
    /// Interaction logic for CreateMessagePanel.xaml
    /// </summary>
    public partial class CreateMessagePanel : Window
    {
        public CreateMessagePanel()
        {
            InitializeComponent();

        }

        public CreateMessagePanel(string recipient) : this()
        {
            Dispatcher.Invoke(() =>
            {
                recipientTxt.Text = recipient;
            });
        }

        private void Send_Clicked(object sender, RoutedEventArgs e)
        {
            using (var cnn = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString))
            {
                cnn.Open();
                var transaction = cnn.BeginTransaction();
                var selectCmd = cnn.CreateCommand();
                selectCmd.CommandText = "SELECT * From Messages ORDER BY ID DESC LIMIT 1";
                int maxID = 0;
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string readResult = reader["ID"].ToString();
                        maxID = int.Parse(readResult) + 1;
                    }
                }
                string recipient = "\"" + recipientTxt.Text + "\"";
                string userSender = "\"" + Data.User.Email + "\"";
                string message = "\"" + messageTxt.Text + "\"";
                string dateTimeStr = "\"" + DateTime.Now.ToString("yyyy'-'MM'-'dd HH':'mm':'ss") +  "\"";
                string IDStr = "\"" + maxID.ToString() + "\"";
                var insertCmd = cnn.CreateCommand();
                insertCmd.CommandText = "INSERT INTO Messages VALUES ( " + recipient + ", " + userSender + ", " + message + ", " + dateTimeStr + ", " + IDStr + " )";
                System.Diagnostics.Debug.WriteLine(insertCmd.CommandText);
                insertCmd.Transaction = transaction;
                insertCmd.ExecuteNonQuery();
                transaction.Commit();
            }
            Close();
        }
    }
}
