using System;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.CallReports
{
    public class CallerCallReport : ICallReport
    {
        public IPhoneNumber PhoneNumber { get; }

        public int CallDuration { get; }

        public decimal Fee { get; protected set; }

        public CallerCallReport(IPhoneNumber callerPhoneNumber, IPhoneNumber receiverPhoneNumber, int callDuration)
        {
            PhoneNumber = receiverPhoneNumber ?? throw new ArgumentNullException(nameof(receiverPhoneNumber));
            CallDuration = callDuration;
            Fee = (callerPhoneNumber 
                ?? throw new ArgumentNullException(nameof(receiverPhoneNumber)))
                .TariffPlan.Fee * CallDuration;
        }
    }
}