using System;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;

namespace AutomaticTelephoneStationTests.EventArgsTests
{
    [TestClass]
    public class StationCallingEventArgsClassTests
    {
        [TestMethod]
        public void StationCallingEventArgsClassCreatingWithValidParameters()
        {
            var stationCallingEventArgsObject = new StationCallingEventArgs(new Call(
                    new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"),
                    new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321")));

            var expectedCall = new Call(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321"));
            var actualCall = stationCallingEventArgsObject.Call;

            Assert.IsTrue(expectedCall.Caller.Number.Equals(actualCall.Caller.Number) 
                          && expectedCall.Receiver.Number.Equals(actualCall.Receiver.Number) 
                          && expectedCall.CallState.Equals(actualCall.CallState)
                          && expectedCall.Duration.Equals(actualCall.Duration));
        }

        [TestMethod]
        public void StationCallingEventArgsClassCreatingWithInvalidParameters()
        {
            ICall callObject = null;

            Assert.ThrowsException<ArgumentNullException>(() => new StationCallingEventArgs(callObject));
        }
    }
}