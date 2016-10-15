using System.Collections.Generic;

namespace Tracer
{
    internal sealed class ThreadTraceInfo
    {
        public long ThreadId { get; set; }

        private readonly Stack<MethodInfoNode> _methodStack = new Stack<MethodInfoNode>();

        public LinkedList<MethodInfoNode> RootThreadMethodsList = new LinkedList<MethodInfoNode>();

        public ThreadTraceInfo(long id)
        {
            ThreadId = id;
        }

        public void StartMethodNode(MethodInfoNode methodNode)
        {
            if (_methodStack.Count == 0)
            {
                RootThreadMethodsList.AddLast(methodNode);
            }
            else
            {
                _methodStack.Peek().ChildInfoNodes.AddLast(methodNode);
            }
            _methodStack.Push(methodNode);
        }

        public void FinishLastMethod(MethodInfoNode methodNode)
        {
            if (_methodStack.Count != 0)
            {
                MethodInfoNode lastMethodNode = _methodStack.Pop();
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

        private bool SameMethods(MethodInfoNode firstNode, MethodInfoNode secondNode)
        {
            return (firstNode.MethodName != null) && (secondNode.MethodName != null) &&
                    firstNode.MethodName.Equals(secondNode.MethodName) &&
                    (firstNode.ParamsAmount == secondNode.ParamsAmount);
        }
    }
}
