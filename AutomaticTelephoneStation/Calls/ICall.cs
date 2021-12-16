using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Subscribers;

namespace AutomaticTelephoneStation.Calls
{
    public interface ICall
    {
        IPhoneNumber Caller { get; }
        IPhoneNumber Receiver { get; }
        CallState CallState { get; set; }
        int Duration { get; }
        void StartStopwatch();
        void StopStopwatch();
    }
}
