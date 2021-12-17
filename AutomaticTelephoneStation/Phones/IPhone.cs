using System;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.Phones
{
    public interface IPhone
    {
        event EventHandler<StartingCallEventArgs> OutgoingCall;
        event EventHandler<ConnectionStateEventArgs> ChangeConnection;
        event EventHandler<StationCallingEventArgs> CallChangeState;
        IPhoneNumber PhoneNumber { get; }
        ITariffPlan TariffPlan { get; }
        ConnectionState ConnectionState { get; }
        PhoneCallState PhoneCallState { get; }
        void Call(IPhoneNumber phoneNumber);
        void ConnectToPort();
        void DisconnectFromPort();
        void OnIncomingCall(object sender, StationCallingEventArgs args);
        void AcceptCall();
        void RejectCall();
    }
}