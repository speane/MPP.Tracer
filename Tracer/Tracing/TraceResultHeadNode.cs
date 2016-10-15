using System.Collections.Generic;

namespace Tracer
{
    public sealed class TraceResultHeadNode
    {
        public long ThreadId { get; set; }
        public double ExecutionTime { get; set; }
        public LinkedList<TraceResultNode> ChildNodes { get; set; }
    }
}
