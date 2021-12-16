using System;
using AutomaticTelephoneStation.Phones;

namespace AutomaticTelephoneStation.Ports
{
    public interface IPort
    {
        event EventHandler<StartingCallEventArgs> OutgoingCall;
        event EventHandler<StationCallingEventArgs> IncomingCall;
        public event EventHandler<StationCallingEventArgs> CallChangeState;
        PortState State { get; }
        ConnectionState ConnectionState { get; }
        void OnPhoneStartingCall(object sender, StartingCallEventArgs args);
        void OnConnectionChange(object sender, ConnectionStateEventArgs args);
        void OnPhoneCallingByStation(object sender, StationCallingEventArgs args);
        void OnCallChangeStateFromPhone(object sender, StationCallingEventArgs args);
    }
}