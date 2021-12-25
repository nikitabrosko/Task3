using System;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public class ReceiverCallReport : ICallReport
    {
        public IPhoneNumber PhoneNumber { get; }
        public int CallDuration { get; }

        public ReceiverCallReport(IPhoneNumber callerPhoneNumber, int callDuration)
        {
            PhoneNumber = callerPhoneNumber ?? throw new ArgumentNullException(nameof(callerPhoneNumber));
            CallDuration = callDuration;
        }
    }
}