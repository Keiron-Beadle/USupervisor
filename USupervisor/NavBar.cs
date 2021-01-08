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
        public static Frame Frame;
        private static Button[] pages;
        private string currentPage;

        public NavBar(string inCurrentPage)
        {
            currentPage = inCurrentPage;
            pages = new Button[3];
            pages[0] = new Button();
            pages[1] = new Button();
            pages[2] = new Button();
            pages[0].Content = "Home";
            pages[1].Content = "Messages";
            pages[2].Content = "Meetings";

            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].FontSize = 15;
                pages[i].HorizontalAlignment = HorizontalAlignment.Center;
                pages[i].VerticalAlignment = VerticalAlignment.Top;
                pages[i].Margin = new Thickness(0, 60 + (50 * i), 0, 0);
                pages[i].Width = 80;
                pages[i].Click += NavBar_Click;
                pages[i].Height = 30;
            }
        }

        public void SetGrid(Grid grid)
        {
            Grid.SetColumn(pages[0], 0);
            Grid.SetColumn(pages[1], 0);
            Grid.SetColumn(pages[2], 0);
            grid.Children.Add(pages[0]);
            grid.Children.Add(pages[1]);
            grid.Children.Add(pages[2]);
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
                    break;
            }
        }        
    }
}
