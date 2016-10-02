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
        internal readonly string name;
        internal readonly string className;
        internal readonly int argCount;
        internal readonly int callDepth;
        internal Stopwatch timer { get; set; }
        

        public TracedMethodItem(string name, string className, int argCount, int callDepth)
        {
            this.name = name;
            this.className = className;
            this.argCount = argCount;
            this.callDepth = callDepth;
            this.timer = new Stopwatch();
            timer.Start();
        }

    }
}
