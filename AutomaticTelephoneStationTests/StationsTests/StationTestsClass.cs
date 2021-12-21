using System.Linq;
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

namespace AutomaticTelephoneStationTests.StationsTests
{
    [TestClass]
    public class StationTestsClass
    {
        [TestMethod]
        public void StationClassCreatingWithValidParameters()
        {
            var stationObject = new Station(CountryCode.Belarus);
            var portObjectFirst = new Port(
                new Phone(
                    new LowTariffPlan(), 
                    new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567")),
                stationObject);

            var portObjectSecond = new Port(
                new Phone(
                    new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567")),
                stationObject);
            var expectedPortController = new PortController();
            expectedPortController.AddPort(portObjectFirst);
            expectedPortController.AddPort(portObjectSecond);

            var actualPortController = stationObject.PortController;

            Assert.IsTrue(expectedPortController.Ports.First().ConnectionState
                .Equals(actualPortController.Ports.First().ConnectionState) 
                          && expectedPortController.Ports.First().State
                              .Equals(actualPortController.Ports.First().State));
        }

        [TestMethod]
        public void OnPhoneStartingCallMethodTests()
        {
            var stationObject = new Station(CountryCode.Belarus);
            var callingPhone = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));
            var targetPhone = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321"));
            var callingPortObject = new Port(callingPhone, stationObject);
            var targetPortObject = new Port(targetPhone, stationObject);
            var startingCallEventArgs = new StartingCallEventArgs(
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321"));

            stationObject.OnPhoneStartingCall(callingPortObject, startingCallEventArgs);
            var expectedCall = new Call(
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321"));
            var actualCall = stationObject.WaitingCalls.First();

            Assert.IsTrue(actualCall.Caller.Number.Equals(expectedCall.Caller.Number) 
                          && actualCall.Receiver.Number.Equals(expectedCall.Receiver.Number)
                          && actualCall.CallState.Equals(expectedCall.CallState)
                          && actualCall.Duration.Equals(expectedCall.Duration));
        }

        [TestMethod]
        public void OnCallChangeStateMethodTestsInWaiting()
        {
            var stationObject = new Station(CountryCode.Belarus);
            var callingPhone = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));
            var targetPhone = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321"));
            var callingPortObject = new Port(callingPhone, stationObject);
            var targetPortObject = new Port(targetPhone, stationObject);
            var startingCallEventArgs = new StartingCallEventArgs(
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321"));

            stationObject.OnPhoneStartingCall(callingPortObject, startingCallEventArgs);
            var stationCallingEventArgsObject = new StationCallingEventArgs(stationObject.WaitingCalls.First());
            stationCallingEventArgsObject.Call.CallState = CallState.InProgress;
            stationObject.OnCallChangeState(targetPortObject, stationCallingEventArgsObject);

            Assert.IsTrue(stationObject.InProgressCalls.Contains(stationCallingEventArgsObject.Call));
        }

        [TestMethod]
        public void OnCallChangeStateMethodTestsInProgress()
        {
            var stationObject = new Station(CountryCode.Belarus);
            var callingPhone = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));
            var targetPhone = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321"));
            var callingPortObject = new Port(callingPhone, stationObject);
            var targetPortObject = new Port(targetPhone, stationObject);
            var startingCallEventArgs = new StartingCallEventArgs(
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321"));

            stationObject.OnPhoneStartingCall(callingPortObject, startingCallEventArgs);
            var stationCallingEventArgsObject = new StationCallingEventArgs(stationObject.WaitingCalls.First());
            stationCallingEventArgsObject.Call.CallState = CallState.InProgress;
            stationObject.OnCallChangeState(targetPortObject, stationCallingEventArgsObject);
            stationCallingEventArgsObject.Call.CallState = CallState.IsEnd;
            stationObject.OnCallChangeState(targetPortObject, stationCallingEventArgsObject);

            Assert.IsTrue(!stationObject.InProgressCalls.Contains(stationCallingEventArgsObject.Call));
        }

        [TestMethod]
        public void OnPhoneStartingCallMethodTestsPhoneNumberIsAnotherCountry()
        {
            var stationObject = new Station(CountryCode.Belarus);
            var callingPhone = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));
            var targetPhone = new Phone(new LowTariffPlan(), new RussiaPhoneNumber(RussiaOperatorCode.Mts, "7654321"));
            var callingPortObject = new Port(callingPhone, stationObject);
            callingPortObject.Phone.ConnectToPort();
            var targetPortObject = new Port(targetPhone, stationObject);
            targetPortObject.Phone.ConnectToPort();

            callingPortObject.Phone.Call(targetPortObject.Phone.PhoneNumber);

            Assert.IsTrue(callingPortObject.Phone.PhoneCallState is PhoneCallState.Silence);
        }
    }
}