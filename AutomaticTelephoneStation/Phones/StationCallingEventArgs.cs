using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.Phones
{
    public class StationCallingEventArgs
    {
        public ICall Call { get; set; }

        public StationCallingEventArgs(ICall call)
        {
            Call = call;
        }
    }
}