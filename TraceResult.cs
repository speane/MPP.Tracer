using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;

namespace MPP.Tracer
{
    public class TraceResult
    {
        public ConcurrentDictionary<int, MethodTree> ThreadsDictionary { get; }

        public TraceResult()
        {
            ThreadsDictionary = new ConcurrentDictionary<int, MethodTree>();
        }

        public void AddMethod(MethodBase CurrentMethod)
        {
            MethodTree thisTree = GetMethodTreeInstance();
            MethodNode parentMethod = thisTree.LastMethod;
            MethodNode CurrentMethodNode = new MethodNode(CurrentMethod,
                DateTime.Now, parentMethod);
            parentMethod.ChildrenList.Add(CurrentMethodNode);
            thisTree.LastMethod = CurrentMethodNode;
        }

        private MethodTree GetMethodTreeInstance()
        {
            int ThreadId = Thread.CurrentThread.ManagedThreadId;
            if (ThreadsDictionary.ContainsKey(ThreadId))
            {
                return ThreadsDictionary[ThreadId];
            }
            else
            {
                MethodTree tree = new MethodTree();
                ThreadsDictionary.TryAdd(ThreadId, tree);
                return tree;
            }
        }

        public void FinishMethod(DateTime StopTime)
        {
            int ThreadId = Thread.CurrentThread.ManagedThreadId;
            if (ThreadsDictionary.ContainsKey(ThreadId))
            {
                MethodTree thisTree = ThreadsDictionary[ThreadId];
                if ((thisTree == null)
                    || (thisTree.LastMethod == thisTree.Root))
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    MethodNode currentMethod = thisTree.LastMethod;
                    currentMethod.Stop = StopTime;
                    thisTree.LastMethod = currentMethod.ParentMethod;
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
