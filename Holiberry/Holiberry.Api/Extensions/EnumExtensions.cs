
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Holiberry.Api.Common.DTO;

namespace Holiberry.Api.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            try
            {
                var ev = enumValue.GetType()
                    .GetMember(enumValue.ToString())
                    .FirstOrDefault();

                if (ev != null && ev.CustomAttributes.Count() > 0)
                {
                    return ev.GetCustomAttribute<DisplayAttribute>()
                        .GetName();
                }
                else
                {
                    return enumValue.ToString();
                }

            }
            catch
            {
                return enumValue.ToString();
            }

        }

        public static string GetFlagEnumDisplayName(this Enum enumValue)
        {
            try
            {
                var enumType = enumValue.GetType();
                var names = new List<string>();
                var enumValues = Enum.GetValues(enumType);

                for (int i = 0; i < enumValues.Length; i++)
                {
                    if (enumValues.Length > 1 && i == 0) // ommit first - default value
                        continue;

                    var e = enumValues.GetValue(i);

                    var flag = (Enum)e;
                    if (enumValue.HasFlag(flag))
                    {
                        names.Add(flag.GetDisplayName());
                    }
                }
                if (names.Count <= 0) throw new ArgumentException();
                if (names.Count == 1) return names.First();

                return string.Join(", ", names);
            }
            catch (Exception)
            {
                return enumValue.ToString();
            }
        }
        

        public static List<EnumValueDTO> GetEnumValues<T>() where T : Enum
        {
            try
            {
                var enumValues = Enum.GetValues(typeof(T));
                
                var list = new List<EnumValueDTO>(enumValues.Length);
                foreach(T enumValue in enumValues)
                {
                    list.Add
                    (
                        new EnumValueDTO(int.Parse(enumValue.ToString("d")), enumValue.GetDisplayName())
                    );
                }

                return list;
            }
            catch (Exception)
            {
                return new List<EnumValueDTO>();
            }
        }
    }
}
