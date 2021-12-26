using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public class CallReportRepository : ICallReportRepository
    {
        private IList<ICallReport> _calls = new List<ICallReport>();

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

        public IEnumerable<ICallReport> FilterByCallDate(DateTime callDate)
        {
            return Calls.Where(cr => cr.CallDate.Equals(callDate));
        }

        public void SortByDuration(SortingParameter sortingParameter)
        {
            switch (sortingParameter)
            {
                case SortingParameter.Ascending:
                    _calls = Calls
                        .OrderBy(cr => cr.CallDuration)
                        .ToList();
                    break;
                case SortingParameter.Descending:
                    _calls = Calls
                        .OrderByDescending(cr => cr.CallDuration)
                        .ToList();
                    break;
            }
        }

        public void SortByWorth(SortingParameter sortingParameter)
        {
            switch (sortingParameter)
            {
                case SortingParameter.Ascending:
                    _calls = Calls
                        .OfType<CallerCallReport>()
                        .OrderBy(cr => cr.Fee)
                        .Cast<ICallReport>()
                        .ToList();
                    break;
                case SortingParameter.Descending:
                    _calls = Calls
                        .OfType<CallerCallReport>()
                        .OrderByDescending(cr => cr.Fee)
                        .Cast<ICallReport>()
                        .ToList();
                    break;
            }
        }

        public void SortByPhoneNumber(SortingParameter sortingParameter)
        {
            switch (sortingParameter)
            {
                case SortingParameter.Ascending:
                    _calls = Calls
                        .OrderBy(cr => cr.PhoneNumber.Number)
                        .ToList();
                    break;
                case SortingParameter.Descending:
                    _calls = Calls
                        .OrderByDescending(cr => cr.PhoneNumber.Number)
                        .ToList();
                    break;
            }
        }

        public void SortByCallDate(SortingParameter sortingParameter)
        {
            switch (sortingParameter)
            {
                case SortingParameter.Ascending:
                    _calls = Calls
                        .OrderBy(cr => cr.CallDate)
                        .ToList();
                    break;
                case SortingParameter.Descending:
                    _calls = Calls
                        .OrderByDescending(cr => cr.CallDate)
                        .ToList();
                    break;
            }
        }
    }
}