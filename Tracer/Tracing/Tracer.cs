namespace Tracer
{
    public class Tracer : ITracer
    {
        private static Tracer _instance;
        private static readonly object SyncRoot = new object();
        private readonly TraceInfo _traceInfo = new TraceInfo();

        public static Tracer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock(SyncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new Tracer();
                        }
                    }
                }
                return _instance;
            }
        }

        private Tracer() { }

        public void StartTrace()
        {
            SystemInfoUtils systemUtils = new SystemInfoUtils();
            MethodInfoNode methodInfoNode = MethodInfoNodeBuilder.CreateMethodInfoNode();
            long threadId = systemUtils.GetThreadId();
            lock (SyncRoot)
            {
                _traceInfo.StartMethodNode(threadId, methodInfoNode);
            }
        }

        public void StopTrace()
        {
            SystemInfoUtils systemUtils = new SystemInfoUtils();
            long threadId = systemUtils.GetThreadId();
            MethodInfoNode methodInfoNode = MethodInfoNodeBuilder.CreateMethodInfoNode();
            lock (SyncRoot)
            {
                _traceInfo.FinishMethodNode(threadId, methodInfoNode);
            }
        }

        public TraceResult GetTraceResult()
        {
            TraceResultBuilder traceResultBuilder = new TraceResultBuilder();
            TraceResult traceResult;
            lock (SyncRoot)
            {
                traceResult = traceResultBuilder.CreateTraceResult(_traceInfo);
            }
            return traceResult;
        }
    }
}
