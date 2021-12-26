using System;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public class CallerCallReport : ICallReport
    {
        public IPhoneNumber PhoneNumber { get; }

        public DateTime CallDate { get; }

        public int CallDuration { get; }

        public decimal Fee { get; protected set; }

        public CallerCallReport(IPhoneNumber callerPhoneNumber, IPhoneNumber receiverPhoneNumber, DateTime callDate, int callDuration)
        {
            PhoneNumber = receiverPhoneNumber ?? throw new ArgumentNullException(nameof(receiverPhoneNumber));
            CallDuration = callDuration;
            CallDate = callDate;
            Fee = (callerPhoneNumber 
                ?? throw new ArgumentNullException(nameof(receiverPhoneNumber)))
                .TariffPlan.Fee * CallDuration;
        }
    }
}