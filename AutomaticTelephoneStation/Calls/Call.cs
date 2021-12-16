using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Subscribers;

namespace AutomaticTelephoneStation.Calls
{
    public class Call : ICall
    {
        public IPhoneNumber Caller { get; }

        public IPhoneNumber Receiver { get; }

        public CallState CallState { get; set; }

        public int Duration { get; }

        public Call(IPhoneNumber caller, IPhoneNumber receiver)
        {
            Caller = caller;
            Receiver = receiver;
            CallState = CallState.IsWaiting;
            Duration = 0;
        }

        public void StartStopwatch()
        {

        }

        public void StopStopwatch()
        {

        }
    }
}