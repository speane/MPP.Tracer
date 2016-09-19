using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Concurrent;

namespace MPP_Lab1
{
    class TraceResult
    {
        public ConcurrentBag<TraceResult> childs;// Видимо нехорошо делать public поле, как обойти это в случае списка??
        private string methodName;
        private string className;
        private long executionTime;
        private int parametrsNumber;
        private int threadId;
        private bool isThreadChild;
        private Stopwatch watch;
        public string MethodName { get { return methodName; } set { methodName = value ; } }
        public string ClassName { get { return className; } set { className = value; } }
        public long ExecutionTime { get { return executionTime; } set { executionTime = value; } }
        public int ParametrsNumber { get { return parametrsNumber; } set { parametrsNumber = value; } }
        public bool IsThreadChild { get { return isThreadChild; } }
        public int ThreadId { get { return threadId; } set { threadId = value; } }
        public void StartWatch()
        {
            watch.Start();
        }
        public void StopWatch()
        {
            watch.Stop();
            executionTime = watch.ElapsedMilliseconds;
        }
        public void GetEllapsedTime()
        {
            if (IsThreadChild)
            {
                executionTime = watch.ElapsedMilliseconds;
            }
        }
        public TraceResult (string name, string className, int time, int number)
        {
            methodName = name;
            this.className = className;
            executionTime = time;
            parametrsNumber = number;
            //threadId = id;
            isThreadChild = false;
            childs = new ConcurrentBag<TraceResult>();
            watch = new Stopwatch();
        }
        public TraceResult(int id)
        {
            threadId = id;
            isThreadChild = true;
            childs = new ConcurrentBag<TraceResult>();
            watch = new Stopwatch();
            watch.Start();
        }
    }
}
