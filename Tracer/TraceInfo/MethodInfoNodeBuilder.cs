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
            MethodInfoNode methodNode = new MethodInfoNode();

            methodNode.StartTime = DateTime.Now;
            methodNode.StopTime = DateTime.Now;
            methodNode.MethodName = SystemInfoUtils.GetParentMethodName();
            methodNode.ParamsAmount = SystemInfoUtils.GetParentMethodParamsAmount();

            return methodNode;
        }
    }
}
