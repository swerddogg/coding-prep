using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMuch
{
    class PalindromeStuff
    {
        //public IList<IList<int>> PalindromePairs(string[] words)
        //{
        //    IList<IList<int>> results = new List<IList<int>>();
        //    //var sb = new StringBuilder();
        //    for (int i = 0; i < words.Length; i++)
        //    {
        //       // char[] first = words[i].ToCharArray().Con;

        //        for (int j = 0; j < words.Length; j++)
        //        {
        //            if (j == i)
        //            {
        //                continue;
        //            }

        //            if (IsPalindrome(first + words[j]))
        //            {
        //                Console.WriteLine("Found one: {0}, {1}", i, j);
        //                results.Add(new List<int>() { i, j });
        //            }
        //        }
        //    }
        //    return results; // as IList<IList<int>>;
        //}

        private bool IsPalindrome(string word)
        {
            int start = 0;
            int end = word.Length - 1;

            while (start < end)
            {
                if (word[start] != word[end])
                    return false;

                start++; end--;
            }
            return true;
        }
    }
}
