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
                Console.WriteLine($"Thread id: " + thread.Key + ";time(ms): " + thread.Value.ExecutionTime +
                                  ";methods: " + thread.Value.TracedMethods.Count);
                foreach (var method in thread.Value.TracedMethods)
                {
                    Console.WriteLine($"    Method: " + method.Name + ";Class: " + method.ClassName +
                                      ";Parameters: " + method.ParametersCount + ";time(ms): " +
                                      method.ExecutionTime);
                    if (method._nestedMethods != null)
                    {
                        foreach (var nestedMethod in method._nestedMethods)
                        {
                            Console.WriteLine($"        Method: " + nestedMethod.Name + ";Class: " +
                                              nestedMethod.ClassName +
                                              ";Parameters: " + nestedMethod.ParametersCount + ";time(ms): " +
                                              nestedMethod.ExecutionTime);
                        }
                    }
                }
            }
        }
    }
}