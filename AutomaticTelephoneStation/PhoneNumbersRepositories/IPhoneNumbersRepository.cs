using System.Collections.Generic;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.PhoneNumbersRepositories
{
    public interface IPhoneNumbersRepository
    {
        IEnumerable<IPhoneNumber> PhoneNumbers { get; }
        void AddNumber(IPhoneNumber phoneNumber);
        bool CheckForExist(IPhoneNumber phoneNumber);
    }
}
