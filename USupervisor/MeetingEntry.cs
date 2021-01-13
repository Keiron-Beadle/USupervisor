using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using USupervisor.Pages;

namespace USupervisor
{
    class MeetingEntry
    {
        private string title;
        private DateTime dateTime;
        List<User> attendees;

        public MeetingEntry(string title, List<User> attendees, DateTime dateTime, Grid mainGrid, int counter)
        {
            this.title = title;
            this.dateTime = dateTime;
            this.attendees = attendees;

            int height = 60;
            int yPos = (height + 10) * counter;

            MakeBackground(mainGrid, height, yPos);

            MakeTitleText(mainGrid, yPos);

            MakeDateTimeText(mainGrid, yPos);

            MakeAttendeesBox(mainGrid, yPos);

            MakeAttendeesTitle(mainGrid, yPos);

            MakeJoinButton(mainGrid, yPos);

            /*
            <TextBlock Grid.Column="1" HorizontalAlignment="Left" FontSize="15" Foreground="White" Margin="30,25,0,0" Grid.Row="1" Text="Meeting with Chad" VerticalAlignment="Top"/>
            <TextBlock Grid.Column="1" HorizontalAlignment="Left" FontSize="12" Foreground="Peru" Margin="30,50,0,0" Grid.Row="1" Text="10/01/21 - 9:00" VerticalAlignment="Top"/>
            <Rectangle Grid.Column="1" Fill="#2c2f70" HorizontalAlignment="Left" Height="58" Margin="206,26,0,0" Grid.Row="1" VerticalAlignment="Top" Width="215"/>
            <TextBlock Grid.Column="1" HorizontalAlignment="Left" FontSize="10" Foreground="White" Margin="209,28,0,0" Grid.Row="1" Text="Attendees" VerticalAlignment="Top"/>*/



        }

        private void MakeJoinButton(Grid mainGrid, int yPos)
        {
           //< Button Content = "Button" Grid.Column = "1" HorizontalAlignment = "Left" Margin = "426,40,0,0" Grid.Row = "1" VerticalAlignment = "Top" Width = "35" Height = "35" />

            Button joinBtn = new Button();
            Grid.SetRow(joinBtn, 1);
            Grid.SetColumn(joinBtn, 1);
            joinBtn.Background = null;
            joinBtn.Content = new Image
            {
                Source = new BitmapImage(new Uri(@"C:\Users\Keiro\Desktop\USupervisor\USupervisor\Resources\joinCallIcon.png")),
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            };
            joinBtn.Click += JoinBtn_Click;
            joinBtn.Width = 35;
            joinBtn.Height = 35;
            joinBtn.Margin = new System.Windows.Thickness(426, yPos + 15, 0, 0);
            joinBtn.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            joinBtn.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            mainGrid.Children.Add(joinBtn);
        }

        private void JoinBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Data.Frame.Navigate(new InProgressMeetingPage());
        }

        private void MakeAttendeesTitle(Grid mainGrid, int yPos)
        {
            TextBlock attendeesText = new TextBlock();
            Grid.SetColumn(attendeesText, 1);
            Grid.SetRow(attendeesText, 1);
            attendeesText.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            attendeesText.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            attendeesText.FontSize = 10;
            attendeesText.Foreground = new SolidColorBrush(Colors.White);
            attendeesText.Margin = new System.Windows.Thickness(209, yPos + 8, 0, 0);
            attendeesText.Text = "Attendees";

            mainGrid.Children.Add(attendeesText);
        }

        private void MakeAttendeesBox(Grid mainGrid, int yPos)
        {
            Rectangle attendeesBox = new Rectangle();
            Grid.SetColumn(attendeesBox, 1);
            Grid.SetRow(attendeesBox, 1);
            attendeesBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            attendeesBox.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            attendeesBox.Fill = new SolidColorBrush(Color.FromRgb(44, 47, 112));
            attendeesBox.Height = 50;
            attendeesBox.Margin = new System.Windows.Thickness(206, yPos + 6, 0, 0);
            attendeesBox.Width = 215;

            mainGrid.Children.Add(attendeesBox);
        }

        private void MakeDateTimeText(Grid mainGrid, int yPos)
        {
            TextBlock dateTimeText = new TextBlock();
            Grid.SetColumn(dateTimeText, 1);
            Grid.SetRow(dateTimeText, 1);
            dateTimeText.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            dateTimeText.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            dateTimeText.FontSize = 12;
            dateTimeText.Foreground = new SolidColorBrush(Colors.Peru);
            dateTimeText.Margin = new System.Windows.Thickness(30, yPos + 25, 0, 0);
            dateTimeText.Text = dateTime.ToShortDateString() + " - " + dateTime.ToShortTimeString();

            mainGrid.Children.Add(dateTimeText);
        }

        private void MakeTitleText(Grid mainGrid, int yPos)
        {
            TextBlock titleText = new TextBlock();
            Grid.SetColumn(titleText, 1);
            Grid.SetRow(titleText, 1);
            titleText.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            titleText.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            titleText.FontSize = 15;
            titleText.Foreground = new SolidColorBrush(Colors.White);
            titleText.Margin = new System.Windows.Thickness(30, yPos + 5, 0, 0);
            titleText.Text = title;

            mainGrid.Children.Add(titleText);
        }

        private void MakeBackground(Grid mainGrid, int height, int yPos)
        {
            Rectangle meetingBorder = new Rectangle();
            Grid.SetColumn(meetingBorder, 1);
            Grid.SetRow(meetingBorder, 1);
            meetingBorder.Fill = new SolidColorBrush(Color.FromRgb(78, 81, 150));
            meetingBorder.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            meetingBorder.Height = height;
            meetingBorder.Margin = new System.Windows.Thickness(20, yPos, 0, 0);
            meetingBorder.Stroke = new SolidColorBrush(Colors.Black);
            meetingBorder.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            meetingBorder.Width = 450;

            mainGrid.Children.Add(meetingBorder);
        }
    }
}
