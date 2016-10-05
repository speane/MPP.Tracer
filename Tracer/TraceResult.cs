using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLab
{
    public class TraceResult
    {
        //id, callStack
        public Dictionary<int, Stack<TracedMethodItem>> TracedThreads { get; set; }

        public TraceResult()
        {
            TracedThreads = new Dictionary<int, Stack<TracedMethodItem>>();
        }

        public void AddInnerMethod(int threadId, TracedMethodItem item)
        {
            if (!TracedThreads.ContainsKey(threadId))
            {
                Stack<TracedMethodItem> callstack = new  Stack<TracedMethodItem>();
                callstack.Push(item);
                TracedThreads.Add(threadId, callstack);
            }
            else
            {
                Stack<TracedMethodItem> callstack = TracedThreads[threadId];
                callstack.Push(item);
            }
        }
        
    }
}
