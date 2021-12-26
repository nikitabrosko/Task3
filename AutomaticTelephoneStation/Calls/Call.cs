using System;
using System.Diagnostics;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.Calls
{
    public class Call : ICall
    {
        private Stopwatch _stopwatch;

        public IPhoneNumber Caller { get; }

        public IPhoneNumber Receiver { get; }

        public CallState CallState { get; set; }

        public DateTime CallDate { get; }

        public int Duration { get; protected set; }

        public Call(IPhoneNumber caller, IPhoneNumber receiver)
        {
            Caller = caller;
            Receiver = receiver;
            CallState = CallState.IsWaiting;
            CallDate = DateTime.Now;
            Duration = 0;
        }

        public void StartStopwatch()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void StopStopwatch()
        {
            _stopwatch.Stop();
            Duration = ((int)_stopwatch.Elapsed.TotalSeconds);
        }
    }
}