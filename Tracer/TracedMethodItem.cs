using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace TracerLab
{
    public class TracedMethodItem
    {
        internal string Name { get; set; }
        internal string ClassName { get; set; }
        internal int ArgCount { get; set; }
        internal int CallDepth { get; set; }
        internal Stopwatch Timer { get; set; }
        

        public TracedMethodItem(string name, string className, int argCount, int callDepth)
        {
            this.Name = name;
            this.ClassName = className;
            this.ArgCount = argCount;
            this.CallDepth = callDepth;
            this.Timer = new Stopwatch();
            Timer.Start();
        }

    }
}
