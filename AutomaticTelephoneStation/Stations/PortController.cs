using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutomaticTelephoneStation.Ports;

namespace AutomaticTelephoneStation.Stations
{
    public class PortController : IPortController
    {
        private readonly IList<IPort> _ports = new List<IPort>();

        public IEnumerable<IPort> Ports => new ReadOnlyCollection<IPort>(_ports);

        public void AddPort(IPort port)
        {
            if (port is null)
            {
                throw new ArgumentNullException(nameof(port));
            }

            _ports.Add(port);
        }
    }
}