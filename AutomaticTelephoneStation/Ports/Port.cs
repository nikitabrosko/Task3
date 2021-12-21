using System;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Stations;

namespace AutomaticTelephoneStation.Ports
{
    public class Port : IPort
    {
        public event EventHandler<StartingCallEventArgs> OutgoingCall;
        public event EventHandler<StationCallingEventArgs> IncomingCall;
        public event EventHandler<StationCallingEventArgs> CallChangeState;
        public event EventHandler<ResponseCallEventArgs> ResponseFromStation;
        public event EventHandler<StationReportEventArgs> CallReport;

        public PortState State { get; private set; }

        public ConnectionState ConnectionState { get; private set; }

        public IPhone Phone { get; }

        public Port(IPhone phone, IStation station)
        {
            if (phone is null)
            {
                throw new ArgumentNullException(nameof(phone));
            }

            if (station is null)
            {
                throw new ArgumentNullException(nameof(station));
            }

            Phone = phone;
            State = PortState.Free;
            ConnectionState = ConnectionState.Disconnected;

            Phone.OutgoingCall += OnPhoneStartingCall;
            Phone.ChangeConnection += OnConnectionChange;
            Phone.CallChangeState += OnCallChangeStateFromPhone;

            CallReport += Phone.OnCallReportFromPort;
            IncomingCall += Phone.OnIncomingCall;
            CallChangeState += station.OnCallChangeState;
            ResponseFromStation += Phone.OnResponseFromPort;
            OutgoingCall += station.OnPhoneStartingCall;
            station.ResponseFromCall += OnResponseFromCall;
            station.CallReport += OnCallReportFromStation;

            station.PortController.AddPort(this);
        }

        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            if (State is PortState.Free && ConnectionState == ConnectionState.Connected)
            {
                State = PortState.Busy;

                OnOutgoingCall(this, new StartingCallEventArgs(args.SourcePhoneNumber, args.TargetPhoneNumber));
            }
        }

        public void OnPhoneCallingByStation(object sender, StationCallingEventArgs args)
        {
            if (State == PortState.Free && ConnectionState == ConnectionState.Connected)
            {
                State = PortState.Busy;
                OnIncomingCall(this, args);
            }
        }

        public void OnResponseFromCall(object sender, ResponseCallEventArgs args)
        {
            if (args.CallState is Calls.CallState.IsEnd)
            {
                State = PortState.Free;
            }

            OnResponseFromStation(sender, args);
        }

        public void OnConnectionChange(object sender, ConnectionStateEventArgs args)
        {
            ConnectionState = args.ConnectionState;
        }

        public void OnCallChangeStateFromPhone(object sender, StationCallingEventArgs args)
        {
            if (args.Call.CallState is Calls.CallState.IsEnd)
            {
                State = PortState.Free;
            }

            OnCallChangeState(sender, args);
        }

        public void OnCallReportFromStation(object sender, StationReportEventArgs args)
        {
            OnCallReport(sender, args);
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

        protected virtual void OnResponseFromStation(object sender, ResponseCallEventArgs args)
        {
            ResponseFromStation?.Invoke(sender, args);
        }

        protected virtual void OnCallReport(object sender, StationReportEventArgs args)
        {
            CallReport?.Invoke(sender, args);
        }
    }
}