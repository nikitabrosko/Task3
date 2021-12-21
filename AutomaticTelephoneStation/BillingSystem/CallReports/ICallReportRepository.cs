using System.Collections.Generic;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public interface ICallReportRepository : IFilterable, ISortable
    {
        IEnumerable<ICallReport> Calls { get; }
        void AddCall(ICallReport call);
    }
}