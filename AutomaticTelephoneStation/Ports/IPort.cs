using System;
using AutomaticTelephoneStation.Phones;

namespace AutomaticTelephoneStation.Ports
{
    public interface IPort
    {
        event EventHandler<StartingCallEventArgs> StartCall;
        PortState State { get; }
        ConnectionState ConnectionState { get; }
        void OnPhoneStartingCall(object sender, StartingCallEventArgs args);
        void OnConnectionChange(object sender, ConnectionStateEventArgs args);
    }
}