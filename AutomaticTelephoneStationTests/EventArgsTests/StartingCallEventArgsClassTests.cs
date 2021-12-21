using System;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.EventArgsTests
{
    [TestClass]
    public class StartingCallEventArgsClassTests
    {
        [TestMethod]
        public void StartingCallEventArgsClassCreatingWithValidParameters()
        {
            var startingCallEventArgsObject = new StartingCallEventArgs(
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321"));

            var expectedSourcePhoneNumber = new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567");
            var expectedTargetPhoneNumber = new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321");
            var actualSourcePhoneNumber = startingCallEventArgsObject.SourcePhoneNumber;
            var actualTargetPhoneNumber = startingCallEventArgsObject.TargetPhoneNumber;

            Assert.IsTrue(expectedSourcePhoneNumber.Number.Equals(actualSourcePhoneNumber.Number)
                          && expectedTargetPhoneNumber.Number.Equals(actualTargetPhoneNumber.Number));
        }

        [TestMethod]
        public void StartingCallEventArgsClassCreatingWithInvalidParametersSourcePhoneNumberIsNull()
        {
            IPhoneNumber phoneNumberObject = null;

            Assert.ThrowsException<ArgumentNullException>(() => new StartingCallEventArgs(phoneNumberObject,
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567")));
        }

        [TestMethod]
        public void StartingCallEventArgsClassCreatingWithInvalidParametersTargetPhoneNumberIsNull()
        {
            IPhoneNumber phoneNumberObject = null;

            Assert.ThrowsException<ArgumentNullException>(() => new StartingCallEventArgs(
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"),
                phoneNumberObject));
        }
    }
}