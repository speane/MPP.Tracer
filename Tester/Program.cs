using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TracerLab;


namespace Tester
{
    class Program
    {
        public static Tracer t = Tracer.getInstance; 
        static void Main(string[] args)
        {
            t.StartTrace();
            FirstTest(0);
            SecondTest(0, 0);
            t.StopTrace();
            TracerConsoleFormatter tcf = new TracerConsoleFormatter();
            tcf.Format(t.GetResult());
            TracerXmlFormatter txf = new TracerXmlFormatter();
            txf.Format(t.GetResult());
            Console.Read();
        }

        private static void FirstTest(int arg1)
        {
            t.StartTrace();
            Thread.Sleep(10);
            List<Thread> threds = new List<Thread>();
            for (int i = 0; i < 2; i++)
            {
                threds.Add(new Thread(ThirdTest));
                threds.Last().Start();
            }

            foreach (var thread in threds)
            {
                thread.Join();
            }
            SecondTest(0, 0);
            t.StopTrace();
        }

        private static void SecondTest(int arg1, int arg2)
        {
            t.StartTrace();
            Thread.Sleep(20);
            ThirdTest();
            t.StopTrace();
        }

        private static void ThirdTest()
        {
            t.StartTrace();
            Thread.Sleep(30);
            t.StopTrace();
        }
    }
}
