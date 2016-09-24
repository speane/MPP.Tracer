using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Collections.Concurrent;

namespace Trace
{
    public class Tracer : ITracer
    {
        private static volatile Tracer instance = null;
        private static readonly Object syncRoot = new Object();
        private static readonly Object syncThreadTraceInfoDictionary = new Object();

        private Dictionary<int, ThreadTraceInfo> ThreadTraceInfoDictionary { get; set; }

        private Tracer() 
        {
            ThreadTraceInfoDictionary = new Dictionary<int, ThreadTraceInfo>();   
        }

        public static Tracer Instance()
        {
            if (instance == null)
            {
                lock(syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new Tracer();
                    }
                }
            }
            return instance;
        }

        public void StartTrace()
        {
            DateTime startTime = DateTime.UtcNow;
            int threadId = Thread.CurrentThread.ManagedThreadId;
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            string methodName = methodBase.Name;
            string className = methodBase.DeclaringType.ToString();
            int paramsCount = methodBase.GetParameters().Length;
            TraceResult traceResult = new TraceResult(threadId, methodName, className, paramsCount, startTime);

            lock(syncThreadTraceInfoDictionary)
            {
                if (!ThreadTraceInfoDictionary.ContainsKey(threadId))
                {
                    ThreadTraceInfoDictionary.Add(threadId, new ThreadTraceInfo(traceResult));
                    ThreadTraceInfoDictionary[threadId].startedTraces.Push(traceResult);
                }
                else
                {
                    TraceResult previousTraceResult = ThreadTraceInfoDictionary[threadId].startedTraces.Peek();
                    previousTraceResult.AddChild(traceResult);
                    ThreadTraceInfoDictionary[threadId].startedTraces.Push(traceResult);
                }
            }
        }

        public void StopTrace()
        {
            DateTime stopTime = DateTime.UtcNow;
            int threadId = Thread.CurrentThread.ManagedThreadId;
            TraceResult currentTraceResult = ThreadTraceInfoDictionary[threadId].startedTraces.Pop();
            currentTraceResult.StopTime = stopTime;
        }


        public TotalTraceResult GetTraceResult()
        {
            TotalTraceResult totalTraceResult = new TotalTraceResult();
            lock (syncThreadTraceInfoDictionary)
            {
                foreach (KeyValuePair<int, ThreadTraceInfo> entry in ThreadTraceInfoDictionary)
                {
                    if (entry.Value.startedTraces.Count == 0)
                    {
                        totalTraceResult.ThreadTraceResults.Add(entry.Value.ThreadRootTraceResult);
                    }
                }
            }
            return totalTraceResult;
            
        }
    }
}
