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

        private Stack<MethodInfoNode> methodStack = new Stack<MethodInfoNode>();

        public LinkedList<MethodInfoNode> RootThreadMethodsList = new LinkedList<MethodInfoNode>();

        public ThreadTraceInfo(long id)
        {
            ThreadId = id;
        }

        public void StartMethodNode(MethodInfoNode methodNode)
        {
            if (methodStack.Count == 0)
            {
                RootThreadMethodsList.AddLast(methodNode);
            }
            else
            {
                methodStack.Peek().ChildInfoNodes.AddLast(methodNode);
            }
            methodStack.Push(methodNode);
        }

        public void FinishLastMethod(MethodInfoNode methodNode)
        {
            if (methodStack.Count != 0)
            {
                MethodInfoNode lastMethodNode = methodStack.Last();
                if (SameMethods(methodNode, lastMethodNode)) {
                    lastMethodNode.StopTime = methodNode.StopTime;
                }
                else
                {
                    throw new NotTheSameMethodException();
                }
            }
            else
            {
                throw new FinishBeforeStartException();
            }
        }

        private Boolean SameMethods(MethodInfoNode firstNode, MethodInfoNode secondNode)
        {
            return (firstNode.MethodName != null) && (secondNode != null) &&
                    !firstNode.MethodName.Equals(secondNode.MethodName) &&
                    (firstNode.ParamsAmount == secondNode.ParamsAmount);
        }
    }
}
