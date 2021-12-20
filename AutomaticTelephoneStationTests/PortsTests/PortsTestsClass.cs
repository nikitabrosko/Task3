using System;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using AutomaticTelephoneStation.TariffPlans;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutomaticTelephoneStationTests.PortsTests
{
    [TestClass]
    public class PortsTestsClass
    {
        [TestMethod]
        public void TestCreatingPortClassWithValidParameters()
        {
            var mock = new Mock<IStation>();

            var phoneObject = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));
            var actualPortObject = new Port(phoneObject, mock.Object);

            Assert.IsTrue(actualPortObject.ConnectionState.Equals(ConnectionState.Disconnected) 
                          && actualPortObject.State.Equals(PortState.Free)
                          && actualPortObject.Phone.Equals(phoneObject));
        }

        [TestMethod]
        public void TestCreatingPortClassWithInvalidParameters()
        {
            var mock = new Mock<IStation>();

            IPhone phoneObject = null;

            Assert.ThrowsException<ArgumentNullException>(() => new Port(phoneObject, mock.Object));
        }

        [TestMethod]
        public void TestOnPhoneCallingByStationMethod()
        {
            var mock = new Mock<IStation>();

            var stationObject = new Station(CountryCode.Belarus);
            var stationCallingEventArgsObject =
                new StationCallingEventArgs(
                    new Call(
                        new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"),
                        new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321")));
            var phoneObject = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));
            var portObject = new Port(phoneObject, mock.Object);
            portObject.Phone.ConnectToPort();

            portObject.OnPhoneCallingByStation(stationObject, stationCallingEventArgsObject);

            Assert.IsTrue(portObject.State is PortState.Busy);
        }

        [TestMethod]
        public void TestOnPhoneStartingCallMethod()
        {
            var mock = new Mock<IStation>();

            var stationObject = new Station(CountryCode.Belarus);
            var stationCallingEventArgsObject =
                new StartingCallEventArgs(
                    new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"),
                    new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321"));
            var phoneObject = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));
            var portObject = new Port(phoneObject, mock.Object);
            portObject.Phone.ConnectToPort();

            portObject.OnPhoneStartingCall(stationObject, stationCallingEventArgsObject);

            Assert.IsTrue(portObject.State is PortState.Busy);
        }

        [TestMethod]
        public void TestOnCallChangeStateMethod()
        {
            var mock = new Mock<IStation>();

            var stationCallingEventArgsObject =
                new StationCallingEventArgs(
                    new Call(
                        new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"),
                        new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321")));
            var phoneObject = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));
            var portObject = new Port(phoneObject, mock.Object);
            portObject.Phone.ConnectToPort();

            portObject.Phone.OnIncomingCall(portObject, stationCallingEventArgsObject);
            portObject.Phone.AcceptCall();

            Assert.IsTrue(stationCallingEventArgsObject.Call.CallState is CallState.InProgress);
        }
    }
}