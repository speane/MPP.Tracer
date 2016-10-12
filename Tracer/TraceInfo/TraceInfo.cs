using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal class TraceInfo
    {
        public Dictionary<long, ThreadTraceInfo> ThreadTraceDictionary { get; }

        public TraceInfo()
        {
            ThreadTraceDictionary = new Dictionary<long, ThreadTraceInfo>();
        }

        public void StartMethodNode(long threadId, MethodInfoNode methodInfoNode)
        {
            ThreadTraceInfo threadTraceInfo = GetThreadTraceInfo(threadId);
            if (threadTraceInfo == null)
            {
                threadTraceInfo = CreateThreadTraceInfo(threadId);
            }
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
            if (ThreadTraceDictionary.ContainsKey(threadId))
            {
                return ThreadTraceDictionary[threadId];
            }
            else
            {
                return null;
            }
        }

        private ThreadTraceInfo CreateThreadTraceInfo(long threadId)
        {
            ThreadTraceInfo traceInfo = new ThreadTraceInfo(threadId);
            ThreadTraceDictionary.Add(threadId, traceInfo);
            return traceInfo;
        }
    }
}
