using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;
using Tracer.Tracing.Formatters;
using System.Threading;

namespace TracerTest
{
    class Program
    {
        private static ITracer tracer = Tracer.Tracer.Instance;

        static void Main(string[] args)
        {
            ITracer tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();
            MethodOne();
            tracer.StopTrace();
            TraceResult traceResult = tracer.GetTraceResult();

            ITraceResultFormatter formatter = new ConsoleTraceResultFormatter();
            formatter.Format(traceResult);

            Console.ReadLine();
        }

        static void MethodOne()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }
    }
}
