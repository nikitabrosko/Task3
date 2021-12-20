using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.Stations
{
    public class Station : IStation
    {
        public event EventHandler<ResponseCallEventArgs> ResponseFromCall;

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
            if (!args.TargetPhoneNumber.Number.StartsWith(string.Concat("+", ((int)_countryCode).ToString())) 
                || !args.SourcePhoneNumber.Number.StartsWith(string.Concat("+", ((int)_countryCode).ToString())))
            {
                OnResponseFromCall(this, new ResponseCallEventArgs(CallState.IsEnd));

                return;
            }

            var call = new Call(args.SourcePhoneNumber, args.TargetPhoneNumber);

            PortController.Ports
                .Single(p => p.Phone.PhoneNumber.Number.Equals(args.TargetPhoneNumber.Number))
                .OnPhoneCallingByStation(sender, new StationCallingEventArgs(call));

            _waitingCalls.Add(call);
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
                }
            }
            else if (_inProgressCalls.Contains(args.Call))
            {
                if (args.Call.CallState is CallState.IsEnd)
                {
                    // Billing system coming soon
                }

                _inProgressCalls.Remove(args.Call);

                OnResponseFromCall(sender, new ResponseCallEventArgs(CallState.IsEnd));
            }
        }

        protected virtual void OnResponseFromCall(object sender, ResponseCallEventArgs args)
        {
            ResponseFromCall?.Invoke(sender, args);
        }
    }
}