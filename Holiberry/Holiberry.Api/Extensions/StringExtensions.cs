using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holiberry.Api.Extensions
{
    public static class StringExtensions
    {
        public static string PolishToUpperASCIICharacters(this string str)
        {
            return str == null ? str : str.ToUpper()
                .Replace('Ę', 'E')
                .Replace('Ó', 'O')
                .Replace('Ą', 'A')
                .Replace('Ś', 'S')
                .Replace('Ł', 'L')
                .Replace('Ż', 'Z')
                .Replace('Ź', 'Z')
                .Replace('Ć', 'C')
                .Replace('Ń', 'N');
        }



        public static string EncodeBase64(this string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }

        public static string DecodeBase64(this string value)
        {
            var valueBytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }


        /// <summary>
        /// Pobiera {count} pierwszych znaków w stringu
        /// </summary>
        public static string Left(this string @this, int count)
        {
            if (@this.Length <= count)
            {
                return @this;
            }
            else
            {
                return @this.Substring(0, count);
            }
        }
        
        



    }
}
