﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Tracer
{
    interface ITraceResultFormatter
    {
        void FormatTraceResult(TraceResult traceResult);
    }
}
