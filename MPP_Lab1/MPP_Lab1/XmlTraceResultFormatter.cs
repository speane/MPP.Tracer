using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Xml.Linq;
namespace MPP_Lab1
{
    class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private void AddElement(List<TraceResult> result, XElement element)
        {
            XElement elem;
            foreach (TraceResult node in result)
            {
                if (node.IsThreadChild)
                {
                    elem = new XElement("thread", new XAttribute("id", Convert.ToString(node.ThreadId)), new XAttribute("time", Convert.ToString(node.ExecutionTime)));
                }
                else
                {
                    elem = new XElement("method", new XAttribute("name", node.MethodName), new XAttribute("time", Convert.ToString(node.ExecutionTime)),
                                        new XAttribute("class", node.ClassName), new XAttribute("params", Convert.ToString(node.ParametrsNumber)));
                }
                element.Add(elem);
                AddElement(node.childs, elem);
            }

        }
        public void Format(TraceResult result)
        {
            XElement element = new XElement("root");
            AddElement(result.childs, element);
            XDocument xDoc = new XDocument();
            xDoc.Add(element);
            xDoc.Save("tracer.xml");
        }
    }
}
