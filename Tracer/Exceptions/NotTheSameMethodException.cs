using System;
using System.Runtime.Serialization;

namespace Tracer.Exceptions
{
    [Serializable]
    internal class NotTheSameMethodException : Exception
    {
        public NotTheSameMethodException()
        {
        }

        public NotTheSameMethodException(string message) : base(message)
        {
        }

        public NotTheSameMethodException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotTheSameMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}