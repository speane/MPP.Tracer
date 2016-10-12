using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    internal class SystemInfoUtils
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        public long GetThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }

        public string GetMethodName(int depth)
        {
            MethodBase method = GetMethod(depth);
            return method.Name;
        }

        public int GetMethodParamsAmount(int depth)
        {
            MethodBase method = GetMethod(depth);
            return method.GetParameters().Count();
        }

        private MethodBase GetMethod(int depth)
        {
            StackTrace trace = new StackTrace(depth + 1);
            StackFrame frame = trace.GetFrame(0);
            return frame.GetMethod();
        }
    }
}
