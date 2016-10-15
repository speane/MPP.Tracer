namespace Tracer
{
    internal sealed class MethodInfoNodeBuilder
    {
        public static MethodInfoNode CreateMethodInfoNode()
        {
            const int METHOD_DEPTH = 3;

            SystemInfoUtils systemUtils = new SystemInfoUtils();
            MethodInfoNode methodNode = new MethodInfoNode
            {
                StartTime = systemUtils.GetCurrentTime(),
                StopTime = systemUtils.GetCurrentTime(),
                MethodName = systemUtils.GetMethodName(METHOD_DEPTH),
                ClassName = systemUtils.GetClassName(METHOD_DEPTH),
                ParamsAmount = systemUtils.GetMethodParamsAmount(METHOD_DEPTH)
            };

            return methodNode;
        }
    }
}
