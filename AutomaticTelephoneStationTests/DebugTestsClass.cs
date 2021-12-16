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
            var station = new Station();

            var subscriberFirst = new Subscriber
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
            subscriberFirst.Port.OutgoingCall += station.OnPhoneStartingCall;
            subscriberFirst.Port.CallChangeState += station.OnCallChangeState;
            subscriberFirst.Phone.ConnectToPort();

            var subscriberSecond = new Subscriber
            (
                "Matvey",
                "Vanyukevich",
                new Port(),
                new Phone
                (
                    new LowTariffPlan(),
                    new PhoneNumber("3137656")
                )
            );
            subscriberSecond.Port.OutgoingCall += station.OnPhoneStartingCall;
            subscriberSecond.Port.CallChangeState += station.OnCallChangeState;
            subscriberSecond.Phone.ConnectToPort();

            station.PortController.AddPort(subscriberFirst.Port);
            station.PortController.AddPort(subscriberSecond.Port);

            subscriberFirst.Phone.Call(new PhoneNumber("3137656"));

            if (subscriberSecond.Phone.PhoneCallState is PhoneCallState.StartCalling)
            {
                subscriberSecond.Phone.AcceptCall();
            }

            if (subscriberSecond.Phone.PhoneCallState is PhoneCallState.InProgress)
            {
                subscriberSecond.Phone.RejectCall();
            }
        }
    }
}
