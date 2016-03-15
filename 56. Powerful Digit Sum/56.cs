using System;
using System.Numerics;

class Program
{
    static int GetDigitSum(int baseNumber, int exponent)
    {
        BigInteger product = BigInteger.Pow(baseNumber, exponent);
        char[] productToCharArray = product.ToString().ToCharArray();
        int result = 0;

        foreach (var ch in productToCharArray)
        {
            result += (int)char.GetNumericValue(ch);
        }

        return result;
    }

    static void Main()
    {
        int best = 0;
        int current = 0;

        for (int baseNumber = 2; baseNumber < 100; baseNumber++)
        {
            for (int exponent = 2; exponent < 100; exponent++)
            {
                current = GetDigitSum(baseNumber, exponent);

                if (current > best)
                {
                    best = current;
                }
            }
        }

        Console.WriteLine("The best digit sum is {0}.", best);
    }    
}