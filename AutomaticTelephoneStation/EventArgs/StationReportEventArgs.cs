using AutomaticTelephoneStation.Calls;

namespace AutomaticTelephoneStation.EventArgs
{
    public class StationReportEventArgs
    {
        public ICall CallReport { get; set; }

        public StationReportEventArgs(ICall call)
        {
            CallReport = call;
        }
    }
}