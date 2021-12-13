using System;

namespace AutomaticTelephoneStation.PhoneNumbers
{
    public class PhoneNumber : IPhoneNumber
    {
        private readonly string _countryCode = "+375";
        private readonly string _operatorCode = "77";

        public string Number { get; }

        public PhoneNumber(string number)
        {
            if (number.Length != 7)
            {
                throw new ArgumentException("number length isn't 7!");
            }

            Number = string.Concat(_countryCode, _operatorCode, number);
        }
    }
}