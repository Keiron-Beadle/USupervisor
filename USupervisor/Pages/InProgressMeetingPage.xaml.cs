using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows;
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
    #region Service Interfaces
    //this is our simple service contract
    [ServiceContract(Namespace = "http://rolandrodriguez.net.samples.wpfchat", CallbackContract = typeof(IChat))]
    public interface IChat
    {
        [OperationContract(IsOneWay = true)]
        void Join(string Member);

        [OperationContract(IsOneWay = true)]
        void Chat(string Member, string Message);

        [OperationContract(IsOneWay = true)]
        void Leave(string Member);

        [OperationContract(IsOneWay = true)]
        void InitialiseMesh();
    }

    //this channel interface provides a multiple inheritence adapter for our channel factory
    //that aggregates the two interfaces need to create the channel
    public interface IChatChannel : IChat, IClientChannel
    {
    }
    #endregion

    public partial class InProgressMeetingPage : Page,IChat
    {
        private string member;
        private IChatChannel participant;
        private InstanceContext site;
        private NetPeerTcpBinding binding;
        private ChannelFactory<IChatChannel> channelFactory;
        private IOnlineStatus statusHandler;
        private delegate void NoArgDelegate();

        public InProgressMeetingPage()
        {
            InitializeComponent();
            NavBar navBar = new NavBar("InProgressMeeting");

            navBar.SetGrid(mainGrid);
            NoArgDelegate exec = new NoArgDelegate(this.ConnectToMesh);
            exec.BeginInvoke(null, null);
        }

        private void ConnectToMesh()
        {
            site = new InstanceContext(this);
            binding = new NetPeerTcpBinding("WPFChatBinding");
            channelFactory = new DuplexChannelFactory<IChatChannel>(site, "WPFChatEndpoint");
            participant = channelFactory.CreateChannel();
            statusHandler = participant.GetProperty<IOnlineStatus>();
            statusHandler.Online += new EventHandler(stat_Online);
            statusHandler.Offline += new EventHandler(stat_Offline);

            participant.InitialiseMesh();
        }

        private void stat_Offline(object sender, EventArgs e)
        {

        }

        private void stat_Online(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                (DispatcherOperationCallback)delegate (object arg)
                {
                    member = Data.User.Name;
                    participant.Join(member);
                    return null;
                }, null);
        }

        public void Join(string Member)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (DispatcherOperationCallback)delegate (object arg)
                {
                    this.chatHistory.Items.Add(Member + " joined the meeting.");
                    return null;
                }, null);
        }

        public void Chat(string Member, string Message)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (DispatcherOperationCallback)delegate (object arg)
                {
                    this.chatHistory.Items.Add(Member + ": " + Message);
                    return null;
                }, null);
        }

        public void InitialiseMesh() { }
       
        public void Leave(string Member)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (DispatcherOperationCallback)delegate (object arg)
                {
                    this.chatHistory.Items.Add(Member + " has left the meeting.");
                    return null;
                }, null);
        }

        private void MessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(messageBox.Text))
            {
                participant.Chat(member, messageBox.Text);
                messageBox.Clear();
            }
        }
    }
}
