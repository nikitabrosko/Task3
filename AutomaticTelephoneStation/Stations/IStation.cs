using System;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;

namespace AutomaticTelephoneStation.Stations
{
    public interface IStation
    {
        event EventHandler<ResponseCallEventArgs> ResponseFromCall;
        PortController PortController { get; }
        void OnPhoneStartingCall(object sender, StartingCallEventArgs args);
        void OnCallChangeState(object sender, StationCallingEventArgs args);
    }
}