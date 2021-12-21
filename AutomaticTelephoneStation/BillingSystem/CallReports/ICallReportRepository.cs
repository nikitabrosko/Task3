using System.Collections.Generic;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public interface ICallReportRepository
    {
        IEnumerable<ICallReport> Calls { get; }
        void AddCall(ICallReport call);
    }
}