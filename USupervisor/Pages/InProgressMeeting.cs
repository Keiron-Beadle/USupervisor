using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        void Whisper(string Member, string MemberTo, string Message);

        [OperationContract(IsOneWay = true)]
        void Leave(string Member);

        [OperationContract(IsOneWay = true)]
        void InitializeMesh();

        [OperationContract(IsOneWay = true)]
        void SynchronizeMemberList(string Member);
    }

    //this channel interface provides a multiple inheritence adapter for our channel factory
    //that aggregates the two interfaces need to create the channel
    public interface IChatChannel : IChat, IClientChannel
    {
    }
    #endregion

    public class InProgressMeeting : IChat
    {

    }
}
