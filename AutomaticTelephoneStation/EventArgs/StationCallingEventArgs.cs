﻿using System;
using AutomaticTelephoneStation.Calls;

namespace AutomaticTelephoneStation.EventArgs
{
    public class StationCallingEventArgs
    {
        public ICall Call { get; set; }

        public StationCallingEventArgs(ICall call)
        {
            Call = call ?? throw new ArgumentNullException(nameof(call));
        }
    }
}