using System;
using Tracer.Interfaces;

namespace Tracer.Classes
{
    internal sealed class ConsoleOutputFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            foreach (var thread in traceResult.TracedThreads)
            {
                Console.WriteLine(
                    $"Thread id: {thread.Key}; time(ms): {thread.Value.ExecutionTime}; methods: {thread.Value.TracedMethods.Count};");
                foreach (var method in thread.Value.TracedMethods)
                {
                    Console.WriteLine(
                        $"\tMethod: {method.Name}; Class: {method.ClassName}; Parameters: {method.ParametersCount}; time(ms): {method.ExecutionTime};");
                    if (method.NestedMethods != null)
                    {
                        foreach (var nestedMethod in method.NestedMethods)
                        {
                            FormatNestedMethods(nestedMethod);
                        }
                    }
                }
            }
        }

        private void FormatNestedMethods(MethodTraceInfo method, int nestingLevel = 2)
        {
            Console.Write(new string('\t', nestingLevel));
            Console.WriteLine(
                $"Method: {method.Name}; Class: {method.ClassName}; Parameters: {method.ParametersCount}; time(ms): {method.ExecutionTime};");

            if (method.NestedMethods != null)
            {
                foreach (var nestedMethod in method.NestedMethods)
                {
                    nestingLevel++;
                    FormatNestedMethods(nestedMethod, nestingLevel);
                }
            }
        }
    }
}