using AutomaticTelephoneStation.Phones;

namespace AutomaticTelephoneStation.EventArgs
{
    public class ConnectionStateEventArgs
    {
        public ConnectionState ConnectionState { get; set; }

        public ConnectionStateEventArgs(ConnectionState connectionState)
        {
            ConnectionState = connectionState;
        }
    }
}
