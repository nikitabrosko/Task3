using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutomaticTelephoneStation.Calls;

namespace AutomaticTelephoneStation.CallReports
{
    public class CallReport : ICallReport
    {
        private readonly IList<ICall> _calls = new List<ICall>();

        public IEnumerable<ICall> Calls => new ReadOnlyCollection<ICall>(_calls);

        public void AddCall(ICall call)
        {
            if (call is null)
            {
                throw new ArgumentNullException(nameof(call));
            }

            _calls.Add(call);
        }
    }
}