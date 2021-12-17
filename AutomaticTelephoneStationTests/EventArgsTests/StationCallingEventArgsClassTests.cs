using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AutomaticTelephoneStationTests.EventArgsTests
{
    [TestClass]
    public class StationCallingEventArgsClassTests
    {
        [TestMethod]
        public void StationCallingEventArgsClassCreatingWithValidParameters()
        {
            var stationCallingEventArgsObject = new StationCallingEventArgs(
                new Call(new PhoneNumber("1234567"), new PhoneNumber("7654321")));

            var expectedCall = new Call(new PhoneNumber("1234567"), new PhoneNumber("7654321"));
            var actualCall = stationCallingEventArgsObject.Call;

            Assert.IsTrue(expectedCall.Caller.Number.Equals(actualCall.Caller.Number) 
                          && expectedCall.Receiver.Number.Equals(actualCall.Receiver.Number) 
                          && expectedCall.CallState.Equals(actualCall.CallState)
                          && expectedCall.Duration.Equals(actualCall.Duration));
        }
    }
}