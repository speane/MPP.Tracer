using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal class TraceInfo
    {
        private Dictionary<long, ThreadTraceInfo> threadTraceDictionary = new Dictionary<long, ThreadTraceInfo>();

        public void StartMethodNode(MethodInfoNode methodInfoNode)
        {
            ThreadTraceInfo threadTraceInfo = GetThreadTraceInfo(methodInfoNode.ThreadId);
            threadTraceInfo.
        }

        private ThreadTraceInfo GetThreadTraceInfo(long threadId)
        {
            if (threadTraceDictionary.ContainsKey(threadId))
            {
                return threadTraceDictionary[threadId];
            }
            else
            {
                ThreadTraceInfo traceInfo = new ThreadTraceInfo(threadId);
                threadTraceDictionary.Add(threadId, traceInfo);
                return traceInfo;
            }
        }
    }
}
