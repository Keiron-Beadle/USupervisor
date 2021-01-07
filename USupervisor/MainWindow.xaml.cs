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
            LoginPage page = new LoginPage();
            SqliteConnectionStringBuilder s = DatabaseContext.Instance.GetConnectionString;
            InitializeComponent();
            navigationFrame.Navigate(page);
        }
    }
}
