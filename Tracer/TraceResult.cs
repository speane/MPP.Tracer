using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        private string className;
        private string methodName;
        private long interval;
        private int argCount;

        public TraceResult(string className, string methodName, long interval, int argCount)
        {
            this.className = className;
            this.methodName = methodName;
            this.interval = interval;
            this.argCount = argCount;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", className, methodName, interval, argCount);
        }
    }
}
