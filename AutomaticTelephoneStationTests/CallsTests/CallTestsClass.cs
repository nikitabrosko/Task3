using System.Threading;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.CallsTests
{
    [TestClass]
    public class CallTestsClass
    {
        [TestMethod]
        public void TestCallClassCreatingWithValidParameters()
        {
            var expectedCallerPhoneNumber = new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567");
            var expectedReceiverPhoneNumber = new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321");
            var expectedCallState = CallState.IsWaiting;
            var expectedDuration = 0;

            var actualCallObject = new Call(
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"), 
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321"));

            Assert.IsTrue(expectedCallerPhoneNumber.Number.Equals(actualCallObject.Caller.Number)
                          && expectedReceiverPhoneNumber.Number.Equals(actualCallObject.Receiver.Number)
                          && expectedCallState.Equals(actualCallObject.CallState)
                          && expectedDuration.Equals(actualCallObject.Duration));
        }

        [TestMethod]
        public void TestStopwatchMethods()
        {
            var callObject = new Call(
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321"));

            var expectedDuration = 1;

            callObject.StartStopwatch();
            Thread.Sleep(1000);
            callObject.StopStopwatch();

            var actualDuration = callObject.Duration;

            Assert.AreEqual(expectedDuration, actualDuration);
        }
    }
}
