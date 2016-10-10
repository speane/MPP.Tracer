using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLab
{
    public class TracerConsoleFormatter : ITraceFormatter
    {
        
        public void Format(TraceResult traceResult)
        {
            if (traceResult == null)
                throw new ArgumentNullException();
            Write(traceResult);
        }

        private void Write(TraceResult traceResult)
        {

            foreach (int threadId in traceResult.TracedThreads.Keys)
            {
                Stack<TracedMethodItem> callStack = new Stack<TracedMethodItem>
                    (new Stack<TracedMethodItem>(traceResult.TracedThreads[threadId]));
                long threadTime = 0;
                while (callStack.Count != 0)
                {
                    TracedMethodItem item = callStack.Pop();
                    Console.WriteLine("{0}: {1} {2} {3} {4} {5}ms",threadId, GetTabs(item.CallDepth), item.ClassName, item.Name, 
                        item.ArgCount, item.Timer.ElapsedMilliseconds);
                    threadTime += item.Timer.ElapsedMilliseconds;
                }
                Console.WriteLine("\nThread №{0} worked: {1}ms\n\n",threadId, threadTime);
            }
        }

        private String GetTabs(int callDepth)
        {
            return new String('\t', callDepth);
        }
    }   
}
