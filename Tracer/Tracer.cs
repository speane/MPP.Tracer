using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace TracerLab
{
    public class Tracer : ITracer
    {

        private static Tracer _instance; 
        private static readonly object Lock = new object();
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
            if (!tracedThreads.ContainsKey(threadId))
            {
                tracedThreads.Add(threadId, new Stack<TracedMethodItem>());
            }
            else
            {
                tracedThreads[threadId].Peek().timer.Stop();
            }
            tracedThreads[threadId].Push(tmi);            
        }

        public void StopTrace()
        {
            int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
            TracedMethodItem tmi = tracedThreads[threadId].Pop();
            tmi.timer.Stop();
            result.AddInnerMethod(threadId, tmi);
            if (tracedThreads[threadId].Count != 0)
                tracedThreads[threadId].Peek().timer.Start();
        }

        public TracedMethodItem CreateTracedMethodItem()
        {
            StackFrame sf = new StackTrace().GetFrame(2);
            System.Reflection.MethodBase mb = sf.GetMethod();
            string name = mb.Name;
            string className = mb.DeclaringType.Name;
            int argCount = mb.GetParameters().Length;
            return new TracedMethodItem(name, className, argCount);
        }

        public string GetResult()
        {
            string s = "";
            foreach(int key in result.tracedThreads.Keys)
            {
                while (result.tracedThreads[key].Count != 0)
                {
                    TracedMethodItem tmi = result.tracedThreads[key].Pop();
                    s = s + key.ToString() + " " + tmi.className + " " + tmi.name + " " + tmi.argCount + " " + tmi.timer.ElapsedMilliseconds + "\n\r";
                }
            }
            return s;

        }

    }
}
