using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers
{
    public class UsaPhoneNumber : PhoneNumberBase
    {
        public UsaOperatorCode OperatorCode { get; }

        public UsaPhoneNumber(UsaOperatorCode operatorCode, ITariffPlan tariffPlan, string number) 
            : base(tariffPlan, number)
        {
            CountryCode = CountryCode.Usa;
            OperatorCode = operatorCode;

            Number = string.Concat("+", (int)CountryCode, (int)OperatorCode, number);
        }
    }
}