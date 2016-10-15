using System;
using System.Runtime.Serialization;

namespace Tracer.Exceptions
{
    [Serializable]
    internal class FinishBeforeStartException : Exception
    {
        public FinishBeforeStartException()
        {
        }

        public FinishBeforeStartException(string message) : base(message)
        {
        }

        public FinishBeforeStartException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FinishBeforeStartException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}