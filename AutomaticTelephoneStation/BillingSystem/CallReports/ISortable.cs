using System.Collections.Generic;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public interface ISortable
    {
        IEnumerable<ICallReport> SortByDuration(SortingParameter sortingParameter);
        IEnumerable<ICallReport> SortByWorth(SortingParameter sortingParameter);
        IEnumerable<ICallReport> SortByPhoneNumber(SortingParameter sortingParameter);
    }
}