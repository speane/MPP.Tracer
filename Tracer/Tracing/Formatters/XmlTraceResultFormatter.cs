using System.Collections.Generic;
using System.Xml.Linq;

namespace Tracer.Tracing.Formatters
{
    public sealed class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private readonly string _filename;

        public XmlTraceResultFormatter(string filename)
        {
            _filename = filename;
        }

        public void Format(TraceResult traceResult)
        {
            XDocument document = new XDocument();
            XElement rootElement = new XElement("root");
            AddHeadNodes(rootElement, traceResult.RootNodes);
            document.Add(rootElement);
            document.Save(_filename);
        }

        private void AddHeadNodes(XContainer rootElement, IEnumerable<TraceResultHeadNode> headNodes)
        {
            foreach (TraceResultHeadNode tempHeadNode in headNodes)
            {
                rootElement.Add(CreateThreadXmlElement(tempHeadNode));
            }
        }

        private XElement CreateThreadXmlElement(TraceResultHeadNode headNode)
        {
            XElement threadElement = new XElement("thread");
            threadElement.SetAttributeValue("time", $"{headNode.ExecutionTime}ms");
            threadElement.SetAttributeValue("id", headNode.ThreadId);

            AddChildElements(threadElement, headNode.ChildNodes);

            return threadElement;
        }

        private XElement CreateMethodElement(TraceResultNode node)
        {
            XElement methodElement = new XElement("method");
            methodElement.SetAttributeValue("params", node.ParamsAmount);
            methodElement.SetAttributeValue("class", node.ClassName);
            methodElement.SetAttributeValue("time", $"{node.ExecutionTime}ms");
            methodElement.SetAttributeValue("name", node.MethodName);

            if (node.ChildNodes != null)
            {
                AddChildElements(methodElement, node.ChildNodes);
            }

            return methodElement;
        }

        private void AddChildElements(XContainer parentElement, IEnumerable<TraceResultNode> childNodes)
        {
            foreach (TraceResultNode tempNode in childNodes)
            {
                parentElement.Add(CreateMethodElement(tempNode));
            }
        }
    }
}
