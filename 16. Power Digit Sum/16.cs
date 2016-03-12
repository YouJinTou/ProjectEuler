using System;
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        BigInteger result = (BigInteger)Math.Pow(2, 1000);
        var resultToString = result.ToString();
        var sum = 0;

        for (int i = 0; i < resultToString.Length; i++)
        {
            sum += (int)char.GetNumericValue(resultToString[i]);
        }

        Console.WriteLine("The sum of the digits is {0}.", sum);
    }
}