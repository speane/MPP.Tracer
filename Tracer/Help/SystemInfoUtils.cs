using System;
using System.Linq;
using System.Threading;
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
            MethodBase method = GetMethod(depth + 1);
            return method.Name;
        }

        public string GetClassName(int depth)
        {
            MethodBase method = GetMethod(depth + 1);
            return method.DeclaringType?.Name;
        }

        public int GetMethodParamsAmount(int depth)
        {
            MethodBase method = GetMethod(depth + 1);
            return method.GetParameters().Count();
        }

        private MethodBase GetMethod(int depth)
        {
            StackTrace trace = new StackTrace(depth);
            StackFrame frame = trace.GetFrame(0);
            return frame.GetMethod();
        }
    }
}
