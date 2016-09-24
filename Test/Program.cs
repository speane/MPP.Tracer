using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trace;
using System.Threading;

namespace Test
{
    class Program
    {
        private static readonly int sleepTimeCeiling = 500;
        private static readonly string pathToXml = "D:\\1.xml";

        private static volatile ITracer tracer = Tracer.Instance();
        private static volatile ITraceResultFormatter consoleFormatter = ConsoleTraceResultFormatter.Instance();
        private static volatile ITraceResultFormatter xmlFormatter = XmlTraceResultFormatter.Instance(pathToXml);
        private static Random rnd = new Random();
        

        private static void Func1()
        {
            tracer.StartTrace();
            Thread.Sleep(rnd.Next(sleepTimeCeiling));
            tracer.StopTrace();
        }

        private static void Func2()
        {
            tracer.StartTrace();
            Func1();
            Thread.Sleep(rnd.Next(sleepTimeCeiling));
            tracer.StopTrace();
        }

        private static void Func3()
        {
            tracer.StartTrace();
            Func2();
            Thread.Sleep(rnd.Next(sleepTimeCeiling));
            tracer.StopTrace();
        }

        private static void Func4()
        {
            tracer.StartTrace();
            Func1();
            Func2();
            Thread.Sleep(rnd.Next(sleepTimeCeiling));
            tracer.StopTrace();
        }

        private static void Func5()
        {
            tracer.StartTrace();
            Func4();
            Thread.Sleep(rnd.Next(sleepTimeCeiling));
            tracer.StopTrace();
        }


        static void Main(string[] args)
        {
            tracer.StartTrace();
            Func4();
            Thread thread1 = new Thread(Func5);
            thread1.Start();
            thread1.Join();
            Thread thread2 = new Thread(Func3);
            thread2.Start();
            thread2.Join();
            Thread.Sleep(rnd.Next(sleepTimeCeiling));
            tracer.StopTrace();
            TotalTraceResult totalTraceResult = tracer.GetTraceResult();
            consoleFormatter.Format(totalTraceResult);
            xmlFormatter.Format(totalTraceResult);
            
            Console.Read();
        }
    }
}
