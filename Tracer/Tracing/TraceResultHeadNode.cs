using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResultHeadNode
    {
        public long ThreadId { get; set; }
        public double ExecutionTime { get; set; }
        public LinkedList<TraceResultNode> ChildNodes { get; set; }
    }
}
