using System;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
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
            var stationObject = new Station(CountryCode.Belarus);

            var phoneObject = new Phone(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"));
            var actualPortObject = new Port(phoneObject, stationObject);

            Assert.IsTrue(actualPortObject.ConnectionState.Equals(ConnectionState.Disconnected) 
                          && actualPortObject.State.Equals(PortState.Free)
                          && actualPortObject.Phone.Equals(phoneObject));
        }

        [TestMethod]
        public void TestCreatingPortClassWithInvalidParametersPhoneIsNull()
        {
            var mock = new Mock<IStation>();

            IPhone phoneObject = null;

            Assert.ThrowsException<ArgumentNullException>(() => new Port(phoneObject, mock.Object));
        }

        [TestMethod]
        public void TestCreatingPortClassWithInvalidParametersStationIsNull()
        {
            IStation stationObject = null;

            var mock = new Mock<IPhone>();

            Assert.ThrowsException<ArgumentNullException>(() => new Port(mock.Object, stationObject));
        }

        [TestMethod]
        public void TestOnPhoneCallingByStationMethod()
        {
            var stationObject = new Station(CountryCode.Belarus);
            var stationCallingEventArgsObject =
                new StationCallingEventArgs(
                    new Call(
                        new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"),
                        new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321")));
            var phoneObject = new Phone(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"));
            var portObject = new Port(phoneObject, stationObject);
            portObject.Phone.ConnectToPort();

            portObject.OnPhoneCallingByStation(stationObject, stationCallingEventArgsObject);

            Assert.IsTrue(portObject.State is PortState.Busy);
        }

        [TestMethod]
        public void TestOnPhoneStartingCallMethod()
        {
            var stationObject = new Station(CountryCode.Belarus);
            var stationCallingEventArgsObject =
                new StartingCallEventArgs(
                    new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"),
                    new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "7654321"));
            var phoneObjectCaller = new Phone(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"));
            var portObjectCaller = new Port(phoneObjectCaller, stationObject);
            var phoneObjectReceiver =
                new Phone(new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "7654321"));
            var portObjectReceiver = new Port(phoneObjectReceiver, stationObject);

            portObjectCaller.Phone.ConnectToPort();

            portObjectCaller.OnPhoneStartingCall(portObjectCaller.Phone, stationCallingEventArgsObject);

            Assert.IsTrue(portObjectCaller.State is PortState.Busy);
        }

        [TestMethod]
        public void TestOnCallChangeStateMethod()
        {
            var stationObject = new Station(CountryCode.Belarus);

            var stationCallingEventArgsObject =
                new StationCallingEventArgs(
                    new Call(
                        new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"),
                        new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321")));
            var phoneObject = new Phone(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"));
            var portObject = new Port(phoneObject, stationObject);
            portObject.Phone.ConnectToPort();

            portObject.Phone.OnIncomingCall(portObject, stationCallingEventArgsObject);
            portObject.Phone.AcceptCall();

            Assert.IsTrue(stationCallingEventArgsObject.Call.CallState is CallState.InProgress);
        }
    }
}