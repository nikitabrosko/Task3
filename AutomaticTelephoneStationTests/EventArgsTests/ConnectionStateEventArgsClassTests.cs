using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.Phones;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.EventArgsTests
{
    [TestClass]
    public class ConnectionStateEventArgsClassTests
    {
        [TestMethod]
        public void ConnectionStateEventArgsClassCreatingWithValidParameters()
        {
            var connectionStateEventArgsObject = new ConnectionStateEventArgs(ConnectionState.Connected);

            var expectedConnectionState = ConnectionState.Connected;
            var actualConnectionState = connectionStateEventArgsObject.ConnectionState;

            Assert.AreEqual(expectedConnectionState, actualConnectionState);
        }
    }
}