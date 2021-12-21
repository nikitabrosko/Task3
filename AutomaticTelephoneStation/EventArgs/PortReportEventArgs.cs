﻿using System;
using AutomaticTelephoneStation.CallReports;

namespace AutomaticTelephoneStation.EventArgs
{
    public class PortReportEventArgs
    {
        public ICallReport CallReport { get; set; }

        public PortReportEventArgs(ICallReport callReport)
        {
            CallReport = callReport ?? throw new ArgumentNullException(nameof(callReport));
        }
    }
}