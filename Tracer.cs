using System;
using System.Diagnostics;
using System.Reflection;

namespace MPP.Tracer
{
    public class Tracer : ITracer
    {
        private static readonly Tracer instance = new Tracer();
        private TraceResult traceResult;
        public static Tracer Instance
        {
            get
            {
                return instance;
            }
        }
       
        static Tracer()
        {

        }

        private Tracer()
        {
            traceResult = new TraceResult();
        }

        public void EndTrace()
        {
            traceResult.FinishMethod(DateTime.Now);
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }

        public void StartTrace()
        {
            MethodBase Method = new StackTrace().GetFrame(1).GetMethod();
            traceResult.AddMethod(Method);
        }
    }
}
