using System;
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
            phoneObject.CallReports.SortByDuration(SortingParameter.Ascending);
            var actualCalls = phoneObject.CallReports.Calls.ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestSortByDurationMethodSortedParameterIsDescending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OrderByDescending(cr => cr.CallDuration)
                .ToList();
            phoneObject.CallReports.SortByDuration(SortingParameter.Descending);
            var actualCalls = phoneObject.CallReports.Calls.ToList();

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
            phoneObject.CallReports.SortByWorth(SortingParameter.Ascending);
            var actualCalls = phoneObject.CallReports.Calls.ToList();

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
            phoneObject.CallReports.SortByWorth(SortingParameter.Descending);
            var actualCalls = phoneObject.CallReports.Calls.ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestSortByPhoneNumberMethodSortedParameterIsAscending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OrderBy(cr => cr.PhoneNumber.Number)
                .ToList();
            phoneObject.CallReports.SortByPhoneNumber(SortingParameter.Ascending);
            var actualCalls = phoneObject.CallReports.Calls.ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestSortByPhoneNumberMethodSortedParameterIsDescending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OrderByDescending(cr => cr.PhoneNumber.Number)
                .ToList();
            phoneObject.CallReports.SortByPhoneNumber(SortingParameter.Descending);
            var actualCalls = phoneObject.CallReports.Calls.ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestSortByCallDateMethodSortedParameterIsAscending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OrderBy(cr => cr.CallDate)
                .ToList();
            phoneObject.CallReports.SortByCallDate(SortingParameter.Ascending);
            var actualCalls = phoneObject.CallReports.Calls.ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        [TestMethod]
        public void TestSortByCallDateMethodSortedParameterIsDescending()
        {
            var phoneObject = GetPhone();

            var expectedCalls = phoneObject.CallReports.Calls
                .OrderByDescending(cr => cr.CallDate)
                .ToList();
            phoneObject.CallReports.SortByCallDate(SortingParameter.Descending);
            var actualCalls = phoneObject.CallReports.Calls.ToList();

            Assert.IsTrue(expectedCalls.SequenceEqual(actualCalls));
        }

        public static IPhone GetPhone()
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
                DateTime.Now,
                10));

            phoneObject.CallReports.AddCall(new CallerCallReport(
                phoneObject.PhoneNumber,
                phoneNumberObjectThird,
                DateTime.Now,
                15));

            phoneObject.CallReports.AddCall(new CallerCallReport(
                phoneObject.PhoneNumber,
                phoneNumberObjectFourth,
                DateTime.Now,
                60));

            phoneObject.CallReports.AddCall(new ReceiverCallReport(phoneNumberObjectSecond, DateTime.Now, 20));
            phoneObject.CallReports.AddCall(new ReceiverCallReport(phoneNumberObjectThird, DateTime.Now, 10));
            phoneObject.CallReports.AddCall(new ReceiverCallReport(phoneNumberObjectFourth, DateTime.Now, 90));

            return phoneObject;
        }
    }
}