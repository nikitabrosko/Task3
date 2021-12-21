using System;
using AutomaticTelephoneStation.BillingSystem.CallReports;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.EventArgs;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.EventArgsTests
{
    [TestClass]
    public class PortReportEventArgsClassTests
    {
        [TestMethod]
        public void PortReportEventArgsClassCreatingWithValidParameters()
        {
            var callReportObject = new CallerCallReport(
                new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "7654321"),
                10);

            var portReportEventArgs = new PortReportEventArgs(callReportObject);

            Assert.AreEqual(callReportObject, portReportEventArgs.CallReport);
        }

        [TestMethod]
        public void PortReportEventArgsClassCreatingWithInvalidParametersCallReportIsNull()
        {
            ICallReport callReportObject = null;

            Assert.ThrowsException<ArgumentNullException>(() => new PortReportEventArgs(callReportObject));
        }
    }
}