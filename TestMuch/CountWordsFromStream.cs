using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMuch
{
    class CountWordsFromStream
    {
        string letters;
        string[] words;
        Dictionary<int, List<string>> wordCountToWordMap;
        int maxWordLength, minWordLength;

        public CountWordsFromStream(string inputs, string[] wordArray)
        {
            letters = inputs;
            words = wordArray;
        }

        private void CreateWordCountMap()
        {
            wordCountToWordMap = new Dictionary<int, List<string>>();
            maxWordLength = 0;
            minWordLength = int.MaxValue;
            foreach (string w in words)
            {
                if (!wordCountToWordMap.ContainsKey(w.Length))
                {
                    if (w.Length > maxWordLength)
                    {
                        maxWordLength = w.Length;
                    }

                    if (w.Length < minWordLength)
                    {
                        minWordLength = w.Length;
                    }

                    wordCountToWordMap[w.Length] = new List<string>();
                }
                wordCountToWordMap[w.Length].Add(w);
            }
        }

        //public Dictionary<string, int> FindWordCountsInStream()
        //{
        //    Dictionary<string, int> wordCountMap = new Dictionary<string, int>();

        //    int start = 0;
        //    int end = minWordLength -1;

        //    for (int tEnd = end; tEnd< maxWordLength; tEnd++)
        //    {

        //    }




        //}


        
    }
}
