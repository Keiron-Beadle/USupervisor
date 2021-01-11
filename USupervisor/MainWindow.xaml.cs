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
            NavBar.Frame = navigationFrame;
            LoginPage page = new LoginPage();
            navigationFrame.Navigate(page);
        }
    }
}
