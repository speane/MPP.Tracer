using System;
using Tracer.Interfaces;

namespace Tracer.Classes
{
    internal class ConsoleOutputFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            foreach (var thread in traceResult.TracedThreads)
            {
                Console.WriteLine($"Thread id: " + thread.Key + ";elapsed milliseconds: " + thread.Value.ExecutionTime +
                                  ";methods count: " + thread.Value.TracedMethods.Count);
                foreach (var method in thread.Value.TracedMethods)
                {
                    Console.WriteLine($"    Method name: " + method.Name + ";Class name: " + method.ClassName +
                                      ";Parameters count: " + method.ParametersCount + ";Execution time: " +
                                      method.ExecutionTime);
                }
            }
        }
    }
}