using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MPP.Tracer
{
    public class MethodNode
    {
        public string Name { get; }
        public DateTime Start { get; }
        private long totalTime;
        public long TotalTime
        {
            get
            {
                return totalTime;
            }
        }
        public DateTime stop;
        public DateTime Stop
        {
            get
            {
                return stop;
            }
            set
            {
                stop = value;
                totalTime = (stop - Start).Milliseconds;
            }
        }
        public string ClassName { get; }
        public int ParamsCount { get; }
        public List<MethodNode> ChildrenList { get; }
        public int Level { get; }
        public MethodNode ParentMethod { get; }

        public MethodNode(MethodBase Method, DateTime StartTime, MethodNode ParentMethod)
            : this(Method.Name, Method.DeclaringType.Name, ParentMethod)
        {
            ParamsCount = Method.GetParameters().Count();
            Start = StartTime;
            if (ParentMethod != null)
            {
                Level = ParentMethod.Level + 1;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public MethodNode()
            : this(string.Empty, string.Empty, null)
        {
            Level = 0;
        }

        private MethodNode(string Name,string ClassName,MethodNode ParentMethod)
        {
            this.ParentMethod = ParentMethod;
            this.Name = Name;
            this.ClassName = ClassName;
            this.ParentMethod = ParentMethod;
            ChildrenList = new List<MethodNode>();
        }
    }
}
