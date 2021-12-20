using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;

namespace AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers
{
    public class UsaPhoneNumber : PhoneNumberBase
    {
        public UsaOperatorCode OperatorCode { get; }

        public UsaPhoneNumber(UsaOperatorCode operatorCode, string number) : base(number)
        {
            CountryCode = CountryCode.Usa;
            OperatorCode = operatorCode;

            Number = string.Concat("+", (int)CountryCode, (int)OperatorCode, number);
        }
    }
}