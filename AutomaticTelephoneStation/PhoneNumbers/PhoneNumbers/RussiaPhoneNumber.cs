﻿using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;

namespace AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers
{
    public class RussiaPhoneNumber : PhoneNumberBase
    {
        public RussiaOperatorCode OperatorCode { get; }

        public RussiaPhoneNumber(RussiaOperatorCode operatorCode, string number) : base(number)
        {
            CountryCode = CountryCode.Russia;
            OperatorCode = operatorCode;

            Number = string.Concat("+", (int)CountryCode, (int)OperatorCode, number);
        }
    }
}