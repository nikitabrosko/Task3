using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests
{
    [TestClass]
    public class DebugTestsClass
    {
        [TestMethod]
        public void DebugTest()
        {
            var phone = new Phone(new MediumTariffPlan(), new PhoneNumber("6694581"));
            var subscriber = new Subscriber("Nikita", "Brosko", phone);
            var port = new Port();
            var station = new Station();

            phone.StartCall += port.OnPhoneStartingCall;
            port.StartCall += station.OnPhoneStartingCall;

            phone.Call(new PhoneNumber("1234567"));
        }
    }
}
