using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace MPP_Lab1
{
    class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        private void Print(ConcurrentBag<TraceResult> result, string stackIndent)
        {
            foreach (TraceResult node in result)
            {
                if (node.IsThreadChild)
                {
                    Console.WriteLine("Thread ID: " + node.ThreadId);
                }
                else
                {
                    Console.WriteLine("{0}Method name:{1} time={2}ms class:{3} params={4}",
                    stackIndent,node.MethodName,node.ExecutionTime,node.ClassName,node.ParametrsNumber);
                }
                Print(node.childs, stackIndent + " ");
                stackIndent = stackIndent.Substring(0, stackIndent.Length - 1 < 0 ? 0 : stackIndent.Length - 1);
            }
        }
        public void Format(TraceResult result)
        {
            Print(result.childs, "");
        }
    }
}
