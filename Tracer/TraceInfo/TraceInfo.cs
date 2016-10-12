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
            if (threadTraceDictionary.ContainsKey(threadId))
            {
                return threadTraceDictionary[threadId];
            }
            else
            {
                return null;
            }
        }

        private ThreadTraceInfo CreateThreadTraceInfo(long threadId)
        {
            ThreadTraceInfo traceInfo = new ThreadTraceInfo(threadId);
            threadTraceDictionary.Add(threadId, traceInfo);
            return traceInfo;
        }
    }
}
