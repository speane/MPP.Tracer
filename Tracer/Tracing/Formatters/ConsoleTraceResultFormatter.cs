using System;

namespace Tracer.Tracing.Formatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            foreach (TraceResultHeadNode tempHeadNode in traceResult.RootNodes)
            {
                PrintHeadNode(tempHeadNode);
            }
        }

        private void PrintHeadNode(TraceResultHeadNode headNode)
        {
            Console.WriteLine($"thread id={headNode.ThreadId} time={headNode.ExecutionTime}ms");
            if (headNode.ChildNodes == null) return;
            foreach (TraceResultNode tempChildNode in headNode.ChildNodes) {
                PrintNode(tempChildNode, 1);
            }
        }

        private void PrintNode(TraceResultNode node, int depth)
        {
            Console.WriteLine(
                $"{GetIndentLine(depth)} method name={node.MethodName} class={node.ClassName} " +
                $"time={node.ExecutionTime}ms params={node.ParamsAmount}");
            if (node.ChildNodes == null) return;
            foreach (TraceResultNode tempChildNode in node.ChildNodes)
            {
                PrintNode(tempChildNode, depth + 1);
            }
        }

        private string GetIndentLine(int length)
        {
            string INDENT_SYMBOL = "-";
            string indentString = "<";
            for (int i = 0; i < length; i++)
            {
                indentString += INDENT_SYMBOL;
            }
            return indentString;
        }
    }
}
