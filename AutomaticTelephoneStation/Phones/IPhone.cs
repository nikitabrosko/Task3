using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.Phones
{
    public interface IPhone
    {
        event EventHandler<StartingCallEventArgs> StartCall;
        public event EventHandler<ConnectionStateEventArgs> ChangeConnection;
        IPhoneNumber PhoneNumber { get; }
        ITariffPlan TariffPlan { get; }
        ConnectionState ConnectionState { get; }
        void Call(IPhoneNumber phoneNumber);
        public void ConnectToPort();
        public void DisconnectFromPort();
    }
}