using System;

namespace AutomaticTelephoneStation.PhoneNumbers.BelarusPhoneNumbers
{
    public class BelarusPhoneNumber : ICountryPhoneNumber
    {
        public string CountryCode => "+375";

        public string Number { get; protected set; }

        public BelarusPhoneNumber(string number)
        {
            if (number.Length != 9)
            {
                throw new ArgumentException("number length isn't 9!");
            }

            Number = string.Concat(CountryCode, number);
        }
    }
}
