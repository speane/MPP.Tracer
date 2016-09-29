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
        public Dictionary<int, Stack<TracedMethodItem>> tracedThreads;

        public TraceResult()
        {
            tracedThreads = new Dictionary<int, Stack<TracedMethodItem>>();
        }

        public void AddInnerMethod(int threadId, TracedMethodItem item)
        {
            if (!tracedThreads.ContainsKey(threadId))
            {
                Stack<TracedMethodItem> callstack = new  Stack<TracedMethodItem>();
                callstack.Push(item);
                tracedThreads.Add(threadId, callstack);
            }
            else
            {
                Stack<TracedMethodItem> callstack = tracedThreads[threadId];
                callstack.Push(item);
            }
        }
        
    }
}
