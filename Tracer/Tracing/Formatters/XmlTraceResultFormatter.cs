using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tracer.Tracing.Formatters
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private string filename;

        public XmlTraceResultFormatter(string filename)
        {
            this.filename = filename;
        }

        public void Format(TraceResult traceResult)
        {
            XDocument document = new XDocument();
            XElement rootElement = new XElement("root");
            AddHeadNodes(rootElement, traceResult.RootNodes);
            document.Add(rootElement);
            document.Save(filename);
        }

        private void AddHeadNodes(XElement rootElement, LinkedList<TraceResultHeadNode> headNodes)
        {
            foreach (TraceResultHeadNode tempHeadNode in headNodes)
            {
                rootElement.Add(CreateThreadXmlElement(tempHeadNode));
            }
        }

        private XElement CreateThreadXmlElement(TraceResultHeadNode headNode)
        {
            XElement threadElement = new XElement("thread");
            threadElement.SetAttributeValue("time", headNode.ExecutionTime.ToString() + "ms");
            threadElement.SetAttributeValue("id", headNode.ThreadId);

            AddChildElements(threadElement, headNode.ChildNodes);

            return threadElement;
        }

        private XElement CreateMethodElement(TraceResultNode node)
        {
            XElement methodElement = new XElement("method");
            methodElement.SetAttributeValue("params", node.ParamsAmount);
            methodElement.SetAttributeValue("class", node.ClassName);
            methodElement.SetAttributeValue("time", node.ExecutionTime.ToString() + "ms");
            methodElement.SetAttributeValue("name", node.MethodName);

            if (node.ChildNodes != null)
            {
                AddChildElements(methodElement, node.ChildNodes);
            }

            return methodElement;
        }

        private void AddChildElements(XElement parentElement, LinkedList<TraceResultNode> childNodes)
        {
            foreach (TraceResultNode tempNode in childNodes)
            {
                parentElement.Add(CreateMethodElement(tempNode));
            }
        }
    }
}
