using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.Phones
{
    public interface IPhone
    {
        event EventHandler<StartingCallEventArgs> StartCall;
        IPhoneNumber PhoneNumber { get; }
        ITariffPlan TariffPlan { get; }
        void Call(IPhoneNumber phoneNumber);
    }
}