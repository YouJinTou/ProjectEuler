using System;
using System.Numerics;

class Program
{
    static int GetFibIndex()
    {
        BigInteger prevFib = 1;
        BigInteger currentFib = 1;
        int index = 3;

        while (!HasOneThousandDigits(currentFib))
        {
            BigInteger temp = currentFib;
            currentFib += prevFib;
            prevFib = temp;

            index++;
        }

        return index--;
    } 

    static bool HasOneThousandDigits(BigInteger currentFib)
    {
        var numberToString = currentFib.ToString();

        return numberToString.Length == 1000;
    }

    static void Main()
    {
        var fibIndex = GetFibIndex();

        Console.WriteLine("The index is {0}.", fibIndex);
    }
}