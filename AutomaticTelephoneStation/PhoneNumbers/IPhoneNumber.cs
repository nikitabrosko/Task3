namespace AutomaticTelephoneStation.PhoneNumbers
{
    public interface IPhoneNumber
    {
        CountryCode CountryCode { get; }
        string Number { get; }
    }
}