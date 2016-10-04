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
        internal string name { get; set; }
        internal string className { get; set; }
        internal int argCount { get; set; }
        internal int callDepth { get; set; }
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
