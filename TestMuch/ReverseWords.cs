using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMuch
{
    class ReverseWords
    {
        public string ReverseWordsInSentence(string input)
        {
            StringBuilder sb = new StringBuilder();
            // start and end index
            int start = 0, end = 0;
            int inputLength = input.Length;

            while (start < inputLength)
            {
                // move start and end to the start of the first word (trim white space at beginning)
                while (input[end] == ' ')
                {
                    start = end += 1;
                    sb.Append(' ');
                }

                // move end index till space or final string character
                while (end <= inputLength - 1 && input[end] != ' ')
                {
                    end++;
                }

                // save the end index
                int savedEnd = end;

                // move chars from end to start into the sb
                while (start < end)
                {
                    sb.Append(input[--end]);
                }

                // move start and end to saved end + 1
                start = end = savedEnd;

                // repeat until start > length of string
            }
            return sb.ToString();

        }

        public string ReverseVowels(string s)
        {

            HashSet<char> dictionary = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
            var newString = s.ToCharArray();
            int strLen = newString.Length;

            int start = 0;
            int end = strLen - 1;

            while (start < end)
            {
                while (start < strLen - 1 && !dictionary.Contains(newString[start]))
                {
                    start++;
                }

                while (end > 0 && !dictionary.Contains(newString[end]))
                {
                    end--;
                }

                if (start < end)
                {
                    newString[end] = s[start];
                    newString[start] = s[end];
                }
            }
            return new string(newString);
        }
    }
}
