using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using AutomaticTelephoneStation.Subscribers;
using AutomaticTelephoneStation.TariffPlans;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests
{
    [TestClass]
    public class DebugTestsClass
    {
        [TestMethod]
        public void DebugTest()
        {
            var subscriber = new Subscriber
            (
                "Nikita", 
                "Brosko", 
                new Port(), 
                new Phone
                (
                    new MediumTariffPlan(), 
                    new PhoneNumber("6694581")
                )
            );

            var station = new Station();

            subscriber.Port.StartCall += station.OnPhoneStartingCall;

            subscriber.Phone.ConnectToPort();
            subscriber.Phone.Call(new PhoneNumber("1234567"));
        }
    }
}
