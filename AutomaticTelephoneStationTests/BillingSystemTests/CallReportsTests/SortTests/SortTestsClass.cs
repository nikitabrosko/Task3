using System.Linq;
using AutomaticTelephoneStation.BillingSystem.CallReports;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.Phones;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.BillingSystemTests.CallReportsTests.SortTests
{
    [TestClass]
    public class SortTestsClass
    {
        [TestMethod]
        public void TestSortByDurationMethodSortedParameterIsAscending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OrderBy(cr => cr.CallDuration)
                .ToList();
            var actualCalls = phoneObject.CallReports.SortByDuration(SortingParameter.Ascending).ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestSortByDurationMethodSortedParameterIsDescending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OrderByDescending(cr => cr.CallDuration)
                .ToList();
            var actualCalls = phoneObject.CallReports.SortByDuration(SortingParameter.Descending).ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestSortByWorthMethodSortedParameterIsAscending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OfType<CallerCallReport>()
                .OrderBy(cr => cr.Fee)
                .ToList();
            var actualCalls = phoneObject.CallReports.SortByWorth(SortingParameter.Ascending).ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestSortByWorthMethodSortedParameterIsDescending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OfType<CallerCallReport>()
                .OrderByDescending(cr => cr.Fee)
                .ToList();
            var actualCalls = phoneObject.CallReports.SortByWorth(SortingParameter.Descending).ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestSortByPhoneNumberMethodSortedParameterIsAscending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OrderBy(cr => cr.PhoneNumber.Number)
                .ToList();
            var actualCalls = phoneObject.CallReports.SortByPhoneNumber(SortingParameter.Ascending).ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestSortByPhoneNumberMethodSortedParameterIsDescending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OrderByDescending(cr => cr.PhoneNumber.Number)
                .ToList();
            var actualCalls = phoneObject.CallReports.SortByPhoneNumber(SortingParameter.Descending).ToList();

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