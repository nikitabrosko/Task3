using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.PhoneTests
{
    [TestClass]
    public class PhoneTestsClass
    {
        [TestMethod]
        public void TestCreatingPhoneClassWithValidParameters()
        {
            var phoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567");
            var phoneObject = new Phone(phoneNumberObject);

            Assert.IsTrue(phoneObject.ConnectionState is ConnectionState.Connected
                          && phoneObject.PhoneNumber.Equals(phoneNumberObject)
                          && phoneObject.PhoneCallState.Equals(PhoneCallState.Silence));
        }

        [TestMethod]
        public void TestConnectToPortMethod()
        {
            var stationObject = new Station(CountryCode.Belarus);
            var phoneObject = new Phone(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"));
            var portObject = new Port(phoneObject, stationObject);

            portObject.Phone.ConnectToPort();

            Assert.IsTrue(portObject.Phone.ConnectionState is ConnectionState.Connected);
        }

        [TestMethod]
        public void TestDisconnectFromPortMethod()
        {
            var phoneObject = new Phone(new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567"));

            phoneObject.ConnectToPort();
            phoneObject.DisconnectFromPort();

            Assert.IsTrue(phoneObject.ConnectionState is ConnectionState.Disconnected);
        }

        [TestMethod]
        public void TestCallMethod()
        {
            var stationObject = new Station(CountryCode.Belarus);

            var sourcePortObject = GetSourcePortObject(stationObject);
            var targetPortObject = GetTargetPortObject(stationObject);

            sourcePortObject.Phone.Call(targetPortObject.Phone.PhoneNumber);

            Assert.IsTrue(targetPortObject.Phone.PhoneCallState is PhoneCallState.StartCalling);
        }

        [TestMethod]
        public void TestRejectCallMethod()
        {
            var stationObject = new Station(CountryCode.Belarus);

            var sourcePortObject = GetSourcePortObject(stationObject);
            var targetPortObject = GetTargetPortObject(stationObject);

            sourcePortObject.Phone.Call(targetPortObject.Phone.PhoneNumber);
            targetPortObject.Phone.RejectCall();

            Assert.IsTrue(targetPortObject.Phone.PhoneCallState is PhoneCallState.Silence 
                          && sourcePortObject.Phone.PhoneCallState is PhoneCallState.Silence
                          && sourcePortObject.State is PortState.Free
                          && targetPortObject.State is PortState.Free);
        }

        [TestMethod]
        public void TestRejectCallAfterAcceptMethod()
        {
            var stationObject = new Station(CountryCode.Belarus);

            var sourcePortObject = GetSourcePortObject(stationObject);
            var targetPortObject = GetTargetPortObject(stationObject);

            sourcePortObject.Phone.Call(targetPortObject.Phone.PhoneNumber);
            targetPortObject.Phone.AcceptCall();
            targetPortObject.Phone.RejectCall();

            Assert.IsTrue(targetPortObject.Phone.PhoneCallState is PhoneCallState.Silence
                          && sourcePortObject.Phone.PhoneCallState is PhoneCallState.Silence
                          && sourcePortObject.State is PortState.Free
                          && targetPortObject.State is PortState.Free);
        }

        [TestMethod]
        public void TestAcceptCallMethod()
        {
            var stationObject = new Station(CountryCode.Belarus);

            var sourcePortObject = GetSourcePortObject(stationObject);
            var targetPortObject = GetTargetPortObject(stationObject);

            sourcePortObject.Phone.Call(targetPortObject.Phone.PhoneNumber);
            targetPortObject.Phone.AcceptCall();

            Assert.IsTrue(targetPortObject.Phone.PhoneCallState is PhoneCallState.InProgress
                          && sourcePortObject.Phone.PhoneCallState is PhoneCallState.InProgress
                          && sourcePortObject.State is PortState.Busy
                          && targetPortObject.State is PortState.Busy);
        }

        public static IPort GetSourcePortObject(IStation stationObject)
        {
            var sourcePhoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "1234567");
            var sourcePhoneObject = new Phone(sourcePhoneNumberObject);
            var sourcePortObject = new Port(sourcePhoneObject, stationObject);
            sourcePortObject.Phone.ConnectToPort();

            return sourcePortObject;
        }

        public static IPort GetTargetPortObject(IStation stationObject)
        {
            var targetPhoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, new LowTariffPlan(), "7654321");
            var targetPhoneObject = new Phone(targetPhoneNumberObject);
            var targetPortObject = new Port(targetPhoneObject, stationObject);
            targetPortObject.Phone.ConnectToPort();

            return targetPortObject;
        }
    }
}
