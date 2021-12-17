using AutomaticTelephoneStation.Calls;

namespace AutomaticTelephoneStation.EventArgs
{
    public class ResponseCallEventArgs
    {
        public CallState CallState { get; set; }

        public ResponseCallEventArgs(CallState callState)
        {
            CallState = callState;
        }
    }
}