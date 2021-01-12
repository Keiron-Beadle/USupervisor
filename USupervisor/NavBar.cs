using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using USupervisor.Pages;

namespace USupervisor
{
    public class NavBar : FrameworkElement
    {
        public static Frame Frame = Data.Frame;
        private static Button[] pages;
        private string currentPage;

        public NavBar(string inCurrentPage)
        {
            currentPage = inCurrentPage;
            pages = new Button[4];
            for (int i = 0; i < pages.Length; i++) { pages[i] = new Button(); }
            pages[0].Content = "Home";
            pages[1].Content = "Messages";
            pages[2].Content = "Meetings";
            pages[3].Content = "Log Out";

            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].FontSize = 15;
                pages[i].HorizontalAlignment = HorizontalAlignment.Center;
                pages[i].VerticalAlignment = VerticalAlignment.Top;
                pages[i].Margin = new Thickness(0, 20 + (50 * i), 0, 0);
                pages[i].Width = 80;
                pages[i].Click += NavBar_Click;
                pages[i].Height = 30;
            }
        }

        public void SetGrid(Grid grid)
        {
            for (int i = 0; i < pages.Length; i++)
            {
                Grid.SetColumn(pages[i], 0);
                Grid.SetRowSpan(pages[i], 2);
                grid.Children.Add(pages[i]);
            }
        }

        private void NavBar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button active = (Button)sender;

            if ((string)active.Content == currentPage)
                return;

            switch (active.Content)
            {
                case "Home":
                    Frame.Navigate(new HomePage());
                    break;
                case "Messages":
                    break;
                case "Meetings":
                    Frame.Navigate(new MeetingPage());
                    break;
                case "Log Out":
                    Data.User = null;
                    Frame.Navigate(new LoginPage());
                    break;
            }
        }        
    }
}
