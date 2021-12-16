using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.Phones
{
    public class Phone : IPhone
    {
        public event EventHandler<StartingCallEventArgs> StartCall;
        public event EventHandler<ConnectionStateEventArgs> ChangeConnection;

        public IPhoneNumber PhoneNumber { get; }

        public ITariffPlan TariffPlan { get; }

        public ConnectionState ConnectionState { get; private set; }

        public Phone(ITariffPlan tariffPlan, IPhoneNumber phoneNumber)
        {
            TariffPlan = tariffPlan;
            PhoneNumber = phoneNumber;

            ConnectionState = ConnectionState.Disconnected;
        }

        public void Call(IPhoneNumber phoneNumber)
        {
            OnStartCall(this, new StartingCallEventArgs(PhoneNumber, phoneNumber));
        }

        public void ConnectToPort()
        {
            ConnectionState = ConnectionState.Connected;

            OnConnectionChange(this, new ConnectionStateEventArgs(ConnectionState.Connected));
        }

        public void DisconnectFromPort()
        {
            ConnectionState = ConnectionState.Disconnected;

            OnConnectionChange(this, new ConnectionStateEventArgs(ConnectionState.Disconnected));
        }

        protected virtual void OnStartCall(object sender, StartingCallEventArgs args)
        {
            StartCall?.Invoke(sender, args);
        }

        protected virtual void OnConnectionChange(object sender, ConnectionStateEventArgs args)
        {
            ChangeConnection?.Invoke(sender, args);
        }
    }
}