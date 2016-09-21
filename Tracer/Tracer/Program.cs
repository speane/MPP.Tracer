using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TracerLibrary;

namespace Tracer
{
    class StackTraceSample
    {
        public static TracerLibrary.Tracer tracer;
        public static void a()
        {
            tracer.StartTrace();
            Thread.Sleep(1000);
            tracer.StopTrace();

        }

        static void Main(string[] args)
        {
            StackTraceSample sample = new StackTraceSample();
            ConsoleTraceResultFormatter consoleFormatter = new ConsoleTraceResultFormatter();
            XmlTraceResultFormatter xmlFormatter = new XmlTraceResultFormatter("trace_result.xml");
            tracer = TracerLibrary.Tracer.Instance();
            Thread th = new Thread(a);
            th.Start();
            Console.ReadLine();
            tracer.StartTrace();
            sample.MyPublicMethod();

            tracer.StopTrace();
            a();
            consoleFormatter.Format(tracer.GetTraceResult());
            xmlFormatter.Format(tracer.GetTraceResult());
            Console.ReadLine();
        }

        public void MyPublicMethod()
        {
            tracer.StartTrace();
            MyProtectedMethod();
            tracer.StopTrace();
        }

        protected void MyProtectedMethod()
        {
            tracer.StartTrace();

            MyInternalClass mic = new MyInternalClass();
            mic.ThrowsException(1, 2, 3);
            tracer.StopTrace();
        }

        class MyInternalClass
        {
            public void ThrowsException(int a, int b, int c)
            {

                tracer.StartTrace();
                for (int i = 0; i < 100000; i++)
                { }
                tracer.StopTrace();
            }
        }
    }
}
