using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
    }
}