using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.PhoneNumbersRepositories
{
    public class PhoneNumbersRepository : IPhoneNumbersRepository
    {
        private readonly IList<IPhoneNumber> _phoneNumbers = new List<IPhoneNumber>();

        public IEnumerable<IPhoneNumber> PhoneNumbers => new ReadOnlyCollection<IPhoneNumber>(_phoneNumbers);

        public void AddNumber(IPhoneNumber phoneNumber)
        {
            if (CheckForExist(phoneNumber))
            {
                throw new ArgumentException($"Number {phoneNumber} is already exists");
            }

            _phoneNumbers.Add(phoneNumber);
        }

        public bool CheckForExist(IPhoneNumber phoneNumber)
        {
            return _phoneNumbers.Any(number => number.Number.Equals(phoneNumber.Number));
        }
    }
}