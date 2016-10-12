using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal class MethodInfoNode
    {
        public MethodInfoNode()
        {
            ChildInfoNodes = new LinkedList<MethodInfoNode>();
        }

        public string MethodName { get; set; }

        public int ParamsAmount { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }

        public LinkedList<MethodInfoNode> ChildInfoNodes { get; }
    }
}
