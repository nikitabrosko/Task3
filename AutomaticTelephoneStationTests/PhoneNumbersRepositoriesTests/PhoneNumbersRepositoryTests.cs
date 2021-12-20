using System;
using System.Linq;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbersRepositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomaticTelephoneStationTests.PhoneNumbersRepositoriesTests
{
    [TestClass]
    public class PhoneNumbersRepositoryTests
    {
        [TestMethod]
        public void TestAddMethod()
        {
            var phoneNumbersRepository = new PhoneNumbersRepository();
            var phoneNumberObjectFirst = new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567");

            phoneNumbersRepository.AddNumber(phoneNumberObjectFirst);

            Assert.IsTrue(phoneNumbersRepository.PhoneNumbers.Contains(phoneNumberObjectFirst));
        }

        [TestMethod]
        public void TestAddMethodWithExistingNumber()
        {
            var phoneNumbersRepository = new PhoneNumbersRepository();
            var phoneNumberObject = new BelarusPhoneNumber(BelarusOperatorCode.Mts, "1234567");

            phoneNumbersRepository.AddNumber(phoneNumberObject);

            Assert.ThrowsException<ArgumentException>(() => phoneNumbersRepository.AddNumber(phoneNumberObject));
        }
    }
}