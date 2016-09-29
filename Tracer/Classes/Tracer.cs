using System.Diagnostics;
using System.Threading;
using Tracer.Interfaces;

namespace Tracer.Classes
{
    public class Tracer : ITracer
    {
        private readonly TraceResult _traceResult = new TraceResult();

        public void StartTrace()
        {
            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1).GetMethod();

            _traceResult.StartThreadTrace(Thread.CurrentThread.ManagedThreadId, method);
        }

        public void StopTrace()
        {
            _traceResult.StopThreadTrace(Thread.CurrentThread.ManagedThreadId);
        }

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }
    }
}