using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Holiberry.Api.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Holiberry.Api.Extensions;
using Holiberry.Api.ServerLogs;
using Holiberry.Api.ServerLogs.Config;
using Holiberry.Api.Models.Exceptions;
using Holiberry.Api.Models.ServerLogs.Entities;
using Holiberry.Api.Models.ServerLogs.Enums;
using Newtonsoft.Json;

namespace Holiberry.Api.ServerLogs
{
    public class ServerLogger : IServerLogger
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public ServerLogger(
            ApplicationDbContext db,
            IConfiguration configuration,
            IHttpContextAccessor contextAccessor
            )
        {
            _db = db;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }


        public async Task LogException(Exception ex, ServerLogLevelE serverLogLevel = ServerLogLevelE.Critical)
        {

            StackTrace st = new StackTrace(ex, true);

            //Get last stacktrace frame
            StackFrame frame = st.GetFrame(0);

            var (path, queryString, requestBody) = await _contextAccessor.HttpContext.GetRequestDataAsync();

            ServerLogM serverLog = new ServerLogM()
            {
                CreatedAt = DateTimeOffset.Now,
                ApiVersion = APIVersionInfo.APIVersion,
                ServerLogLevel = serverLogLevel,
                FileName = TryExtractFileName(frame.GetFileName(), out string fileName) ? fileName : null,
                MethodName = frame.GetMethod()?.Name,
                LineNumber = frame.GetFileLineNumber(),
                ColumnNumber = frame.GetFileColumnNumber(),
                Message = ex.Message,
                InnerMessage = ex?.InnerException?.Message,
                StackTrace = string.Format("Wersja Api: {0} ------- {1} ------- {2}", APIVersionInfo.APIVersion, ex?.StackTrace, ex?.InnerException?.StackTrace),

                Path = path,
                QueryString = queryString,
                RequestBody = requestBody,

                RunSource = ServerLogsConfig.RunSource,
                UserId = TryGetUserId()
            };

            // ograniczenie dlugosci stringow do modeli w bazie
            if (serverLog?.StackTrace != null && serverLog?.StackTrace?.Length >= 4000)
                serverLog.StackTrace = serverLog?.StackTrace?.Take(4000)?.ToString();

            if (serverLog?.Message != null && serverLog?.Message?.Length >= 4000)
                serverLog.Message = serverLog.Message?.Take(4000)?.ToString();

            if (serverLog?.InnerMessage != null && serverLog?.InnerMessage?.Length >= 4000)
                serverLog.InnerMessage = serverLog?.InnerMessage?.Take(4000)?.ToString();

            if (serverLog?.FileName != null && serverLog?.FileName?.Length >= 255)
                serverLog.FileName = serverLog?.FileName?.Take(255)?.ToString();

            if (serverLog?.MethodName != null && serverLog?.MethodName?.Length >= 255)
                serverLog.MethodName = serverLog?.MethodName?.Take(255)?.ToString();

            //Add log to database
            await TryAddServerLogByRawQuery(serverLog);
        }

        public async Task LogException<T>(ILoggableException<T> ex) where T : Exception
        {
            if (ex == null) return;

            string jsonData = "";
            try
            {
                jsonData = JsonConvert.SerializeObject(ex.DataToSerialization, Formatting.Indented);
            }
            catch (Exception) { }

            StackTrace st = new StackTrace(ex.Exception, true);

            //Get last stacktrace frame
            StackFrame frame = st.GetFrame(0);

            var (path, queryString, requestBody) = await _contextAccessor.HttpContext.GetRequestDataAsync();

            ServerLogM serverLog = new ServerLogM()
            {
                CreatedAt = DateTimeOffset.Now,
                ApiVersion = APIVersionInfo.APIVersion,
                ServerLogLevel = ex.LogLevel,
                FileName = TryExtractFileName(frame.GetFileName(), out string fileName) ? fileName : null,
                MethodName = $"{frame.GetMethod()?.Name}, Typ: {frame.GetMethod()?.DeclaringType.Name}",
                LineNumber = frame.GetFileLineNumber(),
                ColumnNumber = frame.GetFileColumnNumber(),
                Message = ex.Exception.Message,
                InnerMessage = ex?.Exception?.InnerException?.Message,
                StackTrace = string.Format("Wersja Api: {0} ------------- {1} ------------- {2} ------------- Json Data: {3}", APIVersionInfo.APIVersion, ex?.Exception?.StackTrace, ex?.Exception?.InnerException?.StackTrace, jsonData),

                Path = path,
                QueryString = queryString,
                RequestBody = requestBody,

                RunSource = ServerLogsConfig.RunSource,
                UserId = TryGetUserId()
            };

            if (serverLog.StackTrace?.Length > 4000)  // max length that can go to db
                serverLog.StackTrace.Substring(0, 4000);

            //Add log to database
            await TryAddServerLogByRawQuery(serverLog);
        }


        public async Task LogCustomException(ServerLogLevelE serverLogLevel, string methodName, string message, string stacktrace, string innerMessage)
        {
            // błąd z niepoprawnym pobraniem zdjęcia - nie chcemy go logować
            if (methodName == "Microsoft.Azure.Storage.Common" && message == "The operation was canceled.")
                return;

            var (path, queryString, requestBody) = await _contextAccessor.HttpContext.GetRequestDataAsync();


            ServerLogM serverLog = new ServerLogM()
            {
                CreatedAt = DateTimeOffset.Now,
                ApiVersion = APIVersionInfo.APIVersion,
                ServerLogLevel = serverLogLevel,
                MethodName = methodName,
                Message = message,
                StackTrace = $"Wersja Api: {APIVersionInfo.APIVersion} ------------------- StackTrace: {stacktrace}",
                InnerMessage = innerMessage,

                Path = path,
                QueryString = queryString,
                RequestBody = requestBody,

                RunSource = ServerLogsConfig.RunSource,
                UserId = TryGetUserId()
            };

            await TryAddServerLogByRawQuery(serverLog);
        }




        private bool TryExtractFileName(string filePath, out string output)
        {
            output = null;
            try
            {
                if (filePath == null)
                    return false;

                var arr = filePath.Split("\\");
                output = arr[arr.Length - 1];

                return true;
            }
            catch
            {
                return false;
            }
        }

        private long? TryGetUserId()
        {
            try
            {
                var userId = _contextAccessor.HttpContext.User.TryGetUserId();

                return userId != 0 ? userId : (long?)null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<bool> TryAddServerLogByRawQuery(ServerLogM serverLog)
        {
            try
            {
                ApplicationDbContext _db;

                string connectionString = _configuration.GetConnectionString("Database");
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseNpgsql(connectionString)
                    .Options;

                _db = new ApplicationDbContext(options);
                await _db.ServerLogs.AddAsync(serverLog);
                await _db.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
