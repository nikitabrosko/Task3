using System.Collections.Generic;
using System.Linq;
using AutomaticTelephoneStation.BillingSystem.CallReports;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.BillingSystemTests.CallReportsTests.FilterTests
{
    [TestClass]
    public class FilerTestsClass
    {
        [TestMethod]
        public void TestFilterByDurationMethod()
        {
            var phoneObject = GetPhone();

            var expectedCalls = new List<ICallReport>
            {
                phoneObject.CallReports.Calls.ToList()[0],
                phoneObject.CallReports.Calls.ToList()[4]
            };
            var actualCalls = phoneObject.CallReports.FilterByDuration(10).ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestFilterByWorthMethod()
        {
            var phoneObject = GetPhone();

            var expectedCalls = new List<ICallReport>
            {
                phoneObject.CallReports.Calls.ToList()[0]
            };
            var actualCalls = phoneObject.CallReports.FilterByWorth(50).ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestFilterByPhoneNumberMethod()
        {
            var phoneObject = GetPhone();

            var expectedCalls = new List<ICallReport>
            {
                phoneObject.CallReports.Calls.ToList()[0],
                phoneObject.CallReports.Calls.ToList()[3]
            };
            var actualCalls = phoneObject.CallReports
                .FilterByPhoneNumber(phoneObject.CallReports.Calls.First().PhoneNumber)
                .ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        public IPhone GetPhone()
        {
            var phoneNumberObjectFirst =
                new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "1234567");
            var phoneNumberObjectSecond =
                new BelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "7654321");
            var phoneNumberObjectThird =
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new MediumTariffPlan(), "8765432");
            var phoneNumberObjectFourth =
                new BelarusPhoneNumber(BelarusOperatorCode.Mts, new HighTariffPlan(), "9876543");

            var phoneObject = new Phone(phoneNumberObjectFirst);

            phoneObject.CallReports.AddCall(new CallerCallReport(
                phoneObject.PhoneNumber,
                phoneNumberObjectSecond,
                10));

            phoneObject.CallReports.AddCall(new CallerCallReport(
                phoneObject.PhoneNumber,
                phoneNumberObjectThird,
                15));

            phoneObject.CallReports.AddCall(new CallerCallReport(
                phoneObject.PhoneNumber,
                phoneNumberObjectFourth,
                60));

            phoneObject.CallReports.AddCall(new ReceiverCallReport(phoneNumberObjectSecond, 20));
            phoneObject.CallReports.AddCall(new ReceiverCallReport(phoneNumberObjectThird, 10));
            phoneObject.CallReports.AddCall(new ReceiverCallReport(phoneNumberObjectFourth, 90));

            return phoneObject;
        }
    }
}