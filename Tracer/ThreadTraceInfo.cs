using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal class ThreadTraceInfo
    {
        public long ThreadId { get; set; }

        private Stack<MethodInfoNode> methodStack;

        public ThreadTraceInfo(long id)
        {
            ThreadId = id;
        }

        public void StartMethodNode(MethodInfoNode methodNode)
        {

        }
    }
}
