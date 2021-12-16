﻿using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.TariffPlans;

namespace AutomaticTelephoneStation.Subscribers
{
    public class Subscriber : ISubscriber
    {

        public string FirstName { get; }

        public string LastName { get; }

        public IPort Port { get; }

        public IPhone Phone { get; }

        public Subscriber(string firstName, string lastName, IPort port, IPhone phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Port = port;
            Phone = phone;

            Phone.StartCall += Port.OnPhoneStartingCall;
            Phone.ChangeConnection += Port.OnConnectionChange;
        }
    }
}