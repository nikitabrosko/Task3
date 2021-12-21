using System.Collections.Generic;
using AutomaticTelephoneStation.Calls;

namespace AutomaticTelephoneStation.CallReports
{
    public interface ICallReport
    {
        IEnumerable<ICall> Calls { get; }
        void AddCall(ICall call);
    }
}