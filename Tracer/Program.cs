using System;
using System.Collections.Generic;
using System.Threading;
using Tracer.Classes;

namespace Tracer
{
    internal class Program
    {
        private static readonly Classes.Tracer Tracer = new Classes.Tracer();
        internal static ConsoleOutputFormatter ConsoleFormatter = new ConsoleOutputFormatter();
        internal static XmlOutputFormatter XmlFormatter = new XmlOutputFormatter();

        private static void Main()
        {
            Tracer.StartTrace();

            TestMethod1(10);

            Tracer.StopTrace();

            ConsoleFormatter.Format(Tracer.GetTraceResult());
            XmlFormatter.Format(Tracer.GetTraceResult());
            Console.ReadLine();
        }

        private static void TestMethod1(int sleepTime)
        {
            Tracer.StartTrace();

            Thread.Sleep(sleepTime);
            TestMethod2(20);

            Tracer.StopTrace();
        }

        private static void TestMethod2(int sleepTime)
        {
            Tracer.StartTrace();

            Thread.Sleep(sleepTime);
            TestMethod3(30);

            Tracer.StopTrace();
        }

        private static void TestMethod3(int sleepTime)
        {
            Tracer.StartTrace();

            Thread.Sleep(sleepTime);
            TestMethod4(40, 5);

            Tracer.StopTrace();
        }

        private static void TestMethod4(int sleepTime, int threadsCount)
        {
            Tracer.StartTrace();

            Thread.Sleep(sleepTime);
            var threads = new List<Thread>();

            for (var i = 0; i < threadsCount; i++)
            {
                var thread = new Thread(() => TestMethod5(10));
                threads.Add(thread);
                thread.Start();
            }

            for (var i = 0; i < 5; i++)
            {
                threads[i].Join();
            }

            Tracer.StopTrace();
        }

        private static void TestMethod5(int sleepTime)
        {
            Tracer.StartTrace();

            Thread.Sleep(sleepTime);

            Tracer.StopTrace();
        }
    }
}