using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public interface ICallReport
    {
        IPhoneNumber PhoneNumber { get; }
        int CallDuration { get; }
    }
}
