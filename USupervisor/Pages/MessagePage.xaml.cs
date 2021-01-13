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
    /// Interaction logic for MessagePage.xaml
    /// </summary>
    public partial class MessagePage : Page
    {
        public string currentState = "Inbox";
        public MessagePage()
        {
            InitializeComponent();
            NavBar navBar = new NavBar("Messages");
            navBar.SetGrid(mainGrid);

            inboxBtn.Background = null;
            inboxBtn.Margin = new Thickness(10, navBar.MinimumYPosition + 15, 0, 0);
            inboxBtn.Click += InboxBtn_Click;
            inboxBtn.Content = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/inboxIcon.png")),
                VerticalAlignment = VerticalAlignment.Center
            };

            outboxBtn.Background = null;
            outboxBtn.Margin = new Thickness(75, navBar.MinimumYPosition + 15, 0, 0);
            outboxBtn.Click += OutboxBtn_Click;
            outboxBtn.Content = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/Images/outboxIcon.png")),
                VerticalAlignment = VerticalAlignment.Center
            };

            createMessageBtn.Click += CreateMessageBtn_Click;

            UpdateMessageState();
        }

        private void CreateMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateMessagePanel cmp = new CreateMessagePanel();
            cmp.Show();
        }

        private void OutboxBtn_Click(object sender, RoutedEventArgs e)
        {
            currentState = "Outbox";
            UpdateMessageState();
        }

        private void InboxBtn_Click(object sender, RoutedEventArgs e)
        {
            currentState = "Inbox";
            UpdateMessageState();
        }

        private void UpdateMessageState()
        {
            Dispatcher.Invoke(() =>
            {
                MessageState.Text = currentState;
            });
            switch (currentState) { case "Inbox": messageFrame.Navigate(new Inbox()); break; case "Outbox": messageFrame.Navigate(new Outbox()); break; }
        }
    }
}
