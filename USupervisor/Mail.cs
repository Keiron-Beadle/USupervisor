using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using USupervisor.Pages;

namespace USupervisor
{
    class Mail
    {
        private string recipient;
        private string sender;
        private string message;
        private DateTime sendDate;
        private int ID;

        public Mail(string recipient, string sender, string message, DateTime sendDate, int ID, Grid mainGrid, int counter)
        {
            this.recipient = recipient;
            this.sender = sender;
            this.message = message;
            this.sendDate = sendDate;
            this.ID = ID;
            int height = 50;
            int yPos = (height + 10) * counter;

            MakeBackgroundRectangle(mainGrid, height, yPos);
            MakeSenderTxt(mainGrid, yPos);
            MakeSendDateTxt(mainGrid, yPos);
            MakePreviewMsgTxt(mainGrid, yPos);
            MakeButton(mainGrid, yPos);
            /*
                <Rectangle Fill="#4247ad" HorizontalAlignment="Left" Height="42" Margin="20,10,0,0" VerticalAlignment="Top" Width="500"/>
                <TextBlock x:Name="senderTxt" HorizontalAlignment="Left" Margin="30,13,0,0" FontSize="13" Foreground="White" VerticalAlignment="Top"/>
                <TextBlock x:Name="sendDateTxt" HorizontalAlignment="Left" Margin="415,13,0,0" FontSize="13" Foreground="Peru" VerticalAlignment="Top"/>
                <TextBlock x:Name="previewMsgTxt" Foreground="White" HorizontalAlignment="Left" Margin="30,30,0,0" MaxWidth="450" VerticalAlignment="Top"/>*/

        }

        private void MakeButton(Grid mainGrid, int yPos)
        {
            Button btn = new Button()
            {
                Width = 500,
                Height = 42,
                Background = null,
                Margin = new Thickness(20, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };
            btn.Style = null;
            btn.Click += Btn_Click;
            mainGrid.Children.Add(btn);
        }

        private void MakeBackgroundRectangle(Grid mainGrid, int height, int yPos)
        {
            Rectangle rect = new Rectangle()
            {
                Fill = new SolidColorBrush(Color.FromRgb(66, 71, 173)),
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 42,
                Margin = new Thickness(20, 10, 0, 0),
                VerticalAlignment = VerticalAlignment.Top,
                Width = 500
            };
            mainGrid.Children.Add(rect);
        }

        private void MakeSenderTxt(Grid mainGrid, int yPos)
        {
            TextBlock txt = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(30, 13, 0, 0),
                FontSize = 13,
                Foreground = new SolidColorBrush(Colors.White),
                VerticalAlignment = VerticalAlignment.Top,
                Text = "From: " + sender
            };
            mainGrid.Children.Add(txt);
        }

        private void MakeSendDateTxt(Grid mainGrid, int yPos)
        {
            TextBlock txt = new TextBlock()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(400, 13, 0, 0),
                FontSize = 13,
                Foreground = new SolidColorBrush(Colors.Peru),
                VerticalAlignment = VerticalAlignment.Top,
                Text = sendDate.ToString()
            };
            mainGrid.Children.Add(txt);
        }

        private void MakePreviewMsgTxt(Grid mainGrid, int yPos)
        {
            TextBlock txt = new TextBlock()
            {
                Foreground = new SolidColorBrush(Colors.White),
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(30, 30, 0, 0),
                MaxWidth = 450,
                MaxHeight= 30,
                VerticalAlignment = VerticalAlignment.Top,
                Text = message
            };
            mainGrid.Children.Add(txt);
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessagePanel message = new MessagePanel(this.sender, this.message, this.ID);
            message.Show();
        }
    }
}
