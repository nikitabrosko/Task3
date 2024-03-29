﻿using System;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.Phones;

namespace AutomaticTelephoneStation.Ports
{
    public interface IPort
    {
        event EventHandler<StartingCallEventArgs> OutgoingCall;
        event EventHandler<StationCallingEventArgs> IncomingCall;
        event EventHandler<StationCallingEventArgs> CallChangeState;
        event EventHandler<ResponseCallEventArgs> ResponseFromStation;
        event EventHandler<PortReportEventArgs> CallReport;
        PortState State { get; }
        ConnectionState ConnectionState { get; }
        IPhone Phone { get; }
        void OnResponseFromCall(object sender, ResponseCallEventArgs args);
        void OnPhoneStartingCall(object sender, StartingCallEventArgs args);
        void OnConnectionChange(object sender, ConnectionStateEventArgs args);
        void OnPhoneCallingByStation(object sender, StationCallingEventArgs args);
        void OnCallChangeStateFromPhone(object sender, StationCallingEventArgs args);
        void OnCallReportFromStation(object sender, StationReportEventArgs args);
    }
}