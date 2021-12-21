using System;
using System.Linq;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.PhoneNumbers
{
    public class PhoneNumberBase : IPhoneNumber
    {
        public CountryCode CountryCode { get; protected set; }

        public ITariffPlan TariffPlan { get; }

        public string Number { get; protected set; }

        protected PhoneNumberBase(ITariffPlan tariffPlan, string number)
        {
            if (number.Length != 7)
            {
                throw new ArgumentException("number length should be equal 7");
            }

            if (number.Any(character => !char.IsDigit(character)))
            {
                throw new ArgumentException("some of the characters in number is not a digits");
            }

            TariffPlan = tariffPlan;
        }
    }
}