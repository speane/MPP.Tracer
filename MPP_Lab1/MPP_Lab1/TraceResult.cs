using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_Lab1
{
    class TraceResult
    {
        public List<TraceResult> childs;
        private string methodName;
        private string className;
        private int executionTime;
        private int parametrsNumber;
        private int threadId;
        public string MethodName { get { return methodName; } set { methodName = value ; } }
        public string ClassName { get { return className; } set { className = value; } }
        public int ExecutionTime { get { return executionTime; } set { executionTime = value; } }
        public int ParametrsNumber { get { return parametrsNumber; } set { parametrsNumber = value; } }
        public int ThreadId { get { return threadId; } set { threadId = value; } }
        public TraceResult (string name, string className, int time, int number, int id)
        {
            methodName = name;
            this.className = className;
            executionTime = time;
            parametrsNumber = number;
            threadId = id;
        }
    }
}
