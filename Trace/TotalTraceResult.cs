using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trace
{
    public class TotalTraceResult
    {       
        internal List<List<TraceResult>> ThreadTraceResults { get; private set; }

        public TotalTraceResult()
        {
            ThreadTraceResults = new List<List<TraceResult>>();
        }

    }
}
