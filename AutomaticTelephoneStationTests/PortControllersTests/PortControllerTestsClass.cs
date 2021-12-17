using System;
using System.Linq;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.PortControllersTests
{
    [TestClass]
    public class PortControllerTestsClass
    {
        [TestMethod]
        public void PortControllerClassCreatingWithValidParameters()
        {
            var portControllerObject = new PortController();
            portControllerObject.AddPort(new Port());

            var expectedPort = new Port();
            var actualPort = portControllerObject.Ports.First();

            Assert.IsTrue(expectedPort.State.Equals(actualPort.State) 
                          && expectedPort.ConnectionState.Equals(actualPort.ConnectionState));
        }

        [TestMethod]
        public void PortControllerClassCreatingWithInvalidParametersPortIsNull()
        {
            IPort port = null;
            var portControllerObject = new PortController();

            Assert.ThrowsException<ArgumentNullException>(() => portControllerObject.AddPort(port));
        }
    }
}