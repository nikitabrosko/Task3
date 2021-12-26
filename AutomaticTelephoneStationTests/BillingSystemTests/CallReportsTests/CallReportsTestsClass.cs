using System;
using System.Linq;
using AutomaticTelephoneStation.BillingSystem.CallReports;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.BillingSystemTests.CallReportsTests
{
    [TestClass]
    public class CallReportsTestsClass
    {
        [TestMethod]
        public void TestCreatingCallerCallReportClassWithValidParameters()
        {
            var callerPhoneNumberObject =
                new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "1234567");
            var receiverPhoneNumberObject =
                new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "7654321");
            var callDuration = 10;
            var callDate = DateTime.Now;
            var expectedFee = 50M;

            var callerCallReportObject =
                new CallerCallReport(callerPhoneNumberObject, receiverPhoneNumberObject, callDate, callDuration);

            Assert.IsTrue(receiverPhoneNumberObject.Equals(callerCallReportObject.PhoneNumber)
                          && callDuration.Equals(callerCallReportObject.CallDuration)
                          && callDate.Equals(callerCallReportObject.CallDate)
                          && expectedFee.Equals(callerCallReportObject.Fee));
        }

        [TestMethod]
        public void TestCreatingCallerCallReportClassWithInvalidParametersCallerPhoneNumberIsNull()
        {
            IPhoneNumber callerPhoneNumberObject = null;
            var receiverPhoneNumberObject =
                new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "7654321");
            var callDuration = 10;

            Assert.ThrowsException<ArgumentNullException>(() =>
                new CallerCallReport(callerPhoneNumberObject, receiverPhoneNumberObject, DateTime.Now, callDuration));
        }

        [TestMethod]
        public void TestCreatingCallerCallReportClassWithInvalidParametersReceiverPhoneNumberIsNull()
        {
            var callerPhoneNumberObject =
                new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "1234567");
            IPhoneNumber receiverPhoneNumberObject = null;
            var callDuration = 10;

            Assert.ThrowsException<ArgumentNullException>(() =>
                new CallerCallReport(callerPhoneNumberObject, receiverPhoneNumberObject, DateTime.Now, callDuration));
        }

        [TestMethod]
        public void TestCreatingReceiverCallReportClassWithValidParameters()
        {
            var callerPhoneNumberObject =
                new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "1234567");
            var callDate = DateTime.Now;
            var callDuration = 10;

            var receiverCallReportObject =
                new ReceiverCallReport(callerPhoneNumberObject, callDate, callDuration);

            Assert.IsTrue(callerPhoneNumberObject.Equals(receiverCallReportObject.PhoneNumber)
                          && callDate.Equals(receiverCallReportObject.CallDate)
                          && callDuration.Equals(receiverCallReportObject.CallDuration));
        }

        [TestMethod]
        public void TestCreatingReceiverCallReportClassWithInvalidParametersCallerPhoneNumberIsNull()
        {
            IPhoneNumber callerPhoneNumberObject = null;
            var callDuration = 10;

            Assert.ThrowsException<ArgumentNullException>(() =>
                new ReceiverCallReport(callerPhoneNumberObject, DateTime.Now, callDuration));
        }

        [TestMethod]
        public void TestCreatingCallReportsRepositoryClassWithValidParameters()
        {
            var callReportRepositoryObject = new CallReportRepository();

            var callerPhoneNumberObject =
                new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "1234567");
            var callDate = DateTime.Now;
            var callDuration = 10;

            var receiverCallReportObject =
                new ReceiverCallReport(callerPhoneNumberObject, callDate, callDuration);

            callReportRepositoryObject.AddCall(receiverCallReportObject);

            Assert.AreEqual(receiverCallReportObject, callReportRepositoryObject.Calls.First());
        }

        [TestMethod]
        public void TestCreatingCallReportsRepositoryClassWithInvalidParametersCallReportIsNull()
        {
            var callReportRepositoryObject = new CallReportRepository();

            ICallReport callObject = null;

            Assert.ThrowsException<ArgumentNullException>(() => callReportRepositoryObject.AddCall(callObject));
        }
    }
}