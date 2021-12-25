using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Ports;

namespace AutomaticTelephoneStation.Stations
{
    public class Station : IStation
    {
        public event EventHandler<ResponseCallEventArgs> ResponseFromCall;
        public event EventHandler<StationReportEventArgs> CallReport;

        private readonly CountryCode _countryCode;
        private readonly IList<ICall> _waitingCalls = new List<ICall>();
        private readonly IList<ICall> _inProgressCalls = new List<ICall>();

        public IEnumerable<ICall> WaitingCalls => new ReadOnlyCollection<ICall>(_waitingCalls);
        
        public IEnumerable<ICall> InProgressCalls => new ReadOnlyCollection<ICall>(_inProgressCalls);

        public PortController PortController { get; }

        public Station(CountryCode countryCode)
        {
            _countryCode = countryCode;
            PortController = new PortController();
        }

        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            if (args.TargetPhoneNumber.Number.StartsWith(string.Concat("+", ((int)_countryCode).ToString())) 
                && args.SourcePhoneNumber.Number.StartsWith(string.Concat("+", ((int)_countryCode).ToString())))
            {
                var call = new Call(args.SourcePhoneNumber, args.TargetPhoneNumber);

                if (!CheckForBusyNumbers(args.SourcePhoneNumber) && !CheckForBusyNumbers(args.TargetPhoneNumber))
                {
                    FindPortViaPhoneNumber(args.TargetPhoneNumber)
                        .OnPhoneCallingByStation(sender, new StationCallingEventArgs(call));

                    FindPortViaPhoneNumber(args.SourcePhoneNumber)
                        .OnPhoneCallingByStation(sender, new StationCallingEventArgs(call));

                    _waitingCalls.Add(call);
                }
            }
            else
            {
                OnResponseFromCall(this, new ResponseCallEventArgs(CallState.IsEnd));
            }
        }

        private bool CheckForBusyNumbers(IPhoneNumber phoneNumber)
        {
            var allCalls = WaitingCalls.Union(InProgressCalls);
            var busyPhoneNumbers = allCalls
                .Select(c => c.Caller.Number)
                .Union(allCalls
                    .Select(c => c.Receiver.Number));

            return busyPhoneNumbers.Contains(phoneNumber.Number);
        }

        public void OnCallChangeState(object sender, StationCallingEventArgs args)
        {
            if (_waitingCalls.Contains(args.Call))
            {
                if (args.Call.CallState is CallState.InProgress)
                {
                    _inProgressCalls.Add(args.Call);

                    OnResponseFromCall(sender, new ResponseCallEventArgs(CallState.InProgress));
                }

                _waitingCalls.Remove(args.Call);

                if (!_inProgressCalls.Contains(args.Call))
                {
                    OnResponseFromCall(sender, new ResponseCallEventArgs(CallState.IsEnd));
                    OnCallReport(this, new StationReportEventArgs(args.Call));
                }
            }
            else if (_inProgressCalls.Contains(args.Call))
            {
                _inProgressCalls.Remove(args.Call);

                OnResponseFromCall(sender, new ResponseCallEventArgs(CallState.IsEnd));
                OnCallReport(this, new StationReportEventArgs(args.Call));
            }
        }

        protected virtual void OnResponseFromCall(object sender, ResponseCallEventArgs args)
        {
            ResponseFromCall?.Invoke(sender, args);
        }

        protected virtual void OnCallReport(object sender, StationReportEventArgs args)
        {
            CallReport?.Invoke(sender, args);
        }

        private IPort FindPortViaPhoneNumber(IPhoneNumber phoneNumber)
        {
            return PortController.Ports
                .Single(p => p.Phone.PhoneNumber.Number.Equals(phoneNumber.Number));
        }
    }
}