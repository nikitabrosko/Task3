using System;
using AutomaticTelephoneStation.Calls;

namespace AutomaticTelephoneStation.EventArgs
{
    public class StationReportEventArgs
    {
        public ICall CallReport { get; set; }

        public StationReportEventArgs(ICall callReport)
        {
            CallReport = callReport ?? throw new ArgumentNullException(nameof(callReport));
        }
    }
}