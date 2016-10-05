using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace TracerLab
{
    public class TracerXmlFormatter : ITraceFormatter
    {
        private readonly string xmlFilePath;
        public TracerXmlFormatter(string filePath = "TraceResult.xml")
        {
            xmlFilePath = filePath;
        }

        public void Format(TraceResult traceResult  )
        {
            if (traceResult == null)
            {
                throw new ArgumentNullException();
            }

            XDocument xDoc = new XDocument();
            XElement xEl = GetRootElement(traceResult);
            xDoc.Add(xEl);
            Save(xDoc, xmlFilePath);
        }

        private XElement GetRootElement(TraceResult traceResult)
        {
            XElement root = new XElement("root");
            foreach (int threadId in traceResult.TracedThreads.Keys)
            {
                XElement threadEl = new XElement("thread");
                Stack<TracedMethodItem> callStack = traceResult.TracedThreads[threadId];
                long threadTime = 0;
                XElement parentEl = threadEl;
                Stack<XElement> parentStack = new Stack<XElement>();
                parentStack.Push(parentEl);
                int callDepth = 0;
                while (callStack.Count != 0)
                {
                    TracedMethodItem item = callStack.Pop();
                    XElement methodEl = GetMethodElement(item);
                    parentStack.Peek().Add(methodEl);
                    if (item.CallDepth > callDepth)
                        parentStack.Push(methodEl);
                    threadTime += item.Timer.ElapsedMilliseconds;
                }
                threadEl.Add(new XAttribute("id", threadId), new XAttribute("time", threadTime));
                root.Add(threadEl);
            }

            return root;
        }

        private XElement GetMethodElement(TracedMethodItem item)
        {
            XElement methodEl = new XElement("method");
            methodEl.Add(new XAttribute("class", item.ClassName));
            methodEl.Add(new XAttribute("name", item.Name));
            methodEl.Add(new XAttribute("arguments", item.ArgCount));
            methodEl.Add(new XAttribute("time", item.Timer.ElapsedMilliseconds));
            return methodEl;
        }

        private void Save(XDocument xmlDocument, string path)
        {
            using (XmlWriter writer = XmlWriter.Create(path))
                xmlDocument.Save(writer);
        }
    }
}
