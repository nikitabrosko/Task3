using System;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public interface ICallReport
    {
        IPhoneNumber PhoneNumber { get; }
        DateTime CallDate { get; }
        int CallDuration { get; }
    }
}
