using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Tracer
{
    public class Tracer : ITracer
    {

        private static Tracer _instance; 
        private static readonly object Lock;
        private static Dictionary<int, Stack<TracedMethodItem>> tracedThreads = new Dictionary<int,Stack<TracedMethodItem>>();
        private static TraceResult result = new TraceResult();

        public static Tracer getInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
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

        private TraceResult _traceResult;

        private Tracer()
        {
            _traceResult = new TraceResult();
        }

        public void StartTrace()
        {
            TracedMethodItem tmi = CreateTracedMethodItem();
            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            tracedThread(threadId, tmi);
        }

        public void StopTrace()
        {

        }

        public TracedMethodItem CreateTracedMethodItem()
        {
            StackFrame sf = new StackTrace().GetFrame(1);
            System.Reflection.MethodBase mb = sf.GetMethod();
            string name = mb.Name;
            string className = mb.DeclaringType.Name;
            int argCount = mb.GetParameters().Length;
            return new TracedMethodItem(name, className, argCount);
        }

    }
}
