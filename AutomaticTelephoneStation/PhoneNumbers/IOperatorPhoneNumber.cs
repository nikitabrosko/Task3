namespace AutomaticTelephoneStation.PhoneNumbers
{
    public interface IOperatorPhoneNumber : ICountryPhoneNumber
    {
        string OperatorCode { get; }
    }
}