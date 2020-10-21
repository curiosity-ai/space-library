using System.Text;

namespace SpaceLibrary
{
    public static class StringHelpers
    { 
        /// <summary>
        /// Helper function to transform a "lower case" string into a "Title Case" string
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            var sb = new StringBuilder(str.Length);
            var flag = true;

            for (int i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(flag ? char.ToUpperInvariant(c) : c);
                    flag = false;
                }
                else if (char.IsWhiteSpace(c))
                {
                    sb.Append(' ');
                    flag = true;
                }
                else
                {
                    flag = true;
                }
            }

            return sb.ToString();
        }
    }
}
