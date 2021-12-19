namespace AutomaticTelephoneStation.PhoneNumbers
{
    public interface ICountryPhoneNumber : IPhoneNumber
    {
        string CountryCode { get; }
    }
}