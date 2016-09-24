using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace Trace
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private static volatile XmlTraceResultFormatter instance = null;
        private static readonly Object syncRoot = new Object();

        private string pathToXml;
        private Stack<XElement> currentMethodElementStack;

        private XmlTraceResultFormatter(string pathToXml)
        {
            this.pathToXml = pathToXml;
            currentMethodElementStack = new Stack<XElement>();
        }

        public static XmlTraceResultFormatter Instance(string pathToXml)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new XmlTraceResultFormatter(pathToXml);
                    }
                }
            }
            return instance;
        }

             

        public void Format(TotalTraceResult totalTraceResult)
        {
            XDocument xmlDoc;
            XElement rootElement;
            /*if (File.Exists(pathToXml))
            {
                xmlDoc = XDocument.Load(pathToXml);
                if (xmlDoc.Root == null)
                {
                    rootElement = new XElement("root");
                }
                else
                {
                    rootElement = xmlDoc.Root;
                }
            }
            else
            {*/
                xmlDoc = new XDocument();
                rootElement = new XElement("root");
                xmlDoc.Add(rootElement);
            //}
            lock (syncRoot)
            { 
                foreach (TraceResult traceResult in totalTraceResult.ThreadTraceResults)
                {
                    XElement threadElement = new XElement("thread", new XAttribute("id", traceResult.ThreadId));
                    rootElement.Add(threadElement);
                    Traverse(traceResult, threadElement);
                }
                xmlDoc.Save(pathToXml);
            }
        }

        private XElement PrintEntryInfo(TraceResult traceResult)
        {
            XElement methodElement = new XElement("method");

            XAttribute nameAttribute = new XAttribute("name", traceResult.MethodName);
            XAttribute classAttribute = new XAttribute("class", traceResult.ClassName);
            XAttribute paramsAttribute = new XAttribute("params", traceResult.ParamsCount.ToString());
            XAttribute timeAttribute = new XAttribute("time", traceResult.RunTime.ToString() + " ms");

            methodElement.Add(nameAttribute, classAttribute, paramsAttribute, timeAttribute);

            return methodElement;
        }

        private void Traverse(TraceResult traceResult, XElement parentElement)
        {
            XElement methodElement = PrintEntryInfo(traceResult);
            parentElement.Add(methodElement);
            currentMethodElementStack.Push(methodElement);
            foreach (TraceResult tr in traceResult.ChildTraceResults)
            {
                Traverse(tr, currentMethodElementStack.Peek());
            }
            currentMethodElementStack.Pop();

        }
    }
}
