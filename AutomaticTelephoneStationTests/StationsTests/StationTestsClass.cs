using System.Linq;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.BelarusPhoneNumbers;
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
            var stationObject = new Station("+375", "77");
            stationObject.PortController.AddPort(new Port(new Phone(new LowTariffPlan(), new TaskOperatorPhoneNumber("1234567"))));

            var expectedPortController = new PortController();
            expectedPortController.AddPort(new Port(new Phone(new LowTariffPlan(), new TaskOperatorPhoneNumber("1234567"))));
            var actualPortController = stationObject.PortController;

            Assert.IsTrue(expectedPortController.Ports.First().ConnectionState
                .Equals(actualPortController.Ports.First().ConnectionState) 
                          && expectedPortController.Ports.First().State
                              .Equals(actualPortController.Ports.First().State));
        }

        [TestMethod]
        public void OnPhoneStartingCallMethodTests()
        {
            var stationObject = new Station("+375", "77");
            var callingPhone = new Phone(new LowTariffPlan(), new TaskOperatorPhoneNumber("1234567"));
            var targetPhone = new Phone(new LowTariffPlan(), new TaskOperatorPhoneNumber("7654321"));
            var callingPortObject = new Port(callingPhone);
            var targetPortObject = new Port(targetPhone);
            stationObject.PortController.AddPort(callingPortObject);
            stationObject.PortController.AddPort(targetPortObject);
            var startingCallEventArgs = new StartingCallEventArgs(
                new TaskOperatorPhoneNumber("1234567"), 
                new TaskOperatorPhoneNumber("7654321"));

            stationObject.OnPhoneStartingCall(callingPortObject, startingCallEventArgs);
            var expectedCall = new Call(new TaskOperatorPhoneNumber("1234567"), new TaskOperatorPhoneNumber("7654321"));
            var actualCall = stationObject.WaitingCalls.First();

            Assert.IsTrue(actualCall.Caller.Number.Equals(expectedCall.Caller.Number) 
                          && actualCall.Receiver.Number.Equals(expectedCall.Receiver.Number)
                          && actualCall.CallState.Equals(expectedCall.CallState)
                          && actualCall.Duration.Equals(expectedCall.Duration));
        }

        [TestMethod]
        public void OnCallChangeStateMethodTestsInWaiting()
        {
            var stationObject = new Station("+375", "77");
            var callingPhone = new Phone(new LowTariffPlan(), new TaskOperatorPhoneNumber("1234567"));
            var targetPhone = new Phone(new LowTariffPlan(), new TaskOperatorPhoneNumber("7654321"));
            var callingPortObject = new Port(callingPhone);
            var targetPortObject = new Port(targetPhone);
            stationObject.PortController.AddPort(callingPortObject);
            stationObject.PortController.AddPort(targetPortObject);
            var startingCallEventArgs = new StartingCallEventArgs(
                new TaskOperatorPhoneNumber("1234567"),
                new TaskOperatorPhoneNumber("7654321"));

            stationObject.OnPhoneStartingCall(callingPortObject, startingCallEventArgs);
            var stationCallingEventArgsObject = new StationCallingEventArgs(stationObject.WaitingCalls.First());
            stationCallingEventArgsObject.Call.CallState = CallState.InProgress;
            stationObject.OnCallChangeState(targetPortObject, stationCallingEventArgsObject);

            Assert.IsTrue(stationObject.InProgressCalls.Contains(stationCallingEventArgsObject.Call));
        }

        [TestMethod]
        public void OnCallChangeStateMethodTestsInProgress()
        {
            var stationObject = new Station("+375", "77");
            var callingPhone = new Phone(new LowTariffPlan(), new TaskOperatorPhoneNumber("1234567"));
            var targetPhone = new Phone(new LowTariffPlan(), new TaskOperatorPhoneNumber("7654321"));
            var callingPortObject = new Port(callingPhone);
            var targetPortObject = new Port(targetPhone);
            stationObject.PortController.AddPort(callingPortObject);
            stationObject.PortController.AddPort(targetPortObject);
            var startingCallEventArgs = new StartingCallEventArgs(
                new TaskOperatorPhoneNumber("1234567"),
                new TaskOperatorPhoneNumber("7654321"));

            stationObject.OnPhoneStartingCall(callingPortObject, startingCallEventArgs);
            var stationCallingEventArgsObject = new StationCallingEventArgs(stationObject.WaitingCalls.First());
            stationCallingEventArgsObject.Call.CallState = CallState.InProgress;
            stationObject.OnCallChangeState(targetPortObject, stationCallingEventArgsObject);
            stationCallingEventArgsObject.Call.CallState = CallState.IsEnd;
            stationObject.OnCallChangeState(targetPortObject, stationCallingEventArgsObject);

            Assert.IsTrue(!stationObject.InProgressCalls.Contains(stationCallingEventArgsObject.Call));
        }
    }
}