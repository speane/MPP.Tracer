using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tracer.Classes
{
    internal class ThreadTraceInfo
    {
        private readonly Stack<MethodTraceInfo> _callStack;
        internal List<MethodTraceInfo> TracedMethods;

        internal ThreadTraceInfo()
        {
            _callStack = new Stack<MethodTraceInfo>();
            TracedMethods = new List<MethodTraceInfo>();
        }

        internal long ExecutionTime => TracedMethods.Select(m => m.ExecutionTime).Sum();

        internal void StartTraceMethod(MethodBase methodBase)
        {
            var tracedMethod = new MethodTraceInfo(methodBase);
            _callStack.Push(tracedMethod);
            TracedMethods.Add(tracedMethod);
        }

        internal void StopThreadTrace()
        {
            _callStack.Peek().StopTrace();
            _callStack.Pop();
        }
    }
}