﻿using Tracer.Classes;

namespace Tracer.Interfaces
{
    internal interface ITracer
    {
        // метод вызывается в начале замеряемого метода
        void StartTrace();

        // метод вызывается в конце замеряемого метода
        void StopTrace();

        // возвращает объект с результатами измерений
        TraceResult GetTraceResult();
    }
}