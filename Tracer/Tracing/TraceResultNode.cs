using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Tracing
{
    internal class TraceResultNode
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public double ExecutionTime { get; set; }
        public int ParamsAmount { get; set; }
        public LinkedList<TraceResultNode> childNodes { get; set; }
    }
}
