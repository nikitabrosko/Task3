using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbersRepositories;
using AutomaticTelephoneStation.Stations;

namespace AutomaticTelephoneStationConsole
{
    public class SubscriberArgs
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IPhoneNumber PhoneNumber { get; set; }

        public IPhoneNumbersRepository PhoneNumbersRepository { get; set; }

        public IStation Station { get; set; }

        public SubscriberArgs(string firstName, string lastName, IPhoneNumber phoneNumber,
            IPhoneNumbersRepository phoneNumbersRepository, IStation station)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            PhoneNumbersRepository = phoneNumbersRepository;
            Station = station;
        }
    }
}