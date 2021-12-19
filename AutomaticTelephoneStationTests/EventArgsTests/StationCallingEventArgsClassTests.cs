using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using AutomaticTelephoneStation.PhoneNumbers.BelarusPhoneNumbers;

namespace AutomaticTelephoneStationTests.EventArgsTests
{
    [TestClass]
    public class StationCallingEventArgsClassTests
    {
        [TestMethod]
        public void StationCallingEventArgsClassCreatingWithValidParameters()
        {
            var stationCallingEventArgsObject = new StationCallingEventArgs(
                new Call(new TaskOperatorPhoneNumber("1234567"), new TaskOperatorPhoneNumber("7654321")));

            var expectedCall = new Call(new TaskOperatorPhoneNumber("1234567"), new TaskOperatorPhoneNumber("7654321"));
            var actualCall = stationCallingEventArgsObject.Call;

            Assert.IsTrue(expectedCall.Caller.Number.Equals(actualCall.Caller.Number) 
                          && expectedCall.Receiver.Number.Equals(actualCall.Receiver.Number) 
                          && expectedCall.CallState.Equals(actualCall.CallState)
                          && expectedCall.Duration.Equals(actualCall.Duration));
        }
    }
}