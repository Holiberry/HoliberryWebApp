using System;
using Holiberry.Api.Models.ServerLogs.Enums;

namespace Holiberry.Api.Models.ServerLogs.Entities
{
    public class ServerLogM
    {
        public long Id { get; set; }
        public int ApiVersion { get; set; }
        public ServerLogLevelE ServerLogLevel { get; set; }
        public DateTimeOffset CreatedAt { get; set; }


        //Exception Data
        public string Path { get; set; }
        public string QueryString { get; set; }
        public string RequestBody { get; set; }
        public string FileName { get; set; }
        public string MethodName { get; set; }
        public int LineNumber { get; set; }
        public int ColumnNumber { get; set; }


        public string Message { get; set; }
        public string InnerMessage { get; set; }
        public string StackTrace { get; set; }

        // źródło błędu (jaki host)
        public string RunSource { get; set; }


        // id użytkownika który otrzymał błąd
        public long? UserId { get; set; }



        public object ValueOrNull(string value)
        {
            if (value == null)
                return DBNull.Value;
            else
                return value;
        }
    }
}
