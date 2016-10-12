﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal class MethodInfoNode
    {
        public long ThreadId { get; set; }

        public string MethodName { get; set; }

        public int MethodParamsAmount { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime StopTime { get; set; }
    }
}
