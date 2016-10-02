using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLab
{
    public class TracerConsoleFormatter : ITraceFormatter
    {
        public TracerConsoleFormatter()
        {

        }

        public void Format(TraceResult traceResult)
        {
            if (traceResult == null)
                throw new ArgumentNullException();
            Write(traceResult);
        }

        private void Write(TraceResult traceResult)
        {

            foreach (int threadId in traceResult.tracedThreads.Keys)
            {
                Stack<TracedMethodItem> callStack = traceResult.tracedThreads[threadId];
                while (callStack.Count != 0)
                {
                    TracedMethodItem item = callStack.Pop();
                    Console.WriteLine("{0}: {1} {2} {3} {4} {5}ms",threadId, GetTabs(item.callDepth), item.className, item.name, 
                        item.argCount, item.timer.ElapsedMilliseconds);
                }
                Console.WriteLine("\n");
            }
        }

        private String GetTabs(int callDepth)
        {
            return new String('\t', callDepth);
        }
    }   
}
