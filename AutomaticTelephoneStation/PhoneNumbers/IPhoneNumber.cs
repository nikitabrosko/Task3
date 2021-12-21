using AutomaticTelephoneStation.BillingSystem.TariffPlans;

namespace AutomaticTelephoneStation.PhoneNumbers
{
    public interface IPhoneNumber
    {
        CountryCode CountryCode { get; }
        ITariffPlan TariffPlan { get; }
        string Number { get; }
    }
}