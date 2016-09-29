using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLab
{
    class TracerConsoleFormatter : ITraceFormatter
    {
        public void Format(TraceResult traceResult)
        {
            if (traceResult == null)
                throw new ArgumentNullException();
        }
    }
}
