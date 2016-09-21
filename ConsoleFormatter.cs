using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Tracer
{
    public class ConsoleFormatter : ITraceResultFormatter
    {
        public void FormatTraceResult(TraceResult traceResult)
        {
            ConcurrentDictionary<int, MethodTree> dictionary = traceResult.ThreadsDictionary;
            foreach (int ThreadId in dictionary.Keys)
            {
                Console.WriteLine("ThreadId={0}", ThreadId);
                MethodTree thisTree = dictionary[ThreadId];
                foreach (MethodNode node in thisTree.BypassTree())
                {
                    Console.WriteLine("{0} Method={1} Class={2} Params={3} Time={4}",
                        GetTabString(node.Level), node.Name, node.ClassName,
                        node.ParamsCount, node.TotalTime);
                }
            }
        }
        
        private string GetTabString(int count)
        {
            return new StringBuilder().Append('\t', count).ToString();
        }
    }
}
