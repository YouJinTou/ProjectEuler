using System;
using System.Numerics;

class Program
{
    static BigInteger GetFactorial(int n)
    {
        BigInteger result = 1;

        while (n >= 1)
        {
            result *= n;

            n--;
        }

        return result;
    } 

    static void Main()
    {
        var factorialOneHundred = GetFactorial(100).ToString();
        int result = 0;

        foreach (var ch in factorialOneHundred)
        {
            result += (int)char.GetNumericValue(ch);
        }

        Console.WriteLine("The sum of all digits of 100! is {0}.", result);
    }
}