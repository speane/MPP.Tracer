using System.Collections.Generic;

namespace Tracer
{
    public sealed class TraceResultNode
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public double ExecutionTime { get; set; }
        public int ParamsAmount { get; set; }
        public LinkedList<TraceResultNode> ChildNodes { get; set; }
    }
}
