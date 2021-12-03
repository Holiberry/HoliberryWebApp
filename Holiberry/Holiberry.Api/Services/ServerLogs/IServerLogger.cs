using System;
using System.Threading.Tasks;
using Holiberry.Api.Models.Exceptions;
using Holiberry.Api.Models.ServerLogs.Enums;

namespace Holiberry.Api.ServerLogs
{
    public interface IServerLogger
    {
        Task LogException(Exception ex, ServerLogLevelE serverLogLevel = ServerLogLevelE.Critical);
        Task LogException<T>(ILoggableException<T> ex) where T : Exception;
        Task LogCustomException(ServerLogLevelE serverLogLevel, string methodName, string message, string stacktrace, string innerMessage);
    }
}
