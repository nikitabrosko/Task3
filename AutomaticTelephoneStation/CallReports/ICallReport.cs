using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.CallReports
{
    public interface ICallReport
    {
        IPhoneNumber PhoneNumber { get; }
        int CallDuration { get; }
    }
}
