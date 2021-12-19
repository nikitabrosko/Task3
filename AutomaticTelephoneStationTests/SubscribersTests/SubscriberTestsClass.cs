using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.BelarusPhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.SubscribersTests
{
    [TestClass]
    public class SubscriberTestsClass
    {
        [TestMethod]
        public void TestCreatingSubscriberClass()
        {
            var phoneNumberObject = new TaskOperatorPhoneNumber("1234567");
            var tariffPlanObject = new LowTariffPlan();
            var phoneObject = new Phone(tariffPlanObject, phoneNumberObject);
            var portObject = new Port(phoneObject);

            var subscriberObject = new Subscriber("Nikita", "Brosko", portObject);

            Assert.IsTrue(subscriberObject.FirstName.Equals("Nikita")
                          && subscriberObject.LastName.Equals("Brosko")
                          && subscriberObject.Phone.Equals(phoneObject)
                          && subscriberObject.Port.Equals(portObject));
        }
    }
}