using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal class MethodInfoNodeBuilder
    {
        public static MethodInfoNode CreateMethodInfoNode()
        {
            int METHOD_DEPTH = 3;

            SystemInfoUtils systemUtils = new SystemInfoUtils();
            MethodInfoNode methodNode = new MethodInfoNode();

            methodNode.StartTime = systemUtils.GetCurrentTime();
            methodNode.StopTime = systemUtils.GetCurrentTime();
            methodNode.MethodName = systemUtils.GetMethodName(METHOD_DEPTH);
            methodNode.ClassName = systemUtils.GetClassName(METHOD_DEPTH);
            methodNode.ParamsAmount = systemUtils.GetMethodParamsAmount(METHOD_DEPTH);

            return methodNode;
        }
    }
}
