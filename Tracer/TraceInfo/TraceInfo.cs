using System.Collections.Generic;

namespace Tracer
{
    internal sealed class TraceInfo
    {
        public Dictionary<long, ThreadTraceInfo> ThreadTraceDictionary { get; }

        public TraceInfo()
        {
            ThreadTraceDictionary = new Dictionary<long, ThreadTraceInfo>();
        }

        public void StartMethodNode(long threadId, MethodInfoNode methodInfoNode)
        {
            ThreadTraceInfo threadTraceInfo = GetThreadTraceInfo(threadId) ?? CreateThreadTraceInfo(threadId);
            threadTraceInfo.StartMethodNode(methodInfoNode);
        }

        public void FinishMethodNode(long threadId, MethodInfoNode methodInfoNode)
        {
            ThreadTraceInfo threadTraceInfo = GetThreadTraceInfo(threadId);
            if (threadTraceInfo != null)
            {
                threadTraceInfo.FinishLastMethod(methodInfoNode);
            }
            else
            {
                throw new FinishBeforeStartException();
            }
        }

        private ThreadTraceInfo GetThreadTraceInfo(long threadId)
        {
            return ThreadTraceDictionary.ContainsKey(threadId) ? ThreadTraceDictionary[threadId] : null;
        }

        private ThreadTraceInfo CreateThreadTraceInfo(long threadId)
        {
            ThreadTraceInfo traceInfo = new ThreadTraceInfo(threadId);
            ThreadTraceDictionary.Add(threadId, traceInfo);
            return traceInfo;
        }
    }
}
