using System.Xml.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MPP.Tracer
{
    public class XMLFormatter : ITraceResultFormatter
    {

        private string path;

        public XMLFormatter(string Path)
        {
            path = Path;
        }

        public void FormatTraceResult(TraceResult traceResult)
        {
            formatTraceResult(traceResult.ThreadsDictionary).Save(path);
        }

        private XElement formatTraceResult(ConcurrentDictionary<int, MethodTree> dictionary)
        {
            XElement root = new XElement("root");
            foreach (int ThreadId in dictionary.Keys)
            {
                root.Add(CreateXMLForThread(ThreadId, dictionary[ThreadId]));
            }
            return root;
        }

        private XElement CreateXMLForMethod(MethodNode node)
        {
            XElement element = new XElement("method",
                new XAttribute("name", node.Name),
                new XAttribute("className", node.ClassName),
                new XAttribute("paramsCount", node.ParamsCount),
                new XAttribute("totalTime", node.TotalTime));
            AddXMLToMethod(element, node.ChildrenList);
            return element;
        }

        private void AddXMLToMethod(XElement methodElement, IList<MethodNode> list)
        {
            foreach (MethodNode methodNode in list)
            {
                methodElement.Add(CreateXMLForMethod(methodNode));
            }
        }

        private XElement CreateXMLForThread(int ThreadId,MethodTree tree)
        {
            XElement threadElement = new XElement("thread",
              new XAttribute("id", ThreadId),
              new XAttribute("totalTime", tree.Root.TotalTime));
            AddXMLToThread(threadElement, tree.Root.ChildrenList);
            return threadElement;
        }

        private void AddXMLToThread(XElement threadElement, List<MethodNode> list)
        {
            foreach (MethodNode node in list)
            {
                threadElement.Add(CreateXMLForMethod(node));
            }
        }
    }
}
