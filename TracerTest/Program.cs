﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;
using System.Threading;

namespace TracerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ITracer tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();
            Thread.Sleep(209);
            tracer.StopTrace();
            TraceResult traceResult = tracer.GetTraceResult();
            Console.WriteLine("Thread id: {0}", traceResult.RootNodes.First().ThreadId);
            Console.ReadLine();
        }
    }
}
