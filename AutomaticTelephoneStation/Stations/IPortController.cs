using System.Collections.Generic;
using AutomaticTelephoneStation.Ports;

namespace AutomaticTelephoneStation.Stations
{
    public interface IPortController
    {
        IEnumerable<IPort> Ports { get; }
        void AddPort(IPort port);
    }
}