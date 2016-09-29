using System.Diagnostics;
using System.Reflection;

namespace Tracer.Classes
{
    internal class MethodTraceInfo
    {
        private readonly Stopwatch _timer;
        internal string ClassName;
        internal long ExecutionTime;

        internal string Name;
        internal int ParametersCount;

        internal MethodTraceInfo(MethodBase methodBase)
        {
            Name = methodBase.Name;
            ClassName = methodBase.DeclaringType?.ToString();
            ParametersCount = methodBase.GetParameters().Length;
            _timer = new Stopwatch();
            _timer.Start();
        }

        internal void StopTrace()
        {
            _timer.Stop();
            ExecutionTime = _timer.ElapsedMilliseconds;
        }
    }
}