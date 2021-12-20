using System;
using System.Linq;

namespace AutomaticTelephoneStation.PhoneNumbers
{
    public class PhoneNumberBase : IPhoneNumber
    {
        public CountryCode CountryCode { get; protected set; }

        public string Number { get; protected set; }

        protected PhoneNumberBase(string number)
        {
            if (number.Length != 7)
            {
                throw new ArgumentException("number length should be equal 7");
            }

            if (number.Any(character => !char.IsDigit(character)))
            {
                throw new ArgumentException("some of the characters in number is not a digits");
            }
        }
    }
}