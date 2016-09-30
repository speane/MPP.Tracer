using System.Collections.Generic;
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
        internal List<MethodTraceInfo> NestedMethods;
        internal int ParametersCount;

        internal MethodTraceInfo(MethodBase methodBase)
        {
            Name = methodBase.Name;
            ClassName = methodBase.DeclaringType?.ToString();
            ParametersCount = methodBase.GetParameters().Length;
            _timer = new Stopwatch();
            _timer.Start();
        }

        internal void AddNestedMethod(MethodTraceInfo method)
        {
            if (NestedMethods == null)
            {
                NestedMethods = new List<MethodTraceInfo>();
            }
            NestedMethods.Add(method);
        }

        internal void StopTrace()
        {
            _timer.Stop();
            ExecutionTime = _timer.ElapsedMilliseconds;
        }
    }
}