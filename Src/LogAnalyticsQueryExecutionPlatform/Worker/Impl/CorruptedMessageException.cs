using System;
using System.Runtime.Serialization;

namespace LogAnalyticsQueryExecutionPlatform.Impl
{
    [Serializable]
    internal class CorruptedMessageException : Exception
    {
        private Exception ex;

        public CorruptedMessageException()
        {
        }

        public CorruptedMessageException(Exception ex)
        {
            this.ex = ex;
        }

        public CorruptedMessageException(string message) : base(message)
        {
        }

        public CorruptedMessageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CorruptedMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}