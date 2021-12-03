using System;
using System.Runtime.Serialization;
using Holiberry.Api.Models.ServerLogs.Enums;

namespace Holiberry.Api.Models.Exceptions
{
    public class NotFoundException : Exception, ILoggableException<NotFoundException>
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public NotFoundException Exception => this;
        public dynamic DataToSerialization { get; set; }
        public ServerLogLevelE LogLevel { get; set; }
        public bool IsLoggingException { get; set; }
    }
}
