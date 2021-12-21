using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public class CallReportRepository : ICallReportRepository
    {
        private readonly IList<ICallReport> _calls = new List<ICallReport>();

        public IEnumerable<ICallReport> Calls => new ReadOnlyCollection<ICallReport>(_calls);

        public void AddCall(ICallReport callReport)
        {
            if (callReport is null)
            {
                throw new ArgumentNullException(nameof(callReport));
            }

            _calls.Add(callReport);
        }

        public IEnumerable<ICallReport> FilterByDuration(int duration)
        {
            return Calls.Where(cr => cr.CallDuration.Equals(duration));
        }

        public IEnumerable<ICallReport> FilterByWorth(decimal worth)
        {
            return Calls.OfType<CallerCallReport>().Where(cr => cr.Fee.Equals(worth));
        }

        public IEnumerable<ICallReport> FilterByPhoneNumber(IPhoneNumber phoneNumber)
        {
            return Calls.Where(cr => cr.PhoneNumber.Equals(phoneNumber));
        }

        public IEnumerable<ICallReport> SortByDuration(SortingParameter sortingParameter)
        {
            switch (sortingParameter)
            {
                case SortingParameter.Ascending:
                    return Calls.OrderBy(cr => cr.CallDuration);
                case SortingParameter.Descending:
                    return Calls.OrderByDescending(cr => cr.CallDuration);
                default:
                    throw new ArgumentException("Unexpected parameter!");
            }
        }

        public IEnumerable<ICallReport> SortByWorth(SortingParameter sortingParameter)
        {
            switch (sortingParameter)
            {
                case SortingParameter.Ascending:
                    return Calls.OfType<CallerCallReport>().OrderBy(cr => cr.Fee);
                case SortingParameter.Descending:
                    return Calls.OfType<CallerCallReport>().OrderByDescending(cr => cr.Fee);
                default:
                    throw new ArgumentException("Unexpected parameter!");
            }
        }

        public IEnumerable<ICallReport> SortByPhoneNumber(SortingParameter sortingParameter)
        {
            switch (sortingParameter)
            {
                case SortingParameter.Ascending:
                    return Calls.OrderBy(cr => cr.PhoneNumber.Number);
                case SortingParameter.Descending:
                    return Calls.OrderByDescending(cr => cr.PhoneNumber.Number);
                default:
                    throw new ArgumentException("Unexpected parameter!");
            }
        }
    }
}