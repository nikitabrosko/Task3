using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.BelarusPhoneNumbers;
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
                new TaskOperatorPhoneNumber("1234567"), 
                new TaskOperatorPhoneNumber("7654321"));

            var expectedSourcePhoneNumber = new TaskOperatorPhoneNumber("1234567");
            var expectedTargetPhoneNumber = new TaskOperatorPhoneNumber("7654321");
            var actualSourcePhoneNumber = startingCallEventArgsObject.SourcePhoneNumber;
            var actualTargetPhoneNumber = startingCallEventArgsObject.TargetPhoneNumber;

            Assert.IsTrue(expectedSourcePhoneNumber.Number.Equals(actualSourcePhoneNumber.Number)
                          && expectedTargetPhoneNumber.Number.Equals(actualTargetPhoneNumber.Number));
        }
    }
}