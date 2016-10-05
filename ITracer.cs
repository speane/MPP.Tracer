
namespace MPP.Tracer
{
    public interface ITracer
    {
        void StartTrace();

        void EndTrace();

        TraceResult GetTraceResult();
    }
}
