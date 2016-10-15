namespace Tracer.Tracing.Formatters
{
    public interface ITraceResultFormatter
    {
        void Format(TraceResult traceResult);
    }
}
