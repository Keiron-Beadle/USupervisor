using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace USupervisor
{
    public sealed class NavBar : Control
    {
        private static NavBar instance = null;
        private static readonly object padlock = new object();

        private static Button[] pages;
        NavBar()
        {
            pages = new Button[3];
            pages[0] = new Button();
            pages[0].Content = "Home";
            pages[1].Content = "Messages";
            pages[2].Content = "Meetings";

            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].FontSize = 10;
                pages[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                pages[i].VerticalAlignment = System.Windows.VerticalAlignment.Center;
                pages[i].Width = 80;
                pages[i].Click += NavBar_Click;
            }
        }

        private void NavBar_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Button active = (Button)sender;
            switch (active.Content)
            {
                case "Home":
                    break;
                case "Messages":
                    break;
                case "Meetings":
                    break;
            }
        }

        public static NavBar Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new NavBar();
                }
                return instance;
            }
        }
    }
}
