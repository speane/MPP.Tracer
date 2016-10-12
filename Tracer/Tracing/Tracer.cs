using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    class Tracer : ITracer
    {
        private TraceInfo traceInfo = new TraceInfo();

        public void StartTrace()
        {
            MethodInfoNode methodInfoNode = MethodInfoNodeBuilder.CreateMethodInfoNode();
            long threadId = SystemInfoUtils.GetThreadId();
            traceInfo.StartMethodNode(threadId, methodInfoNode);
        }

        public void StopTrace()
        {
            MethodInfoNode methodInfoNode = MethodInfoNodeBuilder.CreateMethodInfoNode();
            long threadId = SystemInfoUtils.GetThreadId();
            traceInfo.FinishMethodNode(threadId, methodInfoNode);
        }

        public TraceResult GetTraceResult()
        {
            return null;
        }
    }
}
