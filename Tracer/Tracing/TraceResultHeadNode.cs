using System.Collections.Generic;

namespace Tracer
{
    public class TraceResultHeadNode
    {
        public long ThreadId { get; set; }
        public double ExecutionTime { get; set; }
        public LinkedList<TraceResultNode> ChildNodes { get; set; }
    }
}
