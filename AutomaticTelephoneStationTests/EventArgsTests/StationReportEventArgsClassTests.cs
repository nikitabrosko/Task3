using System;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.EventArgsTests
{
    [TestClass]
    public class StationReportEventArgsClassTests
    {
        [TestMethod]
        public void StationReportEventArgsClassCreatingWithValidParameters()
        {
            var stationCallingEventArgsObject = new StationReportEventArgs(new Call(
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321")));

            var expectedCall = new Call(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321"));
            var actualCall = stationCallingEventArgsObject.CallReport;

            Assert.IsTrue(expectedCall.Caller.Number.Equals(actualCall.Caller.Number)
                          && expectedCall.Receiver.Number.Equals(actualCall.Receiver.Number)
                          && expectedCall.CallState.Equals(actualCall.CallState)
                          && expectedCall.Duration.Equals(actualCall.Duration));
        }

        [TestMethod]
        public void StationReportEventArgsClassCreatingWithInvalidParameters()
        {
            ICall callObject = null;

            Assert.ThrowsException<ArgumentNullException>(() => new StationReportEventArgs(callObject));
        }
    }
}