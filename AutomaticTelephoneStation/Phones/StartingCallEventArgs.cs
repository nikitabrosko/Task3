using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.Phones
{
    public class StartingCallEventArgs
    {
        public IPhoneNumber SourcePhoneNumber { get; set; }
        public IPhoneNumber TargetPhoneNumber { get; set; }

        public StartingCallEventArgs(IPhoneNumber sourcePhoneNumber, IPhoneNumber targetPhoneNumber)
        {
            SourcePhoneNumber = sourcePhoneNumber;
            TargetPhoneNumber = targetPhoneNumber;
        }
    }
}
