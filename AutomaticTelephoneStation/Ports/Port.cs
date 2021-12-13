using System;
using AutomaticTelephoneStation.Phones;

namespace AutomaticTelephoneStation.Ports
{
    public class Port : IPort
    {
        public event EventHandler<StartingCallEventArgs> StartCall;

        public PortState State { get; private set; }

        public Port()
        {
            State = PortState.Free;
        }

        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            if (State is PortState.Free)
            {
                State = PortState.Busy;

                OnStartingCall(this, new StartingCallEventArgs(args.SourcePhoneNumber, args.TargetPhoneNumber));
            }
        }

        protected virtual void OnStartingCall(object senderPhone, StartingCallEventArgs args)
        {
            StartCall?.Invoke(senderPhone, args);
        }
    }
}