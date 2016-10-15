using System.Collections.Generic;

namespace Tracer
{
    public sealed class TraceResult
    {
        public LinkedList<TraceResultHeadNode> RootNodes { get; set; }
    }
}
