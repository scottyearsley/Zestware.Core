using System;
using System.Text;

namespace Zestware
{
    /// <summary>
    /// A collection of string extensions. 
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Performs a case-insensitive equality check.
        /// </summary>
        /// <param name="this">The string instance.</param>
        /// <param name="value">The comparison string</param>
        /// <returns>true is the value parameter matches the string case-insensitive, otherwise false.</returns>
        public static bool EqualsCaseInsensitive(this string @this, string value)
        {
            return @this.Equals(value, StringComparison.OrdinalIgnoreCase);
        }
        
        /// <summary>
        /// Performs a case-insensitive contains check.
        /// </summary>
        /// <param name="this">The string instance.</param>
        /// <param name="value">The value to look for.</param>
        /// <returns>true if the value parameter occurs in the string case-insensitive or if the string is empty,
        /// otherwise false.</returns>
        public static bool ContainsCaseInsensitive(this string @this, string value)
        {
            return @this.Contains(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Splits the string into substrings based on the provided separator, trimming value and removing empty values.
        /// </summary>
        /// <param name="this">The string instance.</param>
        /// <param name="separator">A string that delimits the substrings in this string.</param>
        /// <returns></returns>
        public static string[] Split(this string @this, string separator)
        {
            return @this.Split(separator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }

        public static string ToCamelCase(this string @this)
        {
            ArgumentNullException.ThrowIfNull(@this);
            
            return ConvertCaseString(@this, Case.CamelCase);
        }

        public static string ToPascalCase(this string @this)
        {
            ArgumentNullException.ThrowIfNull(@this);
            
            return ConvertCaseString(@this, Case.PascalCase);
        }
        
        private static string ConvertCaseString(string phrase, Case @case)
        {
            var split = phrase.Split(' ', '-', '.');
            var sb = new StringBuilder();

            switch (@case)
            {
                case Case.CamelCase:
                    sb.Append(split[0].ToLower());
                    split[0] = string.Empty;
                    break;
                case Case.PascalCase:
                    break;
                default:
                    throw new ArgumentException("Invalid Case type", nameof(@case));
            }
                
            foreach (var s in split)
            {
                var chars = s.ToCharArray();
                if (chars.Length > 0)
                {
                    chars[0] = new string(chars[0], 1).ToUpper().ToCharArray()[0];
                }
                sb.Append(new string(chars));
            }
            return sb.ToString();
        }
        
        private enum Case
        {
            PascalCase,
            CamelCase
        }
    }
}