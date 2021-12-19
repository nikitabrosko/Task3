using System;

namespace AutomaticTelephoneStation.PhoneNumbers.BelarusPhoneNumbers
{
    public class TaskOperatorPhoneNumber : IOperatorPhoneNumber
    {
        public string Number { get; }

        public string CountryCode => "+375";

        public string OperatorCode => "77";

        
        public TaskOperatorPhoneNumber(string number)
        {
            if (number.Length != 7)
            {
                throw new ArgumentException("number length isn't 7!");
            }

            Number = string.Concat(CountryCode, OperatorCode, number);
        }
    }
}