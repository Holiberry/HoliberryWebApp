using System;
using System.Collections.Generic;
using Holiberry.Api.Extensions;
using Holiberry.Api.Models.Exceptions;
using Holiberry.Api.Models.ServerLogs.Enums;

namespace Holiberry.Api.Extensions
{
    public static class ExceptionExtensions
    {
        public static T AddError<T>(this T ex, string errorKey, string errorValue) where T : Exception
        {
            if (errorKey != null && errorValue != null)
            {
                ex.Data.Add(errorKey, errorValue);
            }
            return ex;
        }

        public static T AddError<T, TEnum>(this T ex, TEnum errorEnum) where T : Exception where TEnum : Enum
        {
            ex.Data.Add(errorEnum.ToString(), errorEnum.GetDisplayName());

            return ex;
        }


        public static T SingleError<T>(this T ex, string errorKey, string errorValue) where T : Exception
        {
            if (errorKey != null && errorValue != null)
            {
                ex = (T)Activator.CreateInstance(ex.GetType(), errorValue);
                ex.Data.Add(errorKey, errorValue);
            }
            return ex;
        }

        public static T SingleError<T, TEnum>(this T ex, TEnum errorEnum) where T : Exception where TEnum : Enum
        {
            ex = (T)Activator.CreateInstance(ex.GetType(), errorEnum.GetDisplayName());
            ex.Data.Add(errorEnum.ToString(), errorEnum.GetDisplayName());

            return ex;
        }

        public static T AddRangeErrors<T>(this T ex, Dictionary<string, string> errors) where T : Exception
        {
            foreach (var error in errors)
            {
                ex.Data.Add(error.Key, error.Value);
            }
            return ex;
        }

        public static T LogException<T>(this ILoggableException<T> ex, dynamic dataToSerialization = null, ServerLogLevelE logLevel = ServerLogLevelE.Warning) where T : Exception
        {
            return ex.LogException(dataToSerialization, logLevel);
        }

    }
}
