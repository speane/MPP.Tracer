using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trace
{
    public class TotalTraceResult
    {       
        internal List<TraceResult> threadTraceResults;
        public IReadOnlyCollection<TraceResult> ThreadTraceResults { get { return threadTraceResults.AsReadOnly(); } }

        public TotalTraceResult()
        {
            threadTraceResults = new List<TraceResult>();
        }

    }
}
