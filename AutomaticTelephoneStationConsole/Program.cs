using System;
using AutomaticTelephoneStation;
using AutomaticTelephoneStation.PhoneNumbers;
using AutomaticTelephoneStation.PhoneNumbersRepositories;
using AutomaticTelephoneStation.Phones;
using AutomaticTelephoneStation.Ports;
using AutomaticTelephoneStation.Stations;
using AutomaticTelephoneStation.Subscribers;
using System.Linq;
using System.Threading;
using AutomaticTelephoneStation.BillingSystem.CallReports;
using AutomaticTelephoneStation.BillingSystem.TariffPlans;
using AutomaticTelephoneStation.PhoneNumbers.OperatorCodes;
using AutomaticTelephoneStation.PhoneNumbers.PhoneNumbers;

namespace AutomaticTelephoneStationConsole
{
    public class Program
    {
        private static void Main()
        {
            var phoneNumbersRepository = new PhoneNumbersRepository();
            var belarusStation = CreateStation(CountryCode.Belarus);

            var firstSubscriber = CreateSubscriber(
                new SubscriberArgs("Nikita", "Brosko",
                    CreateBelarusPhoneNumber(BelarusOperatorCode.Mts, new MediumTariffPlan(), "6694581", phoneNumbersRepository),
                phoneNumbersRepository, belarusStation));

            firstSubscriber.Phone.ConnectToPort();

            var secondSubscriber = CreateSubscriber(
                new SubscriberArgs("Ivan", "Sidorov",
                    CreateBelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "1234567", phoneNumbersRepository),
                    phoneNumbersRepository, belarusStation));

            secondSubscriber.Phone.ConnectToPort();

            var thirdSubscriber = CreateSubscriber(
                new SubscriberArgs("Sergey", "Grigorovich",
                    CreateBelarusPhoneNumber(BelarusOperatorCode.Life, new LowTariffPlan(), "7654321", phoneNumbersRepository),
                    phoneNumbersRepository, belarusStation));

            thirdSubscriber.Phone.ConnectToPort();

            firstSubscriber.Phone.Call(secondSubscriber.Phone.PhoneNumber);
            secondSubscriber.Phone.AcceptCall();
            //The thread falls asleep to make it look like a real call
            Thread.Sleep(5000);
            //Initiate new call to busy phone
            thirdSubscriber.Phone.Call(firstSubscriber.Phone.PhoneNumber);
            firstSubscriber.Phone.RejectCall();

            PrintSubscriberCallReports(firstSubscriber);
            PrintSubscriberCallReports(secondSubscriber);
        }

        private static void PrintSubscriberCallReports(ISubscriber subscriber)
        {
            foreach (var callReport in subscriber.Phone.CallReports.Calls)
            {
                if (callReport is CallerCallReport call)
                {
                    Console.WriteLine(string.Concat(Environment.NewLine, "Incoming call number: ", call.PhoneNumber.Number, 
                        Environment.NewLine, "call duration: ", call.CallDuration.ToString(), 
                        Environment.NewLine, "call fee: ", call.Fee));
                }
                else
                {
                    Console.WriteLine(string.Concat(Environment.NewLine, "Incoming call number: ", callReport.PhoneNumber.Number, 
                        Environment.NewLine, "call duration: ", callReport.CallDuration.ToString()));
                }
            }
        }

        private static IPhoneNumber CreateUsaPhoneNumber(UsaOperatorCode operatorCode,
            ITariffPlan tariffPlan, string number, IPhoneNumbersRepository phoneNumbersRepository)
        {
            var phoneNumber = new UsaPhoneNumber(operatorCode, tariffPlan, number);

            phoneNumbersRepository.AddNumber(phoneNumber);

            return phoneNumber;
        }

        private static IPhoneNumber CreateRussiaPhoneNumber(RussiaOperatorCode operatorCode,
            ITariffPlan tariffPlan, string number, IPhoneNumbersRepository phoneNumbersRepository)
        {
            var phoneNumber = new RussiaPhoneNumber(operatorCode, tariffPlan, number);

            phoneNumbersRepository.AddNumber(phoneNumber);

            return phoneNumber;
        }

        private static IPhoneNumber CreateBelarusPhoneNumber(BelarusOperatorCode operatorCode, 
            ITariffPlan tariffPlan, string number, IPhoneNumbersRepository phoneNumbersRepository)
        {
            var phoneNumber = new BelarusPhoneNumber(operatorCode, tariffPlan, number);

            phoneNumbersRepository.AddNumber(phoneNumber);

            return phoneNumber;
        }

        private static IPhone CreatePhone(IPhoneNumber phoneNumber, IPhoneNumbersRepository phoneNumbersRepository)
        {
            return new Phone(phoneNumber);
        }

        private static IPort CreatePort(IPhone phone, IStation station)
        {
            return new Port(phone, station);
        }

        private static IStation CreateStation(CountryCode countryCode)
        {
            return new Station(countryCode);
        }

        private static ISubscriber CreateSubscriber(SubscriberArgs subscriberArgs)
        {
            return new Subscriber(subscriberArgs.FirstName, subscriberArgs.LastName,
                    CreatePort(
                        CreatePhone(
                            subscriberArgs.PhoneNumber,
                            subscriberArgs.PhoneNumbersRepository),
                        subscriberArgs.Station));
        }
    }
}