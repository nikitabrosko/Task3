using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.Phones
{
    public class Phone : IPhone
    {
        public event EventHandler<StartingCallEventArgs> StartCall;

        public IPhoneNumber PhoneNumber { get; }

        public ITariffPlan TariffPlan { get; }

        public Phone(ITariffPlan tariffPlan, IPhoneNumber phoneNumber)
        {
            TariffPlan = tariffPlan;
            PhoneNumber = phoneNumber;
        }

        public void Call(IPhoneNumber phoneNumber)
        {
            OnStartCall(this, new StartingCallEventArgs(PhoneNumber, phoneNumber));
        }

        protected virtual void OnStartCall(object sender, StartingCallEventArgs args)
        {
            StartCall?.Invoke(sender, args);
        }
    }
}