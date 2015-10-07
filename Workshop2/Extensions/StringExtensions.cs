using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2
{
    public static class StringExtensions
    {
        public static string CenterAlignString(this string s, string other)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            if (s.Length > other.Length)
            {
                throw new InvalidOperationException();
            }

            int halfSpace = (other.Length - s.Length) / 2;

            return s.PadLeft(halfSpace + s.Length).PadRight(other.Length);
        }
    }
}
