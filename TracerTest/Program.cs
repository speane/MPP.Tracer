using System;
using System.Collections.Generic;

using Tracer;
using Tracer.Tracing.Formatters;

using System.Threading;

namespace TracerTest
{
    public sealed class Program
    {
        private static readonly ITracer tracer = Tracer.Tracer.Instance;

        public static void Main(string[] args)
        {
            try
            {
                tracer.StartTrace();

                MethodOne();
                MethodThree();
                MethodThree();
                MethodThree();
                Thread thread = SingleThreadMethod();
                List<Thread> threads = MultiThreadMethod(3, 100);
                FFFMethod();

                tracer.StopTrace();

                thread.Join();
                foreach (Thread tempThread in threads)
                {
                    tempThread.Join();
                }

                TraceResult traceResult = tracer.GetTraceResult();

                PrintResult(traceResult);

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Error occured during program execution: {e}");
            }
        }

        private static void PrintResult(TraceResult traceResult)
        {
            string XML_FILE_NAME = "result.xml";
            ITraceResultFormatter formatter = null;

            formatter = new XmlTraceResultFormatter(XML_FILE_NAME);
            formatter.Format(traceResult);


            formatter = new ConsoleTraceResultFormatter();
            formatter.Format(traceResult);
        }

        private static void FFFMethod()
        {
            tracer.StartTrace();

            for (int i = 0; i < 7; i++)
            {
                MethodTwo();
            }

            tracer.StopTrace();
        }

        private static void MethodOne()
        {
            tracer.StartTrace();

            Thread.Sleep(100);

            tracer.StopTrace();
        }

        private static void MethodTwo()
        {
            tracer.StartTrace();

            Thread.Sleep(200);
            MethodOne();

            tracer.StopTrace();
        }

        private static void MethodThree()
        {
            tracer.StartTrace();

            Thread.Sleep(300);
            MethodOne();
            MethodTwo();

            tracer.StopTrace();
        }

        private static void SpecialMethod()
        {
            MethodTwo();
            MethodTwo();
        }

        private static Thread SingleThreadMethod()
        {
            Thread newThread = new Thread(MethodTwo);

            newThread.Start();

            return newThread;
        }

        private static List<Thread> MultiThreadMethod(int count, int wait)
        {
            tracer.StartTrace();

            Thread.Sleep(wait);

            List<Thread> threads = new List<Thread>();
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int methodNumber = random.Next(5);
                Thread tempThread;
                switch (methodNumber)
                {
                    case 0:
                        tempThread = new Thread(MethodOne);
                        break;
                    case 1:
                        tempThread = new Thread(MethodTwo);
                        break;
                    case 2:
                        tempThread = new Thread(MethodThree);
                        break;
                    default:
                        tempThread = new Thread(SpecialMethod);
                        break;
                }
                tempThread.Start();
                threads.Add(tempThread);
            }

            tracer.StopTrace();
            return threads;
        }
    }
}
