using System;
using System.Runtime.Serialization;
using Holiberry.Api.Models.ServerLogs.Enums;

namespace Holiberry.Api.Models.Exceptions
{
    public class ForbiddenAccessException : Exception, ILoggableException<ForbiddenAccessException>
    {
        public ForbiddenAccessException()
        {
        }

        public ForbiddenAccessException(string message) : base(message)
        {
        }

        public ForbiddenAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ForbiddenAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }


        public ForbiddenAccessException Exception => this;
        public dynamic DataToSerialization { get; set; }
        public ServerLogLevelE LogLevel { get; set; }
        public bool IsLoggingException { get; set; }
    }
}
