using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Tracing.Formatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            Console.WriteLine("root");
            foreach (TraceResultHeadNode tempHeadNode in traceResult.RootNodes)
            {
                PrintHeadNode(tempHeadNode);
            }
        }

        private void PrintHeadNode(TraceResultHeadNode headNode)
        {
            Console.WriteLine("thread id={0} time={1}ms", headNode.ThreadId, Math.Round(headNode.ExecutionTime));
            if (headNode.ChildNodes != null)
            {
                foreach (TraceResultNode tempChildNode in headNode.ChildNodes) {
                    PrintNode(tempChildNode, 1);
                }
            }
        }

        private void PrintNode(TraceResultNode node, int depth)
        {
            Console.WriteLine("{0} method name={1} class={2} time={3}ms params={4}", GetIndentLine(depth), 
                node.MethodName, node.ClassName, Math.Round(node.ExecutionTime), node.ParamsAmount);
            if (node.ChildNodes != null)
            {
                foreach (TraceResultNode tempChildNode in node.ChildNodes)
                {
                    PrintNode(tempChildNode, depth + 1);
                }
            }
        }

        private string GetIndentLine(int length)
        {
            string INDENT_SYMBOL = "-";
            string indentString = "";
            for (int i = 0; i < length; i++)
            {
                indentString += INDENT_SYMBOL;
            }
            return indentString;
        }
    }
}
