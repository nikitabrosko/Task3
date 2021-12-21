using System;
using AutomaticTelephoneStation.EventArgs;

namespace AutomaticTelephoneStation.Stations
{
    public interface IStation
    {
        event EventHandler<ResponseCallEventArgs> ResponseFromCall;
        event EventHandler<StationReportEventArgs> CallReport;
        PortController PortController { get; }
        void OnPhoneStartingCall(object sender, StartingCallEventArgs args);
        void OnCallChangeState(object sender, StationCallingEventArgs args);
    }
}