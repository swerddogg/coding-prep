using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMuch
{
    class ConvertStringToInt
    {
        public int ConvertToInt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(input);
            }

            int convertedValue = 0;
            int end = input.Length;
            int tens = 1;

            // iterate through each character starting from the end
            while (--end >=0)
            {
                char digit = input[end];
                if (digit >= '0' && digit <= '9')
                {
                    // found a good digit, convert it
                    int previous = convertedValue;
                    convertedValue += (digit - (char)48) * tens;
                    if (previous > convertedValue)
                    {
                        throw new OverflowException($"input value {input} is too large to be represented by an int");
                    }
                    tens *= 10;
                }
                else if (end == 0 && (digit == '+' || digit == '-'))
                {
                    // flip the bit if necessary
                    if (digit == '-')
                    {
                        convertedValue = -convertedValue;
                    }
                }
                else
                {                    
                    // found an invalid digit
                    throw new ArgumentException("input contains invalid input which cannot be converted to an int");
                }
                
            }
            return convertedValue;
        }
    }
}
