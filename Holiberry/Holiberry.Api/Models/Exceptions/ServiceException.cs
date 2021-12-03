using System;
using System.Runtime.Serialization;
using Holiberry.Api.Models.ServerLogs.Enums;

namespace Holiberry.Api.Models.Exceptions
{
    public class ServiceException : Exception, ILoggableException<ServiceException>
    {
        public ServiceException()
        {
        }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ServiceException Exception => this;
        public dynamic DataToSerialization { get; set; }
        public ServerLogLevelE LogLevel { get; set; }
        public bool IsLoggingException { get; set; }
    }
}
