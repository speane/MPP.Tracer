using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLab
{
    interface ITraceFormatter
    {
        void Format(TraceResult traceResult);
    }
}
