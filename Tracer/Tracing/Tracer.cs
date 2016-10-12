using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    class Tracer : ITracer
    {
        private static Tracer instance;
        private static readonly object _syncRoot = new object();
        private TraceInfo traceInfo = new TraceInfo();

        public static Tracer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(_syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Tracer();
                        }
                    }
                }
                return instance;
            }
        }

        private Tracer() { }

        public void StartTrace()
        {
            SystemInfoUtils systemUtils = new SystemInfoUtils();
            MethodInfoNode methodInfoNode = MethodInfoNodeBuilder.CreateMethodInfoNode();
            long threadId = systemUtils.GetThreadId();
            lock (_syncRoot)
            {
                traceInfo.StartMethodNode(threadId, methodInfoNode);
            }
        }

        public void StopTrace()
        {
            SystemInfoUtils systemUtils = new SystemInfoUtils();
            MethodInfoNode methodInfoNode = MethodInfoNodeBuilder.CreateMethodInfoNode();
            long threadId = systemUtils.GetThreadId();
            lock (_syncRoot)
            {
                traceInfo.FinishMethodNode(threadId, methodInfoNode);
            }
        }

        public TraceResult GetTraceResult()
        {
            TraceResultBuilder traceResultBuilder = new TraceResultBuilder();
            TraceResult traceResult = new TraceResult();
            lock (_syncRoot)
            {
                traceResult = traceResultBuilder.CreateTraceResult(traceInfo);
            }
            return traceResult;
        }
    }
}
