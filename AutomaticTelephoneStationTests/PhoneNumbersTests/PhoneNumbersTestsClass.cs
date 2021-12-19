using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.BelarusPhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.PhoneNumbersTests
{
    [TestClass]
    public class PhoneNumbersTestsClass
    {
        [TestMethod]
        public void TestCreatingPhoneNumberWithValidParameters()
        {
            var number = "1234567";

            var phoneNumberObject = new TaskOperatorPhoneNumber(number);
            var expectedPhoneNumber = string.Concat("+375", "77", number);
            var actualPhoneNumber = phoneNumberObject.Number;

            Assert.AreEqual(expectedPhoneNumber, actualPhoneNumber);
        }

        [TestMethod]
        [DataRow("12345678")]
        [DataRow("123456")]
        [DataRow("123")]
        public void TestCreatingPhoneNumberWithInvalidParametersNumberLengthIsIncorrect(string number)
        {
            Assert.ThrowsException<ArgumentException>(() => new TaskOperatorPhoneNumber(number));
        }
    }
}