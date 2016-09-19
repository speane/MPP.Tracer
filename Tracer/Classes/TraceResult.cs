using System.Collections.Concurrent;
using System.Reflection;

namespace Tracer.Classes
{
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadTraceInfo> _tracedThreads;

        public TraceResult()
        {
            _tracedThreads = new ConcurrentDictionary<int, ThreadTraceInfo>();
        }

        public void StartThreadTrace(int threadId, MethodBase methodBase)
        {
            
            ThreadTraceInfo threadTraceInfo = _tracedThreads.GetOrAdd(threadId, new ThreadTraceInfo());
         
            threadTraceInfo.StartTraceMethod(methodBase);
        }

        public void StopThreadTrace(int threadId)
        {
            _tracedThreads[threadId].StopThreadTrace();
        }
    }
}