using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Tracer
{
    public class Tracer : ITracer
    {
        Stopwatch timer = new Stopwatch();

        public void StartTrace()
        {
            
            timer.Start();
        }

        public void StopTrace()
        {
            timer.Stop();

        }

        public TraceResult GetResult()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            string name = sf.GetMethod().Name;
            string className = sf.GetMethod().DeclaringType.Name;
            int argCount = sf.GetMethod().GetParameters().Length;
            long time = timer.ElapsedMilliseconds;
            return new TraceResult(className, name, time, argCount);
        }

    }
}
