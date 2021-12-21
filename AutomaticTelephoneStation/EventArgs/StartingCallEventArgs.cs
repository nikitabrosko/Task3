using System;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.EventArgs
{
    public class StartingCallEventArgs
    {
        public IPhoneNumber SourcePhoneNumber { get; set; }
        public IPhoneNumber TargetPhoneNumber { get; set; }

        public StartingCallEventArgs(IPhoneNumber sourcePhoneNumber, IPhoneNumber targetPhoneNumber)
        {
            SourcePhoneNumber = sourcePhoneNumber ?? throw new ArgumentNullException(nameof(sourcePhoneNumber));
            TargetPhoneNumber = targetPhoneNumber ?? throw new ArgumentNullException(nameof(targetPhoneNumber));
        }
    }
}
