using System.Collections.Generic;
using AutomaticTelephoneStation.Calls;

namespace AutomaticTelephoneStation.CallReports
{
    public interface ICallReportRepository
    {
        IEnumerable<ICallReport> Calls { get; }
        void AddCall(ICallReport call);
    }
}