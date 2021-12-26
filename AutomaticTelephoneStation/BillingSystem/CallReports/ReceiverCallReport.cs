using System;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public class ReceiverCallReport : ICallReport
    {
        public IPhoneNumber PhoneNumber { get; }

        public DateTime CallDate { get; }

        public int CallDuration { get; }

        public ReceiverCallReport(IPhoneNumber callerPhoneNumber, DateTime callDate, int callDuration)
        {
            PhoneNumber = callerPhoneNumber ?? throw new ArgumentNullException(nameof(callerPhoneNumber));
            CallDate = callDate;
            CallDuration = callDuration;
        }
    }
}