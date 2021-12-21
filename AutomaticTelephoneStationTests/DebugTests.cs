using System.Threading;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests
{
    [TestClass]
    public class DebugTests
    {
        [TestMethod]
        public void OnPhoneStartingCallMethodTestsPhoneNumberIsAnotherCountry()
        {
            var stationObject = new Station(CountryCode.Belarus);
            var callingPhone = new Phone(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"));
            var targetPhone = new Phone(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321"));
            var callingPortObject = new Port(callingPhone, stationObject);
            callingPortObject.Phone.ConnectToPort();
            var targetPortObject = new Port(targetPhone, stationObject);
            targetPortObject.Phone.ConnectToPort();

            callingPortObject.Phone.Call(targetPortObject.Phone.PhoneNumber);
            targetPortObject.Phone.AcceptCall();
            Thread.Sleep(1000);
            targetPortObject.Phone.RejectCall();
        }
    }
}