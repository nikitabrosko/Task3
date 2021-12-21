using System;
using AutomaticTelephoneStation.CallReports;
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

        public ICallReport CallReports { get; }

        public Phone(ITariffPlan tariffPlan, IPhoneNumber phoneNumber)
        {
            TariffPlan = tariffPlan;
            PhoneNumber = phoneNumber;

            ConnectionState = ConnectionState.Disconnected;
            PhoneCallState = PhoneCallState.Silence;

            CallReports = new CallReport();
        }

        public void Call(IPhoneNumber phoneNumber)
        {
            if (ConnectionState == ConnectionState.Connected)
            {
                PhoneCallState = PhoneCallState.InProgress;

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

        public void OnResponseFromPort(object sender, ResponseCallEventArgs args)
        {
            switch (args.CallState)
            {
                case CallState.InProgress:
                    PhoneCallState = PhoneCallState.InProgress;
                    break;
                case CallState.IsEnd:
                    PhoneCallState = PhoneCallState.Silence;
                    break;
            }
        }

        public void OnCallReportFromPort(object sender, StationReportEventArgs args)
        {
            CallReports.AddCall(args.CallReport);
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