using System;
using System.Collections.Generic;
using System.Linq;

namespace Tracer
{
    internal sealed class TraceResultBuilder
    {
        public TraceResult CreateTraceResult(TraceInfo traceInfo)
        {
            TraceResult traceResult = new TraceResult();
            LinkedList<TraceResultHeadNode> headNodes = new LinkedList<TraceResultHeadNode>();

            foreach (long tempThreadId in traceInfo.ThreadTraceDictionary.Keys)
            {
                headNodes.AddLast(CreateHeadNode(traceInfo.ThreadTraceDictionary[tempThreadId]));
            }

            traceResult.RootNodes = headNodes;

            return traceResult;
        } 

        private TraceResultHeadNode CreateHeadNode(ThreadTraceInfo threadTraceInfo)
        {
            TraceResultHeadNode headNode = new TraceResultHeadNode
            {
                ThreadId = threadTraceInfo.ThreadId,
                ChildNodes = CreateChildNodeList(threadTraceInfo.RootThreadMethodsList),
                ExecutionTime = Math.Round(GetThreadExecutionTime(threadTraceInfo))
            };
            return headNode;
        }

        private LinkedList<TraceResultNode> CreateChildNodeList(ICollection<MethodInfoNode> methodInfoNodes)
        {
            if (methodInfoNodes.Count == 0) return null;
            LinkedList<TraceResultNode> resultNodes = new LinkedList<TraceResultNode>();
            foreach (MethodInfoNode tempInfoNode in methodInfoNodes)
            {
                resultNodes.AddLast(CreateTraceResultNode(tempInfoNode));
            }
            return resultNodes;
        }

        private TraceResultNode CreateTraceResultNode(MethodInfoNode infoNode)
        {
            TraceResultNode resultNode = new TraceResultNode
            {
                ExecutionTime = Math.Round((infoNode.StopTime - infoNode.StartTime).TotalMilliseconds),
                MethodName = infoNode.MethodName,
                ClassName = infoNode.ClassName,
                ParamsAmount = infoNode.ParamsAmount,
                ChildNodes = CreateChildNodeList(infoNode.ChildInfoNodes)
            };

            return resultNode;
        }

        private static double GetThreadExecutionTime(ThreadTraceInfo threadTraceInfo)
        {
            MethodInfoNode firstMethod = threadTraceInfo.RootThreadMethodsList.First();
            MethodInfoNode lastMethod = threadTraceInfo.RootThreadMethodsList.Last();
            return Math.Round((lastMethod.StopTime - firstMethod.StartTime).TotalMilliseconds);
        }        
    }
}
