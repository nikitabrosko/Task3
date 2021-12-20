using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using AutomaticTelephoneStation.TariffPlans;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutomaticTelephoneStationTests.PhoneTests
{
    [TestClass]
    public class PhoneTestsClass
    {
        [TestMethod]
        public void TestCreatingPhoneClassWithValidParameters()
        {
            var lowTariffPlanObject = new LowTariffPlan();
            var phoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567");
            var phoneObject = new Phone(lowTariffPlanObject, phoneNumberObject);

            Assert.IsTrue(phoneObject.ConnectionState is ConnectionState.Disconnected
                          && phoneObject.PhoneNumber.Equals(phoneNumberObject)
                          && phoneObject.TariffPlan.Equals(lowTariffPlanObject)
                          && phoneObject.PhoneCallState.Equals(PhoneCallState.Silence));
        }

        [TestMethod]
        public void TestConnectToPortMethod()
        {
            var mock = new Mock<IStation>();
            var phoneObject = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));
            var portObject = new Port(phoneObject, mock.Object);

            portObject.Phone.ConnectToPort();

            Assert.IsTrue(portObject.Phone.ConnectionState is ConnectionState.Connected);
        }

        [TestMethod]
        public void TestDisconnectFromPortMethod()
        {
            var phoneObject = new Phone(new LowTariffPlan(), new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567"));

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
            var mock = new Mock<IStation>();

            var sourcePhoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567");
            var sourceTariffPlanObject = new LowTariffPlan();
            var sourcePhoneObject = new Phone(sourceTariffPlanObject, sourcePhoneNumberObject);
            var sourcePortObject = new Port(sourcePhoneObject, mock.Object);
            sourcePortObject.Phone.ConnectToPort();
            sourcePortObject.OutgoingCall += stationObject.OnPhoneStartingCall;
            sourcePortObject.CallChangeState += stationObject.OnCallChangeState;
            stationObject.ResponseFromCall += sourcePortObject.OnResponseFromCall;
            stationObject.PortController.AddPort(sourcePortObject);

            return sourcePortObject;
        }

        public static IPort GetTargetPortObject(IStation stationObject)
        {
            var mock = new Mock<IStation>();

            var targetPhoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321");
            var targetTariffPlanObject = new LowTariffPlan();
            var targetPhoneObject = new Phone(targetTariffPlanObject, targetPhoneNumberObject);
            var targetPortObject = new Port(targetPhoneObject, mock.Object);
            targetPortObject.Phone.ConnectToPort();
            targetPortObject.OutgoingCall += stationObject.OnPhoneStartingCall;
            targetPortObject.CallChangeState += stationObject.OnCallChangeState;
            stationObject.ResponseFromCall += targetPortObject.OnResponseFromCall;
            stationObject.PortController.AddPort(targetPortObject);

            return targetPortObject;
        }
    }
}
