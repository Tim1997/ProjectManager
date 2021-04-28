using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace template__
{
    public static class StringHelper
    {
        public static int GetNumber(this string s)
        {
            var numberString = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (Char.IsDigit(s[i]))
                    numberString += s[i];
            }

            return numberString.Length == 0 ? 0 : Int32.Parse(numberString);
        }
    }
}
