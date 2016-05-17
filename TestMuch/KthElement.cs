using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMuch
{
    public class KthElement
    {
        public int GetKthElement(int[] array1, int[] array2, int k)
        {
            int index1 = 0;
            int index2 = 0;
            int indexCounter = -1;
            if (k > array1.Length + array2.Length - 1 || k < 0) { return -1; }

            while (++indexCounter < k)
            {
                if (index1 != array1.Length && 
                    (index2 == array2.Length || array1[index1] < array2[index2]))
                {
                    index1++;
                }
                else
                {
                    index2++;
                }
            }

            if (index1 != array1.Length &&
                  (index2 == array2.Length || array1[index1] < array2[index2]))
            {
                return array1[index1];
            }
            else
            {
                return array2[index2];
            }
        }
    }
}
