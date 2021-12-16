using System;
using System.Collections.Generic;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using AutomaticTelephoneStation.Calls;

namespace AutomaticTelephoneStation.Stations
{
    public class Station : IStation
    {
        private readonly IList<ICall> _waitingCalls = new List<ICall>();
        private readonly IList<ICall> _inProgressCalls = new List<ICall>();

        public PortController PortController { get; }

        public Station()
        {
            PortController = new PortController();
        }

        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            var call = new Call(args.SourcePhoneNumber, args.TargetPhoneNumber);

            PortController.Ports
                .Last()
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
                }

                _waitingCalls.Remove(args.Call);
            }
            else if (_inProgressCalls.Contains(args.Call))
            {
                if (args.Call.CallState is CallState.IsEnd)
                {
                    
                }

                _inProgressCalls.Remove(args.Call);
            }
        }
    }
}