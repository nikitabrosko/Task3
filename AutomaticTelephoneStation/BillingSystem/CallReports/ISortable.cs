using System.Collections.Generic;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public interface ISortable
    {
        void SortByDuration(SortingParameter sortingParameter);
        void SortByWorth(SortingParameter sortingParameter);
        void SortByPhoneNumber(SortingParameter sortingParameter);
        void SortByCallDate(SortingParameter sortingParameter);
    }
}