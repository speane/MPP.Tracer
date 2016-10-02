using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trace
{
    internal class ThreadTraceInfo
    {
        internal List<TraceResult> ThreadRootTraceResult { get; private set; }
        internal Stack<TraceResult> StartedTraces { get; private set; }

        internal ThreadTraceInfo(TraceResult traceResult)
        {
            ThreadRootTraceResult = new List<TraceResult>();
            ThreadRootTraceResult.Add(traceResult);
            StartedTraces = new Stack<TraceResult>();
        }

    }
}
