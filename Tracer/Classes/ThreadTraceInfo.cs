using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Classes
{
    class ThreadTraceInfo
    {
        private readonly Stack<MethodTraceInfo> _callStack; 
        private readonly List<MethodTraceInfo> _tracedMethods;
        private readonly Stopwatch _timer;

        public ThreadTraceInfo()
        {
            _callStack = new Stack<MethodTraceInfo>();
            _tracedMethods = new List<MethodTraceInfo>();
            _timer =new Stopwatch();
            _timer.Start();
        }

        public void StartTraceMethod(MethodBase methodBase)
        {
            _callStack.Push(new MethodTraceInfo(methodBase));
            _tracedMethods.Add(new MethodTraceInfo(methodBase));
        }

        public void StopThreadTrace()
        {
            _callStack.Pop().StopTrace();
        }
    }
}
