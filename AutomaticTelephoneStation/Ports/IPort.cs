using System;
using AutomaticTelephoneStation.Phones;

namespace AutomaticTelephoneStation.Ports
{
    public interface IPort
    {
        event EventHandler<StartingCallEventArgs> StartCall;

        PortState State { get; }

        void OnPhoneStartingCall(object sender, StartingCallEventArgs args);
    }
}