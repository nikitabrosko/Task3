using System;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.Calls
{
    public interface ICall
    {
        IPhoneNumber Caller { get; }
        IPhoneNumber Receiver { get; }
        CallState CallState { get; set; }
        DateTime CallDate { get; }
        int Duration { get; }
        void StartStopwatch();
        void StopStopwatch();
    }
}
