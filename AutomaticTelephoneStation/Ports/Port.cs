using System;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Phones;

namespace AutomaticTelephoneStation.Ports
{
    public class Port : IPort
    {
        public event EventHandler<StartingCallEventArgs> OutgoingCall;
        public event EventHandler<StationCallingEventArgs> IncomingCall;
        public event EventHandler<StationCallingEventArgs> CallChangeState;

        public PortState State { get; private set; }

        public ConnectionState ConnectionState { get; private set; }

        public Port()
        {
            State = PortState.Free;
            ConnectionState = ConnectionState.Disconnected;
        }

        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            if (State is PortState.Free && ConnectionState == ConnectionState.Connected)
            {
                State = PortState.Busy;

                OnOutgoingCall(this, new StartingCallEventArgs(args.SourcePhoneNumber, args.TargetPhoneNumber));
            }

            State = PortState.Free;
        }

        public void OnPhoneCallingByStation(object sender, StationCallingEventArgs args)
        {
            if (State == PortState.Free && ConnectionState == ConnectionState.Connected)
            {
                State = PortState.Busy;
                OnIncomingCall(this, args);
            }

            State = PortState.Free;
        }

        public void OnConnectionChange(object sender, ConnectionStateEventArgs args)
        {
            ConnectionState = args.ConnectionState;
        }

        public void OnCallChangeStateFromPhone(object sender, StationCallingEventArgs args)
        {
            OnCallChangeState(sender, args);
        }

        protected virtual void OnOutgoingCall(object sender, StartingCallEventArgs args)
        {
            OutgoingCall?.Invoke(sender, args);
        }

        protected virtual void OnIncomingCall(object sender, StationCallingEventArgs args)
        {
            IncomingCall?.Invoke(sender, args);
        }

        protected virtual void OnCallChangeState(object sender, StationCallingEventArgs args)
        {
            CallChangeState?.Invoke(sender, args);
        }
    }
}