using System;
using AutomaticTelephoneStation.CallReports;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
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
        ICallReport CallReports { get; }
        void Call(IPhoneNumber phoneNumber);
        void ConnectToPort();
        void DisconnectFromPort();
        void OnIncomingCall(object sender, StationCallingEventArgs args);
        void OnResponseFromPort(object sender, ResponseCallEventArgs args);
        void OnCallReportFromPort(object sender, StationReportEventArgs args);
        void AcceptCall();
        void RejectCall();
    }
}