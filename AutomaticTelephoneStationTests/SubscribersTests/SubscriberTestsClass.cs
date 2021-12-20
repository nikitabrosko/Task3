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
            var mock = new Mock<IStation>();

            var phoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567");
            var tariffPlanObject = new LowTariffPlan();
            var phoneObject = new Phone(tariffPlanObject, phoneNumberObject);
            var portObject = new Port(phoneObject, mock.Object);

            var subscriberObject = new Subscriber("Nikita", "Brosko", portObject);

            Assert.IsTrue(subscriberObject.FirstName.Equals("Nikita")
                          && subscriberObject.LastName.Equals("Brosko")
                          && subscriberObject.Phone.Equals(phoneObject)
                          && subscriberObject.Port.Equals(portObject));
        }
    }
}