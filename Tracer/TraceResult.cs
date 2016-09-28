using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        //id, callStack
        private Dictionary<int, Stack<TracedMethodItem>> tracedThreads;

        public TraceResult()
        {
            tracedThreads = new Dictionary<int, Stack<TracedMethodItem>>();
        }

        public void AddInnerMethod(int threadId, TracedMethodItem item)
        {
            Stack<TracedMethodItem> callstack = tracedThreads[threadId];
            if (callstack == null)
            {
                callstack = new Stack<TracedMethodItem>();
            }
            callstack.Push(item);
        }
    }
}
