using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.Subscribers
{
    public interface ISubscriber
    {
        string FirstName { get; }
        string LastName { get; }
        IPhone Phone { get; }
    }
}