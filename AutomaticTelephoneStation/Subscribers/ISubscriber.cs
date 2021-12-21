using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;

namespace AutomaticTelephoneStation.Subscribers
{
    public interface ISubscriber
    {
        string FirstName { get; }
        string LastName { get; }
        IPhone Phone { get; }
        IPort Port { get; }
    }
}