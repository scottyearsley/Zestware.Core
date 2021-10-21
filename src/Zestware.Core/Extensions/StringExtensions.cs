using System;
using System.Text;

namespace Zestware
{
    public static class StringExtensions
    {
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
            return ConvertCaseString(@this, Case.CamelCase);
        }

        public static string ToPascalCase(this string @this)
        {
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
                    chars[0] = ((new String(chars[0], 1)).ToUpper().ToCharArray())[0];
                }
                sb.Append(new String(chars));
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