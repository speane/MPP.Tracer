using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Trace
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    { 
        private static readonly string initialIndent = String.Empty;
        private static readonly string indentIncrease = "   ";

        public void Format(TotalTraceResult totalTraceResult)
        {
            foreach (List<TraceResult> listTraceResult in totalTraceResult.ThreadTraceResults)
            {
                double totalTime = 0;
                foreach (TraceResult traceResult in listTraceResult)
                {
                    totalTime += traceResult.RunTime;
                }
                Console.WriteLine("-> Thread ID: {0}; Total Time: {1}", listTraceResult[0].ThreadId, totalTime);               
                foreach(TraceResult traceResult in listTraceResult)
                {
                    Traverse(traceResult, initialIndent);
                }
                Console.WriteLine();
            }
            
        }

        private void PrintEntryInfo(TraceResult traceResult, string indent)
        {
            Console.WriteLine("{0}Method: {1}", indent, traceResult.MethodName);
            Console.WriteLine("{0}Class: {1}", indent, traceResult.ClassName);
            Console.WriteLine("{0}Params: {1}", indent, traceResult.ParamsCount);
            Console.WriteLine("{0}Time: {1} ms", indent, traceResult.RunTime);
        }

        private void Traverse(TraceResult traceResult, string indent)
        {            
            PrintEntryInfo(traceResult, indent);
            indent = indent + indentIncrease;
            foreach (TraceResult tr in traceResult.ChildTraceResults)
            {
                Traverse(tr, indent);
            }            
        }


    }
}
