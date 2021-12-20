using System;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AutomaticTelephoneStationTests.PhoneNumbersTests
{
    [TestClass]
    public class PhoneNumbersTestsClass
    {
        [TestMethod]
        public void TestCreatingBelarusPhoneNumber()
        {
            var number = "1234567";

            var belarusPhoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, number);
            var expectedNumber = "+375331234567";
            var actualNumber = belarusPhoneNumberObject.Number;

            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [TestMethod]
        public void TestCreatingRussiaPhoneNumber()
        {
            var number = "1234567";

            var belarusPhoneNumberObject = new RussiaPhoneNumber(RussiaOperatorCode.BeeLine, number);
            var expectedNumber = "+71291234567";
            var actualNumber = belarusPhoneNumberObject.Number;

            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [TestMethod]
        public void TestCreatingUsaPhoneNumber()
        {
            var number = "1234567";

            var belarusPhoneNumberObject = new UsaPhoneNumber(UsaOperatorCode.Sprint, number);
            var expectedNumber = "+11611234567";
            var actualNumber = belarusPhoneNumberObject.Number;

            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [TestMethod]
        public void TestCreatingPhoneNumberWithValidParameters()
        {
            var number = "1234567";

            var phoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, number);
            var expectedPhoneNumber = string.Concat("+", (int)CountryCode.Belarus, (int)BelarusOperatorCode.Mts, number);
            var actualPhoneNumber = phoneNumberObject.Number;

            Assert.AreEqual(expectedPhoneNumber, actualPhoneNumber);
        }

        [TestMethod]
        [DataRow("12345678")]
        [DataRow("123456")]
        [DataRow("123")]
        public void TestCreatingPhoneNumberWithInvalidParametersNumberLengthIsIncorrect(string number)
        {
            Assert.ThrowsException<ArgumentException>(() => new BelarusPhoneNumber(BelarusOperatorCode.Mts, number));
        }

        [TestMethod]
        public void TestPhoneNumberContainsLetters()
        {
            var number = "123a567";

            Assert.ThrowsException<ArgumentException>(() => new BelarusPhoneNumber(BelarusOperatorCode.Mts, number));
        }
    }
}