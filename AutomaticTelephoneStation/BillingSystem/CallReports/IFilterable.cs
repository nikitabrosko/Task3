using System;
using System.Collections.Generic;
using AutomaticTelephoneStation.PhoneNumbers;

namespace AutomaticTelephoneStation.BillingSystem.CallReports
{
    public interface IFilterable
    {
        IEnumerable<ICallReport> FilterByDuration(int duration);
        IEnumerable<ICallReport> FilterByWorth(decimal worth);
        IEnumerable<ICallReport> FilterByPhoneNumber(IPhoneNumber phoneNumber);
        IEnumerable<ICallReport> FilterByCallDate(DateTime callDate);
    }
}