using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trace
{
    public class TotalTraceResult
    {       
        internal List<TraceResult> _threadTraceResults;
        public IReadOnlyCollection<TraceResult> ThreadTraceResults { get { return _threadTraceResults.AsReadOnly(); } }

        public TotalTraceResult()
        {
            _threadTraceResults = new List<TraceResult>();
        }

    }
}
