using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Collections.Concurrent;
namespace MPP_Lab1
{
    class Tracer : ITracer
    {
        private static volatile Tracer instance = null;
        private static readonly object syncRoot = new Object();
        private TraceResult result;
        private ConcurrentDictionary<int, Stack<TraceResult>> callstack;
        protected Tracer()
        {
            result = new TraceResult(0);
            callstack = new ConcurrentDictionary<int, Stack<TraceResult>>();
        }
        public static Tracer Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if(instance == null)
                    {
                        instance = new Tracer();
                    }
                }
            }
            return instance;
        }
        public void StartTrace()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Stack<TraceResult> stack = new Stack<TraceResult>();
            TraceResult node;
            if (!callstack.ContainsKey(id))
            {
                node = new TraceResult(id);
                result.childs.Add(node);
                stack.Push(node);
                callstack[id] = stack;
            }
            else
            {
                stack = callstack[id];
            }
            StackTrace trace = new StackTrace(false);
            var method = trace.GetFrame(1).GetMethod();
            string methodName = method.Name;
            string className = method.DeclaringType.ToString();
            int paramsNumber = method.GetParameters().Length;
            node = new TraceResult(methodName, className, 0, paramsNumber);
            stack.Peek().childs.Add(node);
            stack.Push(node);
            node.StartWatch();
        }
        public void StopTrace()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Stack<TraceResult> stack;
            if (callstack.ContainsKey(id))
            {
                stack = callstack[id];
            }
            else
                return;
            TraceResult node = stack.Pop();
            node.StopWatch();
            if (stack.Count == 1)
            {
                stack.Peek().GetEllapsedTime();
            }

        }
        public TraceResult GetTraceResult()
        {
            return result;
        }

    }
}
