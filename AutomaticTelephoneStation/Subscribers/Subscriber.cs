using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.Subscribers
{
    public class Subscriber : ISubscriber
    {

        public string FirstName { get; }

        public string LastName { get; }

        public IPhone Phone { get; }

        public Subscriber(string firstName, string lastName, IPhone phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }
    }
}