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
            Thread.Sleep(1);
            RunTest(30000000);
            t.StopTrace();
            Console.WriteLine(t.GetResult());
            Console.ReadLine();
        }

        private static void RunTest(int cycles)
        {
            t.StartTrace();
            for (int i = 0; i < cycles; i++)
            {
                int a = cycles * cycles;
            }
            stest();
            t.StopTrace();
        }

        private static void stest()
        {
            t.StartTrace();
            Thread.Sleep(1);
            t.StopTrace();
        }
    }
}
