using System;
using AutomaticTelephoneStation.BillingSystem.CallReports;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.Phones
{
    public interface IPhone
    {
        event EventHandler<StartingCallEventArgs> OutgoingCall;
        event EventHandler<ConnectionStateEventArgs> ChangeConnection;
        event EventHandler<StationCallingEventArgs> CallChangeState;
        IPhoneNumber PhoneNumber { get; }
        ConnectionState ConnectionState { get; }
        PhoneCallState PhoneCallState { get; }
        ICallReportRepository CallReports { get; }
        void Call(IPhoneNumber phoneNumber);
        void ConnectToPort();
        void DisconnectFromPort();
        void OnIncomingCall(object sender, StationCallingEventArgs args);
        void OnResponseFromPort(object sender, ResponseCallEventArgs args);
        void OnCallReportFromPort(object sender, PortReportEventArgs args);
        void AcceptCall();
        void RejectCall();
    }
}