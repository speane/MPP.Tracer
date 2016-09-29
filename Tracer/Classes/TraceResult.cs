using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Tracer.Classes
{
    public class TraceResult
    {
        internal ConcurrentDictionary<int, ThreadTraceInfo> TracedThreads;

        internal TraceResult()
        {
            TracedThreads = new ConcurrentDictionary<int, ThreadTraceInfo>();
        }

        internal void StartThreadTrace(int threadId, MethodBase methodBase)
        {
            var threadTraceInfo = TracedThreads.GetOrAdd(threadId, new ThreadTraceInfo());

            threadTraceInfo.StartTraceMethod(methodBase);
        }

        internal void StopThreadTrace(int threadId)
        {
            ThreadTraceInfo threadsTraceInfo;
            if (!TracedThreads.TryGetValue(threadId, out threadsTraceInfo))
            {
                throw new ArgumentException("invalid thread id");
            }
            threadsTraceInfo.StopThreadTrace();
        }
    }
}