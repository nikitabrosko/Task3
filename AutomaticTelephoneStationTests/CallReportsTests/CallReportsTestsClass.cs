using System;
using System.Linq;
using AutomaticTelephoneStation.CallReports;
using AutomaticTelephoneStation.Calls;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.CallReportsTests
{
    [TestClass]
    public class CallReportsTestsClass
    {
        [TestMethod]
        public void TestCreatingCallReportsClassWithValidParameters()
        {
            var callReportObject = new CallReport();

            var callObject = new Call(
                new BelarusPhoneNumber(BelarusOperatorCode.Life, "1234567"),
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, "7654321"));

            callReportObject.AddCall(callObject);

            Assert.AreEqual(callObject, callReportObject.Calls.First());
        }

        [TestMethod]
        public void TestCreatingCallReportsClassWithInvalidParametersCallIsNull()
        {
            var callReportObject = new CallReport();

            ICall callObject = null;

            Assert.ThrowsException<ArgumentNullException>(() => callReportObject.AddCall(callObject));
        }
    }
}