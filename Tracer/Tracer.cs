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
            long threadId = 0;
            MethodInfoNode methodInfoNode = new MethodInfoNode();

            traceInfo.StartMethodNode(threadId, methodInfoNode);
        }

        public void StopTrace()
        {
            long threadId = 0;
            MethodInfoNode methodInfoNode = new MethodInfoNode();

            traceInfo.FinishMethodNode(threadId, methodInfoNode);
        }

        public TraceResult GetTraceResult()
        {
            return null;
        }
    }
}
