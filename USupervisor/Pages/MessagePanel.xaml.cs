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
using System.Windows.Threading;

namespace USupervisor.Pages
{
    /// <summary>
    /// Interaction logic for MessagePanel.xaml
    /// </summary>
    public partial class MessagePanel : Window
    {
        private int ID;

        public MessagePanel(string sender, string message, int ID)
        {
            this.ID = ID;
            InitializeComponent();
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                messageTxt.Text = message;
                senderTxt.Text = sender;
            });
        }

        private void Reply_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SqliteConnection cnn = new SqliteConnection(Database.Instance.GetConnectionString.ConnectionString);
            using (cnn)
            {
                cnn.Open();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "DELETE FROM Messages WHERE ID IS " + ("'" + ID.ToString() + "'");
                cmd.ExecuteNonQuery();
            }
            
        }
    }
}
