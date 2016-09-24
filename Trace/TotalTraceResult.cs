using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trace
{
    public class TotalTraceResult
    {       
        internal List<TraceResult> ThreadTraceResults { get; private set; }
        public IReadOnlyCollection<TraceResult> ThreadTraceResultsReadOnly { get { return ThreadTraceResults.AsReadOnly(); } }

        public TotalTraceResult()
        {
            ThreadTraceResults = new List<TraceResult>();
        }

    }
}
