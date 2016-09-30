using System.Xml.Linq;
using Tracer.Interfaces;

namespace Tracer.Classes
{
    class XmlOutputFormatter: ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            var document = new XDocument();
            var documentRoot = new XElement("root");
          

            foreach (var thread in traceResult.TracedThreads)
            {
                var threadElement = new XElement("thread");
                threadElement.Add(new XAttribute("id", thread.Key));
                threadElement.Add(new XAttribute("time", thread.Value.ExecutionTime));

                foreach (var method in thread.Value.TracedMethods)
                {
                    var methodElement = new XElement("method");

                    methodElement.Add(new XAttribute("name", method.Name));
                    methodElement.Add(new XAttribute("class", method.ClassName));
                    methodElement.Add(new XAttribute("parameters", method.ParametersCount));
                    methodElement.Add(new XAttribute("time", method.ExecutionTime));

                    if (method.NestedMethods != null)
                    foreach (var nestedMethod in method.NestedMethods)
                    {
                        var nestedMethodElement = new XElement("method");

                        nestedMethodElement.Add(new XAttribute("name", nestedMethod.Name));
                        nestedMethodElement.Add(new XAttribute("class", nestedMethod.ClassName));
                        nestedMethodElement.Add(new XAttribute("parameters", nestedMethod.ParametersCount));
                        nestedMethodElement.Add(new XAttribute("time", nestedMethod.ExecutionTime));

                        methodElement.Add(nestedMethodElement);
                    }

                    threadElement.Add(methodElement);
                }

                documentRoot.Add(threadElement);
            }

            document.Add(documentRoot);
            document.Save("traceResult.xml");
        }
    }
}
