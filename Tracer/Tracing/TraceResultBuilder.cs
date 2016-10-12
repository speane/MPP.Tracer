﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Tracing
{
    internal class TraceResultBuilder
    {
        public TraceResult CreateTraceResult(TraceInfo traceInfo)
        {
            return null;
        } 

        private TraceResultHeadNode CreateHeadNode(ThreadTraceInfo threadTraceInfo)
        {
            TraceResultHeadNode headNode = new TraceResultHeadNode();
            headNode.ThreadId = threadTraceInfo.ThreadId;
            headNode
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
            TraceResultNode tempResultNode = new TraceResultNode();
            tempResultNode.ExecutionTime = (infoNode.StopTime - infoNode.StartTime).TotalMilliseconds;
            tempResultNode.MethodName = infoNode.MethodName;
            tempResultNode.ClassName = infoNode.ClassName;
            tempResultNode.ParamsAmount = infoNode.ParamsAmount;

            return tempResultNode;
        }
    }
}