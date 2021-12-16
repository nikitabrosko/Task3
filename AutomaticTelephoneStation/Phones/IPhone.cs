using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.Phones
{
    public interface IPhone
    {
        event EventHandler<StartingCallEventArgs> OutgoingCall;
        public event EventHandler<ConnectionStateEventArgs> ChangeConnection;
        public event EventHandler<StationCallingEventArgs> CallChangeState;
        IPhoneNumber PhoneNumber { get; }
        ITariffPlan TariffPlan { get; }
        ConnectionState ConnectionState { get; }
        public PhoneCallState PhoneCallState { get; }
        void Call(IPhoneNumber phoneNumber);
        public void ConnectToPort();
        public void DisconnectFromPort();
        void OnIncomingCall(object sender, StationCallingEventArgs args);
        void AcceptCall();
        void RejectCall();
    }
}