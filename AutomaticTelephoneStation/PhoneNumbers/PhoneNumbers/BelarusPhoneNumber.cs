using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;

namespace AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers
{
    public class BelarusPhoneNumber : PhoneNumberBase
    {
        public BelarusOperatorCode OperatorCode { get; }

        public BelarusPhoneNumber(BelarusOperatorCode operatorCode, string number) : base(number)
        {
            CountryCode = CountryCode.Belarus;
            OperatorCode = operatorCode;

            Number = string.Concat("+", (int)CountryCode, (int)OperatorCode, number);
        }
    }
}