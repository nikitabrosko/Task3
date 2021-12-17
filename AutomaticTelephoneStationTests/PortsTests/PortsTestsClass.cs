using System;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using AutomaticTelephoneStation.TariffPlans;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.PortsTests
{
    [TestClass]
    public class PortsTestsClass
    {
        [TestMethod]
        public void TestCreatingPortClassWithValidParameters()
        {
            var phoneObject = new Phone(new LowTariffPlan(), new PhoneNumber("1234567"));
            var actualPortObject = new Port(phoneObject);

            Assert.IsTrue(actualPortObject.ConnectionState.Equals(ConnectionState.Disconnected) 
                          && actualPortObject.State.Equals(PortState.Free)
                          && actualPortObject.Phone.Equals(phoneObject));
        }

        [TestMethod]
        public void TestCreatingPortClassWithInvalidParameters()
        {
            IPhone phoneObject = null;

            Assert.ThrowsException<ArgumentNullException>(() => new Port(phoneObject));
        }

        [TestMethod]
        public void TestOnPhoneCallingByStationMethod()
        {
            var stationObject = new Station();
            var stationCallingEventArgsObject =
                new StationCallingEventArgs(
                    new Call(
                        new PhoneNumber("1234567"), 
                        new PhoneNumber("7654321")));
            var phoneObject = new Phone(new LowTariffPlan(), new PhoneNumber("1234567"));
            var portObject = new Port(phoneObject);
            portObject.Phone.ConnectToPort();

            portObject.OnPhoneCallingByStation(stationObject, stationCallingEventArgsObject);

            Assert.IsTrue(portObject.State is PortState.Free);
        }

        [TestMethod]
        public void TestOnPhoneStartingCallMethod()
        {
            var stationObject = new Station();
            var stationCallingEventArgsObject =
                new StartingCallEventArgs(
                    new PhoneNumber("1234567"), 
                    new PhoneNumber("7654321"));
            var phoneObject = new Phone(new LowTariffPlan(), new PhoneNumber("1234567"));
            var portObject = new Port(phoneObject);
            portObject.Phone.ConnectToPort();

            portObject.OnPhoneStartingCall(stationObject, stationCallingEventArgsObject);

            Assert.IsTrue(portObject.State is PortState.Free);
        }

        [TestMethod]
        public void TestOnCallChangeStateMethod()
        {
            var stationObject = new Station();
            var stationCallingEventArgsObject =
                new StationCallingEventArgs(
                    new Call(
                        new PhoneNumber("1234567"),
                        new PhoneNumber("7654321")));
            var phoneObject = new Phone(new LowTariffPlan(), new PhoneNumber("1234567"));
            var portObject = new Port(phoneObject);
            portObject.Phone.ConnectToPort();

            portObject.Phone.OnIncomingCall(portObject, stationCallingEventArgsObject);
            portObject.Phone.AcceptCall();

            Assert.IsTrue(stationCallingEventArgsObject.Call.CallState is CallState.InProgress);
        }
    }
}