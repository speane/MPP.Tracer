using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace Trace
{
    public class TraceResult
    {
        public List<TraceResult> ChildTraceResults { get; private set; }

        public int ThreadId { get; private set; }

        public string MethodName { get; private set; }

        public string ClassName { get; private set; }

        public int ParamsCount { get; private set; }

        public DateTime StartTime { get; private set; }

        public DateTime StopTime { get; internal set; }

        public double RunTime
        {
            get
            {  return StopTime.Subtract(StartTime).TotalMilliseconds; }
        }

        public TraceResult(int threadId, string methodName, string className, int paramsCount, DateTime startTime)
        {
            ThreadId = threadId;
            MethodName = methodName;
            ClassName = className;
            ParamsCount = paramsCount;
            StartTime = startTime;
            ChildTraceResults = new List<TraceResult>();
        }

        public void AddChild(TraceResult traceResult)
        {
            ChildTraceResults.Add(traceResult);
        }

    }
}
