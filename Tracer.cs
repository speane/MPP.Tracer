using System;
using System.Diagnostics;
using System.Reflection;

namespace MPP.Tracer
{
    public class Tracer : ITracer
    {
        private static volatile Tracer instance = null;
        private static readonly object locker = new object();
        private TraceResult traceResult;
        public static Tracer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
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
