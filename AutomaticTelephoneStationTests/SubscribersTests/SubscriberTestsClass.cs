using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutomaticTelephoneStationTests.SubscribersTests
{
    [TestClass]
    public class SubscriberTestsClass
    {
        [TestMethod]
        public void TestCreatingSubscriberClass()
        {
            var stationObject = new Station(CountryCode.Belarus);

            var phoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567");
            var phoneObject = new Phone(phoneNumberObject);
            var portObject = new Port(phoneObject, stationObject);

            var subscriberObject = new Subscriber("Nikita", "Brosko", portObject);

            Assert.IsTrue(subscriberObject.FirstName.Equals("Nikita")
                          && subscriberObject.LastName.Equals("Brosko")
                          && subscriberObject.Phone.Equals(phoneObject)
                          && subscriberObject.Port.Equals(portObject));
        }
    }
}