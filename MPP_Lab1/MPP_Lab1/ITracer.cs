﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_Lab1
{
    interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();

       
    }
}
