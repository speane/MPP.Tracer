using System;
using System.Collections.Generic;

namespace Tracer
{
    internal class MethodInfoNode
    {
        public MethodInfoNode()
        {
            ChildInfoNodes = new LinkedList<MethodInfoNode>();
        }

        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public int ParamsAmount { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }

        public LinkedList<MethodInfoNode> ChildInfoNodes { get; }
    }
}
