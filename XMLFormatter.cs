using System.Linq;
using System;
using System.Xml.Linq;
using System.Collections.Concurrent;

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
            XElement root = new XElement("root");
            ConcurrentDictionary<int, MethodTree> dictionary = traceResult.ThreadsDictionary;
            foreach (int ThreadId in dictionary.Keys)
            {
                root.Add(CreateXMLForThread(ThreadId,dictionary[ThreadId]));
            }
            root.Save(path);
        }

        private XElement CreateXMLForMethod(MethodNode node)
        {
            XElement element = new XElement("method",
                new XAttribute("name", node.Name),
                new XAttribute("className", node.ClassName),
                new XAttribute("paramsCount", node.ParamsCount),
                new XAttribute("totalTime", node.TotalTime));
            foreach (MethodNode methodNode in node.ChildrenList)
            {
                element.Add(CreateXMLForMethod(methodNode));
            }
            return element;
        }

        private XElement CreateXMLForThread(int ThreadId,MethodTree tree)
        {
            XElement threadElement = new XElement("thread");
            threadElement.SetAttributeValue("id", ThreadId);
            foreach (MethodNode node in tree.Root.ChildrenList)
            {
                threadElement.Add(CreateXMLForMethod(node));
            }
            return threadElement;
        }
    }
}
