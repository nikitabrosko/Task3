using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;

namespace AutomaticTelephoneStation.Stations
{
    public class Station : IStation
    {
        public void OnPhoneStartingCall(object sender, StartingCallEventArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}