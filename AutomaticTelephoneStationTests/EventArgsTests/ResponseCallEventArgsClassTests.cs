using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.EventArgs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.EventArgsTests
{
    [TestClass]
    public class ResponseCallEventArgsClassTests
    {
        [TestMethod]
        public void ResponseCallEventArgsClassCreatingWithValidParameters()
        {
            var callState = CallState.InProgress;

            var responseCallEventArgsObject = new ResponseCallEventArgs(callState);

            Assert.AreEqual(callState, responseCallEventArgsObject.CallState);
        }
    }
}