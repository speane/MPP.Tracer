using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Tracer.Classes
{
    class MethodTraceInfo
    {
        private readonly Stopwatch _timer;

        internal string Name;
        internal string ClassName;
        internal string ExecutionTime;
        internal int ParametersCount;

        public MethodTraceInfo(MethodBase methodBase)
        {
            Name = methodBase.Name;
            ClassName = methodBase.DeclaringType?.ToString();
            ParametersCount = methodBase.GetParameters().Length;
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void StopTrace()
        {
            _timer.Stop();
        }

    }
}
