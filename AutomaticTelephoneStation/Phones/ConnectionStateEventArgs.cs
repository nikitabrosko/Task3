using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticTelephoneStation.Phones
{
    public class ConnectionStateEventArgs
    {
        public ConnectionState ConnectionState { get; }

        public ConnectionStateEventArgs(ConnectionState connectionState)
        {
            ConnectionState = connectionState;
        }
    }
}
