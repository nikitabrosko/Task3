using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;

namespace AutomaticTelephoneStation.Stations
{
    public interface IStation
    {
        void OnPhoneStartingCall(object sender, StartingCallEventArgs args);
    }
}