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
        /// Performs a case-insensitive equality check with a default string comparison.
        /// </summary>
        /// <param name="this">The string instance.</param>
        /// <param name="comparison">The comparison string</param>
        /// <returns></returns>
        public static bool EqualsCaseInsensitive(this string @this, string comparison)
        {
            return @this.Equals(comparison, StringComparison.OrdinalIgnoreCase);
        }

        public static bool ContainsCaseInsensitive(this string @this, string comparison)
        {
            return @this.Contains(comparison, StringComparison.OrdinalIgnoreCase);
        }

        public static string ToCamelCase(this string @this)
        {
            if (@this is null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
            
            return ConvertCaseString(@this, Case.CamelCase);
        }

        public static string ToPascalCase(this string @this)
        {
            if (@this is null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
            
            return ConvertCaseString(@this, Case.PascalCase);
        }
        
        private static string ConvertCaseString(string phrase, Case @case)
        {
            var split = phrase.Split(' ', '-', '.');
            var sb = new StringBuilder();

            if (@case == Case.CamelCase)
            {
                sb.Append(split[0].ToLower());
                split[0] = string.Empty;
            }
            else if (@case == Case.PascalCase)
            {
                sb = new StringBuilder();
            }
                
            foreach (var s in split)
            {
                var chars = s.ToCharArray();
                if (chars.Length > 0)
                {
                    chars[0] = ((new string(chars[0], 1)).ToUpper().ToCharArray())[0];
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