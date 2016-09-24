using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trace
{
    public class TotalTraceResult
    {       
        internal List<TraceResult> _ThreadTraceResults { get; private set; }
        public IReadOnlyCollection<TraceResult> ThreadTraceResults { get { return _ThreadTraceResults.AsReadOnly(); } }

        public TotalTraceResult()
        {
            _ThreadTraceResults = new List<TraceResult>();
        }

    }
}
