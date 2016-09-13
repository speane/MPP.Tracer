using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;

namespace Tester
{
    class Program
    {
        static Tracer.Tracer Tracer = new Tracer.Tracer();
        static void Main(string[] args)
        {
            Tracer.StartTrace();
            RunTest(20000000);
            Tracer.StopTrace();
            Console.WriteLine(Tracer.GetResult().ToString());
        }

        private static void RunTest(int cycles)
        {
            Tracer.Tracer Tracer = new Tracer.Tracer();
            Tracer.StartTrace();
            for (int i = 0; i < cycles; i++)
            {
                int a = cycles * cycles;
            }
            Tracer.StopTrace();
            Console.WriteLine(Tracer.GetResult().ToString());
        }
    }
}
