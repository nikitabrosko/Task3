using System;
using System.Linq;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using AutomaticTelephoneStation.TariffPlans;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutomaticTelephoneStationTests.PortControllersTests
{
    [TestClass]
    public class PortControllerTestsClass
    {
        [TestMethod]
        public void PortControllerClassCreatingWithValidParameters()
        {
            var mock = new Mock<IStation>();

            var phoneObject = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));
            var portControllerObject = new PortController();
            portControllerObject.AddPort(new Port(phoneObject, mock.Object));

            var expectedPort = new Port(phoneObject, mock.Object);
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