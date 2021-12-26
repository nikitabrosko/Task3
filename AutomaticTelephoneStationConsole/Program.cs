using System;
using System.Collections.Generic;
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

            var secondSubscriber = CreateSubscriber(
                new SubscriberArgs("Ivan", "Sidorov",
                    CreateBelarusPhoneNumber(BelarusOperatorCode.A1, new LowTariffPlan(), "1234567", phoneNumbersRepository),
                    phoneNumbersRepository, belarusStation));

            var thirdSubscriber = CreateSubscriber(
                new SubscriberArgs("Sergey", "Grigorovich",
                    CreateBelarusPhoneNumber(BelarusOperatorCode.Life, new LowTariffPlan(), "7654321", phoneNumbersRepository),
                    phoneNumbersRepository, belarusStation));

            AddFakeCalls(firstSubscriber, secondSubscriber, thirdSubscriber);

            firstSubscriber.Phone.Call(secondSubscriber.Phone.PhoneNumber);
            secondSubscriber.Phone.AcceptCall();
            //The thread falls asleep to make it look like a real call
            Thread.Sleep(5000);
            //Initiate new call to busy phone
            thirdSubscriber.Phone.Call(firstSubscriber.Phone.PhoneNumber);
            firstSubscriber.Phone.RejectCall();
            
            while (true)
            {
                Console.WriteLine("Hello! Would you like to do?" +
                                  "\n1 - Print subscriber call reports" +
                                  "\n2 - Filter and print subscriber call reports" +
                                  "\n3 - Sort and print subscriber call reports");

                var consoleKey = Console.ReadKey().Key;

                if (consoleKey.Equals(ConsoleKey.Escape))
                {
                    break;
                }

                switch (consoleKey)
                {
                    case ConsoleKey.D1:
                        PrintSubscriberCallReports(firstSubscriber);
                        PrintSubscriberCallReports(secondSubscriber);
                        PrintSubscriberCallReports(thirdSubscriber);
                        break;
                    case ConsoleKey.D2:
                        while (true)
                        {
                            Console.WriteLine("Choose a filtering option:" +
                                              "\n1 - By phone number" +
                                              "\n2 - By call date" +
                                              "\n3 - By call duration" +
                                              "\n4 - By call worth");

                            var consoleKeySecond = Console.ReadKey().Key;

                            if (consoleKeySecond.Equals(ConsoleKey.Escape))
                            {
                                break;
                            }

                            switch (consoleKeySecond)
                            {
                                case ConsoleKey.D1:
                                    PrintFilteringByPhoneNumberCallReports(firstSubscriber.Phone.CallReports,
                                        secondSubscriber.Phone.PhoneNumber);
                                    break;
                                case ConsoleKey.D2:
                                    PrintFilteringByCallDateCallReports(firstSubscriber.Phone.CallReports,
                                        firstSubscriber.Phone.CallReports.Calls.First().CallDate);
                                    break;
                                case ConsoleKey.D3:
                                    PrintFilteringByDurationCallReports(firstSubscriber.Phone.CallReports, 5);
                                    break;
                                case ConsoleKey.D4:
                                    PrintFilteringByWorthCallReports(firstSubscriber.Phone.CallReports, 50);
                                    break;
                                default:
                                    Console.WriteLine("Unexpected key! Try again!");
                                    continue;
                            }
                        }
                        break;
                    case ConsoleKey.D3:
                        while (true)
                        {
                            Console.WriteLine("Choose a sort option:" +
                                              "\n1 - By phone number" +
                                              "\n2 - By call date" +
                                              "\n3 - By call duration" +
                                              "\n4 - By call worth");

                            var consoleKeySecond = Console.ReadKey().Key;

                            if (consoleKeySecond.Equals(ConsoleKey.Escape))
                            {
                                break;
                            }

                            switch (consoleKeySecond)
                            {
                                case ConsoleKey.D1:
                                    PrintSortedCallReports(SortParameter.ByPhoneNumber, 
                                        SortingParameter.Descending,
                                        firstSubscriber.Phone.CallReports);
                                    break;
                                case ConsoleKey.D2:
                                    PrintSortedCallReports(SortParameter.ByCallDate,
                                        SortingParameter.Descending,
                                        firstSubscriber.Phone.CallReports);
                                    break;
                                case ConsoleKey.D3:
                                    PrintSortedCallReports(SortParameter.ByDuration,
                                        SortingParameter.Descending,
                                        firstSubscriber.Phone.CallReports);
                                    break;
                                case ConsoleKey.D4:
                                    PrintSortedCallReports(SortParameter.ByWorth,
                                        SortingParameter.Descending,
                                        firstSubscriber.Phone.CallReports);
                                    break;
                                default:
                                    Console.WriteLine("Unexpected key! Try again!");
                                    continue;
                            }
                        }
                        break;
                    default:
                        Console.WriteLine(string.Concat(Environment.NewLine, "Unexpected key! Try again!", Environment.NewLine));
                        continue;
                }
            }
        }

        private static void AddFakeCalls(ISubscriber mainSubscriber, ISubscriber secondSubscriber, ISubscriber thirdSubscriber)
        {
            mainSubscriber.Phone.CallReports.AddCall(new CallerCallReport(
                mainSubscriber.Phone.PhoneNumber,
                secondSubscriber.Phone.PhoneNumber,
                DateTime.Today,
                10));

            mainSubscriber.Phone.CallReports.AddCall(new CallerCallReport(
                mainSubscriber.Phone.PhoneNumber,
                secondSubscriber.Phone.PhoneNumber,
                DateTime.Today,
                15));

            mainSubscriber.Phone.CallReports.AddCall(new CallerCallReport(
                mainSubscriber.Phone.PhoneNumber,
                thirdSubscriber.Phone.PhoneNumber,
                DateTime.Now,
                60));

            mainSubscriber.Phone.CallReports.AddCall(new ReceiverCallReport(secondSubscriber.Phone.PhoneNumber,
                DateTime.Now, 20));
            mainSubscriber.Phone.CallReports.AddCall(new ReceiverCallReport(thirdSubscriber.Phone.PhoneNumber,
                DateTime.Today, 10));
            mainSubscriber.Phone.CallReports.AddCall(new ReceiverCallReport(thirdSubscriber.Phone.PhoneNumber,
                DateTime.Now, 90));
        }

        private static void PrintSubscriberCallReports(ISubscriber subscriber)
        {
            Console.WriteLine(string.Concat(Environment.NewLine, subscriber.FirstName, " ", subscriber.LastName, " calls:"));

            PrintCallReports(subscriber.Phone.CallReports.Calls);
        }

        private static void PrintFilteringByPhoneNumberCallReports(ICallReportRepository callReportRepository, IPhoneNumber phoneNumber)
        {
            PrintCallReports(callReportRepository.FilterByPhoneNumber(phoneNumber));
        }

        private static void PrintFilteringByCallDateCallReports(ICallReportRepository callReportRepository, DateTime dateTime)
        {
            PrintCallReports(callReportRepository.FilterByCallDate(dateTime));
        }

        private static void PrintFilteringByDurationCallReports(ICallReportRepository callReportRepository, int duration)
        {
            PrintCallReports(callReportRepository.FilterByDuration(duration));
        }

        private static void PrintFilteringByWorthCallReports(ICallReportRepository callReportRepository, decimal worth)
        {
            PrintCallReports(callReportRepository.FilterByWorth(worth));
        }

        private static void PrintSortedCallReports(SortParameter filterParameter, 
            SortingParameter sortingParameter, ICallReportRepository callReportRepository)
        {
            switch (filterParameter)
            {
                case SortParameter.ByPhoneNumber:
                    callReportRepository.SortByPhoneNumber(sortingParameter);
                    Print(callReportRepository.Calls);
                    break;
                case SortParameter.ByCallDate:
                    callReportRepository.SortByCallDate(sortingParameter);
                    Print(callReportRepository.Calls);
                    break;
                case SortParameter.ByDuration:
                    callReportRepository.SortByDuration(sortingParameter);
                    Print(callReportRepository.Calls);
                    break;
                case SortParameter.ByWorth:
                    callReportRepository.SortByWorth(sortingParameter);
                    Print(callReportRepository.Calls);
                    break;
            }

            void Print(IEnumerable<ICallReport> callReports)
            {
                Console.WriteLine(string.Concat(Environment.NewLine,
                    $"Filtering call reports with {filterParameter} parameter: ", Environment.NewLine));

                PrintCallReports(callReports);
            }
        }

        private static void PrintCallReports(IEnumerable<ICallReport> callReports)
        {
            if (callReports.Count() is 0)
            {
                Console.WriteLine(string.Concat(Environment.NewLine, "No result found", Environment.NewLine));
            }

            foreach (var callReport in callReports)
            {
                if (callReport is CallerCallReport callerCallReport)
                {
                    Console.WriteLine(string.Concat(Environment.NewLine, "Incoming call number: ", callerCallReport.PhoneNumber.Number,
                        Environment.NewLine, "call duration: ", callerCallReport.CallDuration,
                        Environment.NewLine, "call date: ", callerCallReport.CallDate,
                        Environment.NewLine, "call fee: ", callerCallReport.Fee,
                        Environment.NewLine));
                }
                else
                {
                    Console.WriteLine(string.Concat(Environment.NewLine, "Incoming call number: ", callReport.PhoneNumber.Number,
                        Environment.NewLine, "call date: ", callReport.CallDate,
                        Environment.NewLine, "call duration: ", callReport.CallDuration,
                        Environment.NewLine));
                }
            }
        }

        private static IPhoneNumber CreateBelarusPhoneNumber(BelarusOperatorCode operatorCode, 
            ITariffPlan tariffPlan, string number, IPhoneNumbersRepository phoneNumbersRepository)
        {
            var phoneNumber = new BelarusPhoneNumber(operatorCode, tariffPlan, number);

            phoneNumbersRepository.AddNumber(phoneNumber);

            return phoneNumber;
        }

        private static IPhone CreatePhone(IPhoneNumber phoneNumber)
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
                            subscriberArgs.PhoneNumber),
                        subscriberArgs.Station));
        }
    }
}