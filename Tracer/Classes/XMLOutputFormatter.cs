using System.Xml.Linq;
using Tracer.Interfaces;

namespace Tracer.Classes
{
    class XMLOutputFormatter: ITraceResultFormatter
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

                    threadElement.Add(methodElement);
                }

                documentRoot.Add(threadElement);
            }

            document.Add(documentRoot);
            document.Save("traceResult.xml");
        }
    }
}
