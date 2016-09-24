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
        private static volatile ConsoleTraceResultFormatter instance = null;
        private static readonly Object syncRoot = new Object();

        private static readonly string initialIndent = "";
        private static readonly string indentIncrease = "   ";

        private ConsoleTraceResultFormatter() { }

        public static ConsoleTraceResultFormatter Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new ConsoleTraceResultFormatter();
                    }
                }
            }
            return instance;
        }

        public void Format(TotalTraceResult totalTraceResult)
        {
            lock(syncRoot)
            {
                foreach (TraceResult traceResult in totalTraceResult.ThreadTraceResults)
                {
                    Console.WriteLine("-> Thread ID: {0}", traceResult.ThreadId);
                    Traverse(traceResult, initialIndent); ;
                    Console.WriteLine();
                }
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
