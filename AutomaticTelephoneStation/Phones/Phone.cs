using System;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.Phones
{
    public class Phone : IPhone
    {
        public event EventHandler<StartingCallEventArgs> OutgoingCall;
        public event EventHandler<ConnectionStateEventArgs> ChangeConnection;
        public event EventHandler<StationCallingEventArgs> CallChangeState;

        private ICall _call;

        public IPhoneNumber PhoneNumber { get; }

        public ITariffPlan TariffPlan { get; }

        public ConnectionState ConnectionState { get; private set; }

        public PhoneCallState PhoneCallState { get; private set; }

        public Phone(ITariffPlan tariffPlan, IPhoneNumber phoneNumber)
        {
            TariffPlan = tariffPlan;
            PhoneNumber = phoneNumber;

            ConnectionState = ConnectionState.Disconnected;
            PhoneCallState = PhoneCallState.Silence;
        }

        public void Call(IPhoneNumber phoneNumber)
        {
            if (ConnectionState == ConnectionState.Connected)
            {
                OnOutgoingCall(this, new StartingCallEventArgs(PhoneNumber, phoneNumber));
            }
        }

        public void ConnectToPort()
        {
            ConnectionState = ConnectionState.Connected;

            OnConnectionChange(this, new ConnectionStateEventArgs(ConnectionState.Connected));
        }

        public void DisconnectFromPort()
        {
            ConnectionState = ConnectionState.Disconnected;

            OnConnectionChange(this, new ConnectionStateEventArgs(ConnectionState.Disconnected));
        }

        public void OnIncomingCall(object sender, StationCallingEventArgs args)
        {
            PhoneCallState = PhoneCallState.StartCalling;

            _call = args.Call;
        }

        protected virtual void OnOutgoingCall(object sender, StartingCallEventArgs args)
        {
            OutgoingCall?.Invoke(sender, args);
        }

        protected virtual void OnConnectionChange(object sender, ConnectionStateEventArgs args)
        {
            ChangeConnection?.Invoke(sender, args);
        }

        protected virtual void OnCallChangeState(object sender, StationCallingEventArgs args)
        {
            CallChangeState?.Invoke(sender, args);
        }

        public void AcceptCall()
        {
            if (PhoneCallState is PhoneCallState.StartCalling)
            {
                PhoneCallState = PhoneCallState.InProgress;

                _call.CallState = CallState.InProgress;

                OnCallChangeState(this, new StationCallingEventArgs(_call));

                _call.StartStopwatch();
            }
        }

        public void RejectCall()
        {
            switch (PhoneCallState)
            {
                case PhoneCallState.StartCalling:
                    PhoneCallState = PhoneCallState.EndOfCall;
                    break;
                case PhoneCallState.InProgress:
                    _call.StopStopwatch();
                    PhoneCallState = PhoneCallState.EndOfCall;
                    break;
            }

            _call.CallState = CallState.IsEnd;

            OnCallChangeState(this, new StationCallingEventArgs(_call));

            PhoneCallState = PhoneCallState.Silence;
        }
    }
}