using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace MPP_Lab1
{
    class Tracer
    {
        public TraceResult result = new TraceResult(0);
        private TraceResult node;

        private TraceResult isThreadExist(int id)
        {
            foreach (TraceResult node in result.childs)
            {
                if (node.IsThreadChild && node.ThreadId == id)
                {
                    return node;
                }
                    
            }
            return null;
        }
        private TraceResult isMethodExist(string name,TraceResult node)
        {
            foreach (TraceResult elem in node.childs)
            {
                if (elem.MethodName.Equals(name))
                {
                    return elem;
                }

            }
            return null;
        }
        private string GetMethodName(string name)
        {
            string result;
            result = name.Substring(name.IndexOf(' ') + 1);
            result = result.Substring(0, result.IndexOf('('));
            return result;
        }
        private int GetParamsNumber (string name)
        {
            int result = 0;
            int position = name.IndexOf('(');
             name = name.Substring(position + 1, name.Length - 2 - position);
            if (name.Length == 0)
                return result;
            if (!name.Contains(' '))
                return 1;
            else
            {
                foreach (char c in name)
                {
                    if (c == ',')
                        result++;
                }
                return result + 1;
            }
        }
        public void StartTrace()
        {
            Thread thread = Thread.CurrentThread;
            int id = thread.ManagedThreadId;
            TraceResult node = isThreadExist(id);
            if (node == null)
            {
                node = new TraceResult(id);
                result.childs.Add(node);
            }
            StackTrace trace = new StackTrace(false);
            StackFrame[]  frames = trace.GetFrames();
            for (int i = frames.Length-1;i>0;i--)
            {
                string methodName = GetMethodName(frames[i].GetMethod().ToString());

                int paramsNumber = GetParamsNumber(frames[i].GetMethod().ToString());
                TraceResult findedNode = isMethodExist(methodName, node);

                if (findedNode == null)
                {
                    findedNode = new TraceResult(methodName, "class", 0, paramsNumber);
                    node.childs.Add(findedNode);
                    node = findedNode;
                }
                else
                {
                    node = findedNode;
                }

            }
        }
    }
}
