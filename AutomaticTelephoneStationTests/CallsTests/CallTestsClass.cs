using System.Threading;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.PhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.CallsTests
{
    [TestClass]
    public class CallTestsClass
    {
        [TestMethod]
        public void TestCallClassCreatingWithValidParameters()
        {
            var expectedCallerPhoneNumber = new PhoneNumber("1234567");
            var expectedReceiverPhoneNumber = new PhoneNumber("7654321");
            var expectedCallState = CallState.IsWaiting;
            var expectedDuration = 0;

            var actualCallObject = new Call(new PhoneNumber("1234567"), new PhoneNumber("7654321"));

            Assert.IsTrue(expectedCallerPhoneNumber.Number.Equals(actualCallObject.Caller.Number)
                          && expectedReceiverPhoneNumber.Number.Equals(actualCallObject.Receiver.Number)
                          && expectedCallState.Equals(actualCallObject.CallState)
                          && expectedDuration.Equals(actualCallObject.Duration));
        }

        [TestMethod]
        public void TestStopwatchMethods()
        {
            var callObject = new Call(new PhoneNumber("1234567"), new PhoneNumber("7654321"));

            var expectedDuration = 1;

            callObject.StartStopwatch();
            Thread.Sleep(1000);
            callObject.StopStopwatch();

            var actualDuration = callObject.Duration;

            Assert.AreEqual(expectedDuration, actualDuration);
        }
    }
}
