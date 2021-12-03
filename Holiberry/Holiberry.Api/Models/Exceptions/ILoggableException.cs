using System;
using Holiberry.Api.Models.ServerLogs.Enums;

namespace Holiberry.Api.Models.Exceptions
{
    public interface ILoggableException<T> where T : Exception
    {
        T Exception { get; }

        dynamic DataToSerialization { get; set; }
        ServerLogLevelE LogLevel { get; set; }
        bool IsLoggingException { get; set; }


        public T LogException(dynamic dataToSerialization, ServerLogLevelE logLevel = ServerLogLevelE.Warning)
        {
            LogLevel = logLevel;
            DataToSerialization = dataToSerialization;
            IsLoggingException = true;

            return Exception;
        }
    }
}
