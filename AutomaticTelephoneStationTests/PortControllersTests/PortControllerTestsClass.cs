using System;
using System.Linq;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
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
            var stationObject = new Station(CountryCode.Belarus);
            var phoneObject = new Phone(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"));
            var portControllerObject = new PortController();

            var expectedPort = new Port(phoneObject, stationObject);
            portControllerObject.AddPort(expectedPort);
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