using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal class TraceResultBuilder
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
            TraceResultHeadNode headNode = new TraceResultHeadNode();
            headNode.ThreadId = threadTraceInfo.ThreadId;
            headNode.ChildNodes = CreateChildNodeList(threadTraceInfo.RootThreadMethodsList);
            headNode.ExecutionTime = Math.Round(GetThreadExecutionTime(threadTraceInfo));
            return headNode;
        }

        private LinkedList<TraceResultNode> CreateChildNodeList(LinkedList<MethodInfoNode> methodInfoNodes)
        {
            if (methodInfoNodes.Count != 0)
            {
                LinkedList<TraceResultNode> resultNodes = new LinkedList<TraceResultNode>();
                foreach (MethodInfoNode tempInfoNode in methodInfoNodes)
                {
                    resultNodes.AddLast(CreateTraceResultNode(tempInfoNode));
                }
                return resultNodes;
            }
            else
            {
                return null;
            }
        }

        private TraceResultNode CreateTraceResultNode(MethodInfoNode infoNode)
        {
            TraceResultNode resultNode = new TraceResultNode();
            resultNode.ExecutionTime = Math.Round((infoNode.StopTime - infoNode.StartTime).TotalMilliseconds);
            resultNode.MethodName = infoNode.MethodName;
            resultNode.ClassName = infoNode.ClassName;
            resultNode.ParamsAmount = infoNode.ParamsAmount;
            resultNode.ChildNodes = CreateChildNodeList(infoNode.ChildInfoNodes);

            return resultNode;
        }

        private double GetThreadExecutionTime(ThreadTraceInfo threadTraceInfo)
        {
            MethodInfoNode firstMethod = threadTraceInfo.RootThreadMethodsList.First();
            MethodInfoNode lastMethod = threadTraceInfo.RootThreadMethodsList.Last();
            return Math.Round((lastMethod.StopTime - firstMethod.StartTime).TotalMilliseconds);
        }        
    }
}
