using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMuch
{
    public class WordSegment
    {
        public static HashSet<string> dict;
        public static bool TryToSegment(string s)
        {
            if (dict.Contains(s))
                return true;
            
            int end = 1;

            while (end <= s.Length)
            {
                if (dict.Contains(s.Substring(0, end)))
                {
                    bool success = true;
                    if (end < s.Length)
                    {
                        success = TryToSegment(s.Substring(end));
                    }

                    if (success)
                        return true;
                }                                
                end++;
            }
            return false;
        }
    }
}
